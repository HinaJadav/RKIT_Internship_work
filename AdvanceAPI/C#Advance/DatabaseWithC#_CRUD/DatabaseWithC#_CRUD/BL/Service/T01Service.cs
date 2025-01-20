using DatabaseWithC__CRUD.DB;
using DatabaseWithC__CRUD.Models.DTO;
using DatabaseWithC__CRUD.Models.POCO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace DatabaseWithC__CRUD.BL.Service
{
    /// <summary>
    /// Provides methods for managing teams (Add, Read, Update, Delete).
    /// </summary>
    public class T01Service
    {
        private readonly DBConnection _dbConnection;

        /// <summary>
        /// Initializes a new instance of the T01Service class.
        /// </summary>
        public T01Service()
        {
            _dbConnection = new DBConnection();
        }

        // Create 

        /// <summary>
        /// Adds a new team to the database.
        /// </summary>
        /// <param name="dtoymt01">The DTO containing team data to be added.</param>
        /// <returns>True if the team was added successfully; otherwise, false.</returns>
        public bool AddTeam(DTOYMT01 dtoymt01)
        {
            try
            {
                // Convert to POCO
                YMT01 pocoymt01 = DTOToPOCOConversion.dtoToPocoConvert(dtoymt01);

                // Insert query for POCO into database
                string query = "INSERT INTO YMT01 (T01F01, T01F02, T01F03, T01F04) VALUES (@T01F01, @T01F02, @T01F03, @T01F04);";

                // Define local function for setting up parameters
                void QueryParameters(MySqlCommand cmd)
                {
                    cmd.Parameters.AddWithValue("@T01F01", pocoymt01.T01F01);
                    cmd.Parameters.AddWithValue("@T01F02", pocoymt01.T01F02);
                    cmd.Parameters.AddWithValue("@T01F03", pocoymt01.T01F03);
                    cmd.Parameters.AddWithValue("@T01F04", pocoymt01.T01F04);
                }

                // execute query
                _dbConnection.ExecuteNonQuery(query, QueryParameters);

                return true;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed (optional)
                throw new Exception("Error retrieving teams: " + ex.Message);
            }

        }

        // Read 

        /// <summary>
        /// Retrieves all teams from the database.
        /// </summary>
        /// <returns>A list of all teams.</returns>
        public List<YMT01> GetAllTeams()
        {
            try
            {
                string query = "SELECT * FROM YMT01;";

                List<YMT01> ymt01DataList = new List<YMT01>();

                using (MySqlDataReader reader = _dbConnection.ExecuteReader(query, null))
                {
                    while (reader.Read())
                    {
                        ymt01DataList.Add(new YMT01
                        {
                            T01F01 = (int)reader["T01F01"],
                            T01F02 = (string)reader["T01F02"],
                            T01F03 = (int)reader["T01F03"],
                            T01F04 = (string)reader["T01F04"]
                        });
                    }
                }

                return ymt01DataList;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed (optional)
                throw new Exception("Error retrieving teams: " + ex.Message);
            }
        }

        /// <summary>
        /// Retrieves a team by its ID from the database.
        /// </summary>
        /// <param name="teamId">The ID of the team to retrieve.</param>
        /// <returns>The team corresponding to the provided ID.</returns>
        public YMT01 GetTeamById(int teamId)
        {
            try
            {
                string query = "SELECT * FROM YMT01 WHERE T01F01 = @T01F01;";

                YMT01 ymt01DataObject = new YMT01();

                using (MySqlDataReader reader = _dbConnection.ExecuteReader(query, cmd =>
                {
                    cmd.Parameters.AddWithValue("@T01F01", teamId); // Set the query parameter
                }))
                {
                    if (reader.Read())
                    {
                        ymt01DataObject.T01F01 = (int)reader["T01F01"];
                        ymt01DataObject.T01F02 = (string)reader["T01F02"];
                        ymt01DataObject.T01F03 = (int)reader["T01F03"];
                        ymt01DataObject.T01F04 = (string)reader["T01F04"];
                    }
                    else
                    {
                        throw new Exception("Team not found.");
                    }
                }

                return ymt01DataObject;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving team by ID: " + ex.Message);
            }
        }


        // Update 

        /// <summary>
        /// Updates an existing team in the database.
        /// </summary>
        /// <param name="dtoymt01">The DTO containing updated team data.</param>
        /// <returns>True if the team was updated successfully; otherwise, false.</returns>
        public bool UpdateTeam(DTOYMT01 dtoymt01)
        {
            try
            {
                // Convert to POCO
                YMT01 pocoymt01 = DTOToPOCOConversion.dtoToPocoConvert(dtoymt01);

                // Update query for update POCO into database
                string query = "UPDATE YMT01 SET T01F02 = @T01F02, T01F03 = @T01F03, T01F04 = @T01F04 WHERE T01F01 = @T01F01;";

                // Define local function for setting up parameters
                void QueryParameters(MySqlCommand cmd)
                {
                    cmd.Parameters.AddWithValue("@T01F01", pocoymt01.T01F01);
                    cmd.Parameters.AddWithValue("@T01F02", pocoymt01.T01F02);
                    cmd.Parameters.AddWithValue("@T01F03", pocoymt01.T01F03);
                    cmd.Parameters.AddWithValue("@T01F04", pocoymt01.T01F04);
                }

                // execute update query
                _dbConnection.ExecuteNonQuery(query, QueryParameters);

                return true;
            }
            catch (Exception ex)
            {
                // handle the exception part
                throw new System.Exception($"Error updating team: {ex.Message}");
            }
        }

        // Delete

        /// <summary>
        /// Deletes a team from the database.
        /// </summary>
        /// <param name="teamId">The ID of the team to delete.</param>
        /// <returns>True if the team was deleted successfully; otherwise, false.</returns>
        public bool DeleteTeam(int teamId)
        {
            try
            {
                string query = "DELETE FROM YMT01 WHERE T01F01 = @T01F01;";

                _dbConnection.ExecuteNonQuery(query, cmd =>
                {
                    cmd.Parameters.AddWithValue("@T01F01", teamId);
                });
                return true;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed (optional)
                throw new Exception("Error deleting team: " + ex.Message);
            }
        }
    }
}
