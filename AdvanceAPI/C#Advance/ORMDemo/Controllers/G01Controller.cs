using ORMDemo.BL;
using ORMDemo.Models;
using ORMDemo.Models.DTO;
using ORMDemo.Models.POCO;
using System.Web.Http;

namespace ORM.Controllers
{
    [RoutePrefix("api/game")] // Base route for the GameController
    public class G01Controller : ApiController
    {
        private readonly G01Service gameService;

        public G01Controller()
        {
            gameService = new G01Service();
        }

        // Add Game
        [HttpPost]
        [Route("add")] // POST api/game/add
        public IHttpActionResult AddGame(DTOYMG01 gameDto)
        {
            YMG01 gameModel = gameService.PreSaveGame(gameDto);
            var (isValidGame, gameValidationMessage) = gameService.ValidateOnSaveGame(gameModel);

            if (isValidGame)
            {
                Response response = gameService.SaveGame(gameModel);
                return Ok(response.Message);
            }

            return BadRequest(gameValidationMessage);
        }

        // Update Game
        [HttpPut]
        [Route("update/{id}")] // PUT api/game/update/{id}
        public IHttpActionResult UpdateGame(int id, DTOYMG01 gameDto)
        {
            YMG01 editGameModel = gameService.preDeleteGame(id); // change method name
            if (editGameModel != null)
            {
                editGameModel.G01F02 = gameDto.G01102;
                editGameModel.G01F03 = gameDto.G01103;
                var (isValidUpdateGame, updateGameValidationMessage) = gameService.ValidateOnSaveGame(editGameModel);

                if (isValidUpdateGame)
                {
                    Response response = gameService.SaveGame(editGameModel);
                    return Ok(response.Message);
                }

                return BadRequest(updateGameValidationMessage);
            }

            return NotFound();
        }

        // Delete Game
        [HttpDelete]
        [Route("delete/{id}")] // DELETE api/game/delete/{id}
        public IHttpActionResult DeleteGame(int id)
        {
            YMG01 preDeleteGameModel = gameService.preDeleteGame(id);
            var (isValidDeleteGame, deleteGameValidationMessage) = gameService.ValidateOnDeleteGame(preDeleteGameModel);

            if (isValidDeleteGame)
            {
                Response response = gameService.DeleteGame(id);
                return Ok(response.Message);
            }

            return BadRequest(deleteGameValidationMessage);
        }
    }
}
// update , updateonly, insert, insertonly, select with join, scalar 