using FinalDemo.Models.POCOs;
using Microsoft.Extensions.Configuration;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.MySql;
using System;
using System.Data;

namespace FinalDemo.DB
{
    public class DBConnection
    {
        private static IDbConnection _db;
        private static OrmLiteConnectionFactory _dbFactory;

        /// <summary>
        /// Opens a connection to the database and creates necessary tables if they do not exist.
        /// </summary>
        /// <returns>
        /// Returns the established database connection.
        /// </returns>
        public static IDbConnection OpenConnection(IConfiguration configuration)
        {
            try
            {
                // Fetch the connection string from appsettings.json
                string connectionString = configuration.GetConnectionString("MyDbConnection");

                // Create a new ORM Lite connection factory
                _dbFactory = new OrmLiteConnectionFactory(connectionString, MySqlDialect.Provider);

                // Open a connection using the factory
                _db = _dbFactory.Open();

                // Create the tables if they do not exist
                CreateTables();

                // Return the established connection
                return _db;
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error opening DB connection: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Creates the necessary tables in the database if they do not already exist.
        /// </summary>
        private static void CreateTables()
        {
            try
            {
                _db.CreateTableIfNotExists<YMU01>();
                _db.CreateTableIfNotExists<YMB01>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating tables: {ex.Message}");
                throw;
            }
        }
    }
}
