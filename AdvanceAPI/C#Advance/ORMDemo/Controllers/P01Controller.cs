/*using ORMDemo.BL;
using ORMDemo.Models;
using ORMDemo.Models.DTO;
using ORMDemo.Models.POCO;
using System.Web.Http;

namespace ORM.Controllers
{
    [RoutePrefix("api/player")] // Base route for the P01Controller
    public class P01Controller : ApiController
    {
        private readonly P01Service playerService;

        public P01Controller()
        {
            playerService = new P01Service();
        }

        // Add Player
        [HttpPost]
        [Route("add")] // POST api/player/add
        public IHttpActionResult AddPlayer(DTOYMP01 playerDto)
        {
            YMP01 playerModel = playerService.PreSavePlayer(playerDto);
            var (isValidPlayer, playerValidationMessage) = playerService.ValidateOnSavePlayer(playerModel);

            if (isValidPlayer)
            {
                Response response = playerService.SavePlayer(playerModel);
                return Ok(response.Message);
            }

            return BadRequest(playerValidationMessage);
        }

        // Update Player
        [HttpPut]
        [Route("update/{id}")] // PUT api/player/update/{id}
        public IHttpActionResult UpdatePlayer(int id, DTOYMP01 playerDto)
        {
            YMP01 editPlayerModel = playerService.PreDeletePlayer(id);
            if (editPlayerModel != null)
            {
                editPlayerModel.P01F02 = playerDto.P01102;
                editPlayerModel.P01F03 = playerDto.P01103;
                editPlayerModel.P01F04 = playerDto.P01104;
                editPlayerModel.P01F05 = playerDto.P01105;

                var (isValidEditPlayer, editPlayerValidationMessage) = playerService.ValidateOnSavePlayer(editPlayerModel);

                if (isValidEditPlayer)
                {
                    Response response = playerService.SavePlayer(editPlayerModel);
                    return Ok(response.Message);
                }

                return BadRequest(editPlayerValidationMessage);
            }

            return NotFound();
        }

        // Delete Player
        [HttpDelete]
        [Route("delete/{id}")] // DELETE api/player/delete/{id}
        public IHttpActionResult DeletePlayer(int id)
        {
            YMP01 preDeletePlayerModel = playerService.PreDeletePlayer(id);
            var (isValidDeletePlayer, deletePlayerValidationMessage) = playerService.ValidateOnDeletePlayer(preDeletePlayerModel);

            if (isValidDeletePlayer)
            {
                Response response = playerService.DeletePlayer(id);
                return Ok(response.Message);
            }

            return BadRequest(deletePlayerValidationMessage);
        }
    }
}
*/