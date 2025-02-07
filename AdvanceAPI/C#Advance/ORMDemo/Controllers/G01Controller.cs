using ORMDemo.BL;
using ORMDemo.Models;
using ORMDemo.Models.DTO;
using ORMDemo.Models.Enums;
using System.Collections.Generic;
using System.Web.Http;

namespace ORM.Controllers
{
    [RoutePrefix("api/game")] // Base route for the GameController
    public class G01Controller : ApiController
    {
        private G01Service _gameService;
        private Response _response;

        public G01Controller()
        {
            _gameService = new G01Service();
        }

        /// <summary>
        /// Adds a new game.
        /// </summary>
        /// <param name="gameDto">Game DTO object.</param>
        /// <returns>Response indicating success or failure.</returns>
        [HttpPost]
        [Route("add")] // POST api/game/add
        public Response AddGame(DTOYMG01 gameDto)
        {
            _gameService.Type = OperationType.A;
            _gameService.PreSave(gameDto);
            _response = _gameService.Validation();
            if (!_response.IsError)
            {
                _response = _gameService.Save();
            }
            return _response;
        }

        /// <summary>
        /// Updates an existing game.
        /// </summary>
        /// <param name="gameDto">Game DTO object.</param>
        /// <returns>Response indicating success or failure.</returns>
        [HttpPut]
        [Route("update")] // PUT api/game/update/{id}
        public Response UpdateGame(DTOYMG01 gameDto)
        {
            _gameService.Type = OperationType.E;
            _gameService.PreSave(gameDto);
            _response = _gameService.Validation();
            if (!_response.IsError)
            {
                _response = _gameService.Save();
            }
            return _response;
        }

        /// <summary>
        /// Deletes a game by ID.
        /// </summary>
        /// <param name="id">Game ID.</param>
        /// <returns>Response indicating success or failure.</returns>
        [HttpDelete]
        [Route("delete/{id}")] // DELETE api/game/delete/{id}
        public Response DeleteGame(int id)
        {
            return _gameService.Delete(id);
        }

        /// <summary>
        /// Retrieves all games.
        /// </summary>
        /// <returns>List of all games.</returns>
        [HttpGet]
        [Route("getAll")]
        public IHttpActionResult GetAll()
        {
            return Ok(_gameService.GetAll());
        }

        /// <summary>
        /// Retrieves a game by its ID.
        /// </summary>
        /// <param name="id">Game ID.</param>
        /// <returns>Game object.</returns>
        [HttpGet]
        [Route("getOneById/{id}")]
        public IHttpActionResult GetById(int id)
        {
            return Ok(_gameService.GetGameById(id));
        }

        /// <summary>
        /// Gets the total count of games.
        /// </summary>
        /// <returns>Total count of games.</returns>
        [HttpGet]
        [Route("getCount")]
        public IHttpActionResult GetGameCount()
        {
            return Ok(_gameService.GetTotalGamesCount());
        }

        /// <summary>
        /// Retrieves all games along with their associated players.
        /// </summary>
        /// <returns>List of games with player details.</returns>
        [HttpGet]
        [Route("with-players")]
        public IHttpActionResult GetGamesWithPlayers()
        {
            List<object> result = _gameService.GetGamesWithPlayers();
            return Ok(result);
        }
    }
}
