using FinalDemo.Models.POCOs;
using ServiceStack.OrmLite;
using System.Data;

namespace FinalDemo.DB
{
    public class DBConnection
    {
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

                // Open a new database connection using OrmLite
                using var db = new OrmLiteConnectionFactory(connectionString, MySqlDialect.Provider).OpenDbConnection();

                // Create the tables if they do not exist
                CreateTables(db);

                // Return the established connection
                return db;
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
        private static void CreateTables(IDbConnection db)
        {
            try
            {
                db.CreateTableIfNotExists<YMU01>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating tables: {ex.Message}");
                throw;
            }
        }
    }
}