using DatabaseWithC__CRUD.BL.Service;
using DatabaseWithC__CRUD.Models.DTO;
using DatabaseWithC__CRUD.Models.POCO;
using System.Collections.Generic;
using System.Web.Http;

namespace DatabaseWithC__CRUD.Controllers
{
    /// <summary>
    /// Controller to manage team-related actions such as Add, Get, Update, and Delete.
    /// </summary>
    [RoutePrefix("api/t01")]
    public class T01Controller : ApiController
    {
        private readonly T01Service _t01Service;

        /// <summary>
        /// Initializes a new instance of the T01Controller class.
        /// </summary>
        public T01Controller()
        {
            _t01Service = new T01Service();
        }

        /// <summary>
        /// Gets all teams.
        /// </summary>
        /// <returns>A list of all teams.</returns>
        
        // GET: api/t01
        [HttpGet]
        public IHttpActionResult GetAllTeams()
        {
            try
            {
                List<YMT01> teams = _t01Service.GetAllTeams();
                return Ok(teams); // Return 200 OK with the list of teams
            }
            catch (System.Exception ex)
            {
                return InternalServerError(ex); // Return 500 Internal Server Error
            }
        }

        /// <summary>
        /// Gets a team by its ID.
        /// </summary>
        /// <param name="id">The ID of the team.</param>
        /// <returns>The team if found; otherwise, 404 Not Found.</returns>

        // GET: api/t01/{id}
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetTeamById(int id)
        {
            try
            {
                YMT01 team = _t01Service.GetTeamById(id);

                if (team == null)
                {
                    return NotFound(); // Return 404 if team is not found
                }

                return Ok(team); // Return 200 OK with the team details
            }
            catch (System.Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Adds a new team.
        /// </summary>
        /// <param name="dtoymt01">The DTO containing team data.</param>
        /// <returns>200 OK if added successfully; 400 if failed.</returns>

        // POST: api/t01
        [HttpPost]
        public IHttpActionResult AddTeam([FromBody] DTOYMT01 dtoymt01)
        {
            if (dtoymt01 == null)
            {
                return BadRequest("Invalid team data."); // Return 400 Bad Request if the input is null
            }

            try
            {
                bool result = _t01Service.AddTeam(dtoymt01);

                if (result)
                {
                    return Ok("Team added successfully."); // Return 200 OK
                }

                return BadRequest("Failed to add team."); // Return 400 if the service failed
            }
            catch (System.Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Updates an existing team.
        /// </summary>
        /// <param name="dtoymt01">The DTO containing updated team data.</param>
        /// <returns>200 OK if updated successfully; 400 if failed.</returns>

        // PUT: api/t01
        [HttpPut]
        public IHttpActionResult UpdateTeam([FromBody] DTOYMT01 dtoymt01)
        {
            if (dtoymt01 == null)
            {
                return BadRequest("Invalid team data."); // Return 400 Bad Request
            }

            try
            {
                // Check if the record exists in the database
                YMT01 existingTeam = _t01Service.GetTeamById(dtoymt01.T01101);
                if (existingTeam == null)
                {
                    return NotFound(); // Return 404 Not Found if the record does not exist
                }

                // Update the record
                bool result = _t01Service.UpdateTeam(dtoymt01);

                if (result)
                {
                    return Ok("Team updated successfully."); // Return 200 OK
                }

                return BadRequest("Failed to update team."); // Return 400 if the service failed
            }
            catch (System.Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Deletes a team by its ID.
        /// </summary>
        /// <param name="id">The ID of the team to delete.</param>
        /// <returns>200 OK if deleted successfully; 400 if failed.</returns>

        // DELETE: api/t01/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult DeleteTeam(int id)
        {
            try
            {
                // Check if the record exists in the database
                YMT01 existingTeam = _t01Service.GetTeamById(id);
                if (existingTeam == null)
                {
                    return NotFound(); // Return 404 Not Found if the record does not exist
                }

                // Proceed to delete the record
                bool result = _t01Service.DeleteTeam(id);

                if (result)
                {
                    return Ok("Team deleted successfully."); // Return 200 OK
                }

                return BadRequest("Failed to delete team."); // Return 400 if the service failed
            }
            catch (System.Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
