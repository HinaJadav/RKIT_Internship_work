using System;
using System.Web.Mvc;
using DatabaseWithC__CRUD.BL.Service;
using DatabaseWithC__CRUD.Models.DTO;

namespace DatabaseWithC__CRUD.Controllers
{
    [RoutePrefix("api/team")]  
    public class T01Controller : Controller
    {
        private readonly T01Service _teamService;

        public T01Controller()
        {
            _teamService = new T01Service();
        }

        // Default route: api/team
        public ActionResult Index()
        {
            return View();
        }

        // POST: api/team/add
        [HttpPost]
        [Route("add")]
        public JsonResult AddTeam(DTOYMT01 dtoymt01)
        {
            try
            {
                _teamService.AddTeam(dtoymt01);
                return Json(new { success = true, message = "Team added successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"An error occurred: {ex.Message}" });
            }
        }

        // GET: api/team/all
        [HttpGet]
        [Route("all")]
        public JsonResult GetAllTeams()
        {
            try
            {
                var teams = _teamService.GetAllTeams();
                if (teams == null || teams.Count == 0)
                {
                    return Json(new { success = false, message = "No teams found." }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { success = true, teams }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"An error occurred: {ex.Message}" }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: api/team/{id}
        [HttpGet]
        [Route("{id:int}")]
        public JsonResult GetTeamById(int id)
        {
            try
            {
                var team = _teamService.GetTeamById(id);
                if (team == null)
                {
                    return Json(new { success = false, message = "Team not found." }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { success = true, team }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"An error occurred: {ex.Message}" }, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: api/team/update
        [HttpPost]
        [Route("update")]
        public JsonResult UpdateTeam(DTOYMT01 dtoymt01)
        {
            try
            {
                _teamService.UpdateTeam(dtoymt01);
                return Json(new { success = true, message = "Team updated successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"An error occurred: {ex.Message}" });
            }
        }

        // POST: api/team/delete/{id}
        [HttpPost]
        [Route("delete/{id:int}")]
        public JsonResult DeleteTeam(int id)
        {
            try
            {
                _teamService.DeleteTeam(id);
                return Json(new { success = true, message = "Team deleted successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"An error occurred: {ex.Message}" });
            }
        }
    }
}
