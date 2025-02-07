using ORMDemo.DB;
using ORMDemo.Extension;
using ORMDemo.Models;
using ORMDemo.Models.DTO;
using ORMDemo.Models.Enums;
using ORMDemo.Models.POCO;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;

namespace ORMDemo.BL
{
    /// <summary>
    /// Service class for handling Game-related operations, including CRUD operations and validation.
    /// </summary>
    public class G01Service
    {
        private YMG01 _g01Obj;
        private int _gameId;
        private Response _response;

        public OperationType Type { get; set; }

        public G01Service()
        {
            _response = new Response();
        }

        /// <summary>
        /// Converts a Game DTO to a Game POCO for database operations.
        /// </summary>
        /// <param name="dtoGame">Game DTO object</param>
        public void PreSave(DTOYMG01 dtoGame)
        {
            _g01Obj = dtoGame.Convert<YMG01>();
            if (Type == OperationType.E || Type == OperationType.D)
            {
                if (dtoGame.G01101 > 0)
                {
                    _gameId = dtoGame.G01101;
                }
            }
        }

        /// <summary>
        /// Checks if a game exists in the database.
        /// </summary>
        /// <param name="gameId">Game ID</param>
        /// <returns>True if the game exists, otherwise false</returns>
        private bool IsGameExist(int gameId)
        {
            using (var db = DBConnection.OpenConnection())
            {
                return db.Exists<YMG01>(gameId);
            }
        }

        /// <summary>
        /// Validates the game data before performing save or delete operations.
        /// </summary>
        /// <returns>Response object indicating validation status</returns>
        public Response Validation()
        {
            if (Type == OperationType.E || Type == OperationType.D)
            {
                if (_gameId <= 0)
                {
                    _response.IsError = true;
                    _response.Message = "Enter a valid Game ID.";
                }
                else if (!IsGameExist(_gameId))
                {
                    _response.IsError = true;
                    _response.Message = "Game does not exist.";
                }
            }
            return _response;
        }

        /// <summary>
        /// Saves or updates a game record in the database.
        /// </summary>
        /// <returns>Response object with operation result</returns>
        public Response Save()
        {
            try
            {
                using (var db = DBConnection.OpenConnection())
                {
                    if (Type == OperationType.A)
                    {
                        db.Insert(_g01Obj);
                        _response.Message = $"Successfully saved game {_g01Obj.G01F02}.";
                    }
                    else if (Type == OperationType.E)
                    {
                        db.Update(_g01Obj);
                        _response.Message = $"Successfully updated game {_g01Obj.G01F02}.";
                    }
                }
            }
            catch (Exception ex)
            {
                _response.IsError = true;
                _response.Message = $"Error: {ex.Message}";
            }
            return _response;
        }

        /// <summary>
        /// Deletes a game record from the database.
        /// </summary>
        /// <param name="gameId">Game ID</param>
        /// <returns>Response object indicating the deletion status</returns>
        public Response Delete(int gameId)
        {
            try
            {
                using (var db = DBConnection.OpenConnection())
                {
                    db.DeleteById<YMG01>(gameId);
                }
                _response.Message = "Game is deleted!";
            }
            catch (Exception ex)
            {
                _response.IsError = true;
                _response.Message = ex.Message;
            }
            return _response;
        }

        /// <summary>
        /// Retrieves all game records from the database.
        /// </summary>
        /// <returns>List of games</returns>
        public List<YMG01> GetAll()
        {
            using (var db = DBConnection.OpenConnection())
            {
                return db.Select<YMG01>();
            }
        }

        /// <summary>
        /// Retrieves a specific game by its ID.
        /// </summary>
        /// <param name="gameId">Game ID</param>
        /// <returns>Game object if found, otherwise null</returns>
        public YMG01 GetGameById(int gameId)
        {
            using (var db = DBConnection.OpenConnection())
            {
                return db.SingleById<YMG01>(gameId);
            }
        }

        /// <summary>
        /// Retrieves the total number of games stored in the database.
        /// </summary>
        /// <returns>Count of games</returns>
        public int GetTotalGamesCount()
        {
            using (var db = DBConnection.OpenConnection())
            {
                return db.Scalar<int>(db.From<YMG01>().Select(x => Sql.Count("*")));
            }
        }

        /// <summary>
        /// Retrieves a list of games along with their associated players.
        /// </summary>
        /// <returns>List of game-player objects</returns>
        public List<object> GetGamesWithPlayers()
        {
            using (var db = DBConnection.OpenConnection())
            {
                var query = db.From<YMG01>()
                    .Join<YMG01, YMP01>((game, player) => game.G01F01 == player.P01F05)
                    .Select<YMG01, YMP01>((game, player) => new
                    {
                        game.G01F01,
                        game.G01F02,
                        player.P01F01,
                        player.P01F02
                    });
                return db.Select<object>(query);
            }
        }
    }
}
