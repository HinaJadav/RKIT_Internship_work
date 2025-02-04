using ORMDemo.Models.POCO;
using ServiceStack.OrmLite;
using System;
using System.Configuration;
using System.Data;

namespace ORMDemo.DB
{
    public class DBConnection
    {
        /// <summary>
        /// Stores the actual database connection.
        /// </summary>
        public static IDbConnection _db;

        /// <summary>
        /// Opens a connection to the database and creates necessary tables if they do not exist.
        /// </summary>
        /// <returns>
        /// Returns the established database connection.
        /// </returns>
        public static IDbConnection OpenConnection()
        {
            try
            {
                // Fetch the connection string from the configuration file (App.Config)
                string connectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;

                // Create a new ORM Lite connection factory with the provided connection string and MySQL dialect
                var dbFactory = new OrmLiteConnectionFactory(connectionString, MySqlDialect.Provider);

                // Open a connection using the factory
                _db = dbFactory.Open();

                // Create the tables if they do not exist
                CreateTables();

                // Return the established connection
                return _db;
            }
            catch (Exception ex)
            {
                // Log the error and rethrow the exception to halt execution
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
                // Create the GAM01 table if it does not exist
                _db.CreateTableIfNotExists<YMG01>();

                // Create the PLA01 table if it does not exist
                _db.CreateTableIfNotExists<YMP01>();
            }
            catch (Exception ex)
            {
                // Log the error and rethrow the exception to halt execution
                Console.WriteLine($"Error creating tables: {ex.Message}");
                throw;
            }
        }
    }
}
