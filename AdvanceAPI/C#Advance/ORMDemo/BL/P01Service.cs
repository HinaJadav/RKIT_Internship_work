using ORMDemo.DB;
using ORMDemo.Models;
using ORMDemo.Models.DTO;
using ORMDemo.Models.POCO;
using ServiceStack.OrmLite;
using System;

namespace ORMDemo.BL
{
    public class P01Service
    {
        // PreSave() : It converts player DTO to player POCO

        /// <summary>
        /// Converts the Player DTO (Data Transfer Object) to Player POCO (Plain Old CLR Object).
        /// </summary>
        public YMP01 PreSavePlayer(DTOYMP01 dtoPlayer)
        {
            // Mapping DTO properties to POCO properties
            YMP01 player = new YMP01
            {
                P01F02 = dtoPlayer.P01102,  // Player Name
                P01F03 = dtoPlayer.P01103,  // Player Email
                P01F04 = dtoPlayer.P01104,  // Team Name
                P01F05 = dtoPlayer.P01105,  // Game ID
                P01F06 = DateTime.Now      // Current Timestamp
            };

            return player;  // Return the POCO object
        }

        // ValidateOnSave() : Validates player POCO before saving

        /// <summary>
        /// Validates the Player POCO before saving to the database.
        /// </summary>
        public (bool IsValid, string Message) ValidateOnSavePlayer(YMP01 player)
        {
            // Check if the Player Name is empty
            if (string.IsNullOrEmpty(player.P01F02)) return (false, "Name cannot be empty.");

            // Check if the Player Email is empty
            if (string.IsNullOrEmpty(player.P01F03)) return (false, "Email cannot be empty.");

            // Check if the Team Name is empty
            if (string.IsNullOrEmpty(player.P01F04)) return (false, "Team name cannot be empty.");

            // Check if the Game ID is zero or invalid
            if (player.P01F05 == 0) return (false, "Invalid game id.");

            // If all validations pass
            return (true, "Player validation passed.");
        }

        // Save(): Save player POCO to the Database and return response

        /// <summary>
        /// Saves the Player POCO to the database and returns a response.
        /// </summary>
        public Response SavePlayer(YMP01 player)
        {
            Response response = new Response();

            try
            {
                // Open a database connection and start a transaction
                using (var db = DBConnection.OpenConnection())
                using (var trans = db.OpenTransaction())
                {
                    // Save the player, it will update if the record exists or insert a new one
                    db.Save(player);
                    trans.Commit();  // Commit the transaction to save changes
                }

                // Set the success message in the response
                response.Message = $"Successfully saved player {player.P01F02}.";
            }
            catch (Exception ex)
            {
                // In case of an error, set IsError flag and message
                response.IsError = true;
                response.Message = $"Error: {ex.Message}";
            }

            return response;  // Return the response object with result
        }

        // PreDelete() : Prepare player record for deletion

        /// <summary>
        /// Fetches the Player record before deletion.
        /// </summary>
        public YMP01 PreDeletePlayer(int id)
        {
            using (var db = DBConnection.OpenConnection())
            {
                // Fetch the player record by its ID
                return db.SingleById<YMP01>(id);
            }
        }

        // ValidateOnDelete() : Validate player record before deletion

        /// <summary>
        /// Validates if the Player record can be deleted.
        /// </summary>
        public (bool IsValid, string Message) ValidateOnDeletePlayer(YMP01 player)
        {
            // If the player is null, it means it can't be deleted
            if (player == null)
            {
                return (false, "Player not found.");
            }

            // Otherwise, the player can be deleted
            return (true, "Player can be deleted.");
        }

        // Delete() : Delete player from the database

        /// <summary>
        /// Deletes a Player from the database and returns a response indicating success or failure.
        /// </summary>
        public Response DeletePlayer(int id)
        {
            Response response = new Response();

            try
            {
                // Open a database connection and start a transaction
                using (var db = DBConnection.OpenConnection())
                using (var trans = db.OpenTransaction())
                {
                    // Fetch the player record by ID
                    YMP01 player = db.SingleById<YMP01>(id);
                    if (player != null)
                    {
                        // If the player exists, delete it from the database
                        db.Delete(player);
                        trans.Commit();  // Commit the transaction to save changes
                        response.Message = $"Player {id} successfully deleted.";
                    }
                    else
                    {
                        // If the player is not found, set an error message
                        response.IsError = true;
                        response.Message = "Player not found.";
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