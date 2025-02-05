﻿using ORMDemo.DB;
using ORMDemo.Models;
using ORMDemo.Models.DTO;
using ORMDemo.Models.POCO;
using ServiceStack.OrmLite;
using System;

namespace ORMDemo.BL
{
    public class G01Service
    {
        // PreSaveGame() : Convert Game DTO to Game POCO
        /// <summary>
        /// Converts a Game DTO to a Game POCO for database operations.
        /// </summary>
        public YMG01 PreSaveGame(DTOYMG01 dtoGame)
        {
            // Create a new instance of GAM01 POCO and map DTO values
            YMG01 game = new YMG01
            {
                G01F02 = dtoGame.G01102,  // Mapping DTO field to POCO property
                G01F03 = dtoGame.G01103   // Mapping DTO field to POCO property
            };
            // common method 

            return game;  // Return the mapped POCO object
        }

        // ValidateOnSaveGame(): Validates game POCO before saving
        /// <summary>
        /// Validates the Game POCO to ensure it is valid before saving to the database.
        /// </summary>
        public (bool IsValid, string Message) ValidateOnSaveGame(YMG01 game)
        {
            // move to dto basic validation

            // internal db check : like check for id is exists or not 

            // Check if the game name is empty
            if (string.IsNullOrEmpty(game.G01F02))
                return (false, "Game name cannot be empty.");

            // Check if the number of players is greater than zero
            if (game.G01F03 <= 0)
                return (false, "Number of player in team must be greater than zero.");

            // If both conditions are satisfied, return success message
            return (true, "Game data validation passed.");
        }

        // Save(): Save Game POCO to the Database
        /// <summary>
        /// Saves the Game POCO to the database and returns a response with the result.
        /// </summary>
        public Response SaveGame(YMG01 game)
        {
            Response response = new Response();

            try
            {
                // Open a database connection and start a transaction
                using (var db = DBConnection.OpenConnection())
                //using (var trans = db.OpenTransaction())
                {
                    // Save the game, if it exists it will update, otherwise insert a new record
                    db.Save(game);
                    //trans.Commit();  // Commit the transaction to save changes
                    // why transaction use: 2 table dependency
                }

                // Set the success message in the response
                response.Message = $"Successfully saved game {game.G01F02}.";
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
        public YMG01 preDeleteGame(int id)
        {
            using (var db = DBConnection.OpenConnection())
            {
                // Fetch the game record by its ID
                return db.SingleById<YMG01>(id);
            }
        }

        // ValidateOnDeleteGame(): Validate game record before deletion
        /// <summary>
        /// Validates if the Game record can be deleted.
        /// </summary>
        public (bool IsValid, string Message) ValidateOnDeleteGame(YMG01 game)
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
                using (var db = DBConnection.OpenConnection())
                //using (var trans = db.OpenTransaction())
                {
                    // Fetch the game record by ID
                    var game = db.SingleById<YMG01>(id); //--> global var --> into predelete // 
                    if (game != null)
                    {
                        // If game exists, delete it from the database
                        db.Delete(game);
                        //trans.Commit();  // Commit the transaction to save changes
                        // Set the success message
                        response.Message = $"Game {game.G01F02} successfully deleted.";
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