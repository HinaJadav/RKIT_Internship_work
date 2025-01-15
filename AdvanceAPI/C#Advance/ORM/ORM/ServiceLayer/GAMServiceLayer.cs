using ORM.Data;
using ORM.DTO;
using ORM.POCO;
using ServiceStack.OrmLite;
using System;
using System.Data;

namespace ORM.ServiceLayer
{
    public class GAMServiceLayer
    {
        // PreSaveGame() : Convert Game DTO to Game POCO
        /// <summary>
        /// Converts a Game DTO to a Game POCO for database operations.
        /// </summary>
        public GAM01 PreSaveGame(DTOGAM01 dtoGame)
        {
            // Create a new instance of GAM01 POCO and map DTO values
            var game = new GAM01
            {
                M02F02 = dtoGame.M02102,  // Mapping DTO field to POCO property
                M03F03 = dtoGame.M03103   // Mapping DTO field to POCO property
            };

            return game;  // Return the mapped POCO object
        }

        // ValidateOnSaveGame(): Validates game POCO before saving
        /// <summary>
        /// Validates the Game POCO to ensure it is valid before saving to the database.
        /// </summary>
        public (bool IsValid, string Message) ValidateOnSaveGame(GAM01 game)
        {
            // Check if the game name is empty
            if (string.IsNullOrEmpty(game.M02F02))
                return (false, "Game name cannot be empty.");

            // Check if the number of players is greater than zero
            if (game.M03F03 <= 0)
                return (false, "Number of player in team must be greater than zero.");

            // If both conditions are satisfied, return success message
            return (true, "Game data validation passed.");
        }

        // Save(): Save Game POCO to the Database
        /// <summary>
        /// Saves the Game POCO to the database and returns a response with the result.
        /// </summary>
        public Response SaveGame(GAM01 game)
        {
            Response response = new Response();

            try
            {
                // Open a database connection and start a transaction
                using (var db = DbConnection.OpenConnection())
                using (var trans = db.OpenTransaction())
                {
                    // Save the game, if it exists it will update, otherwise insert a new record
                    db.Save(game);
                    trans.Commit();  // Commit the transaction to save changes
                }

                // Set the success message in the response
                response.Message = $"Successfully saved game {game.M02F02}.";
            }
            catch (Exception ex)
            {
                // In case of error, set IsError flag and message
                response.IsError = true;
                response.Message = $"Error: {ex.Message}";
            }

            return response;  // Return the response object with result
        }

        // PreDeleteGame(): Prepare Game record for deletion
        /// <summary>
        /// Prepares the Game record by fetching it before deletion.
        /// </summary>
        public GAM01 preDeleteGame(int id)
        {
            using (var db = DbConnection.OpenConnection())
            {
                // Fetch the game record by its ID
                return db.SingleById<GAM01>(id);
            }
        }

        // ValidateOnDeleteGame(): Validate game record before deletion
        /// <summary>
        /// Validates if the Game record can be deleted.
        /// </summary>
        public (bool IsValid, string Message) ValidateOnDeleteGame(GAM01 game)
        {
            // If the game is null, it means it can't be deleted
            if (game == null)
                return (false, "Game not found.");

            // Otherwise, the game can be deleted
            return (true, "Game can be deleted.");
        }

        // Delete(): Delete Game from Database and return responses
        /// <summary>
        /// Deletes a Game from the database and returns a response indicating success or failure.
        /// </summary>
        public Response DeleteGame(int id)
        {
            Response response = new Response();

            try
            {
                using (var db = DbConnection.OpenConnection())
                using (var trans = db.OpenTransaction())
                {
                    // Fetch the game record by ID
                    var game = db.SingleById<GAM01>(id);
                    if (game != null)
                    {
                        // If game exists, delete it from the database
                        db.Delete(game);
                        trans.Commit();  // Commit the transaction to save changes
                        // Set the success message
                        response.Message = $"Game {game.M02F02} successfully deleted.";
                    }
                    else
                    {
                        // If game not found, set error message
                        response.IsError = true;
                        response.Message = "Game not found.";
                    }
                }
            }
            catch (Exception ex)
            {
                // In case of an error, set error flag and message
                response.IsError = true;
                response.Message = $"Error: {ex.Message}";
            }

            return response;  // Return the response object with result
        }
    }
}
