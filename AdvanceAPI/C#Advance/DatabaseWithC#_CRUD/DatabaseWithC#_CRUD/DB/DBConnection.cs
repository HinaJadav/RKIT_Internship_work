using MySql.Data.MySqlClient;
using System;
using System.Configuration;

namespace DatabaseWithC__CRUD.DB
{
    /// <summary>
    /// Provides methods for database connection and query execution.
    /// </summary>
    public class DBConnection
    {
        private string _connectionString;

        /// <summary>
        /// Initializes the DBConnection with the connection string from the configuration.
        /// </summary>
        public DBConnection()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        }

        /// <summary>
        /// Creates and returns a new MySqlConnection using the connection string.
        /// </summary>
        /// <returns>A new instance of MySqlConnection.</returns>
        public MySqlConnection CreateConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        /// <summary>
        /// Executes a SQL query that returns a data reader.
        /// </summary>
        /// <param name="query">The SQL query to execute.</param>
        /// <param name="queryParameters">A delegate for adding parameters to the SQL command.</param>
        /// <returns>A MySqlDataReader to read the result set.</returns>
        public MySqlDataReader ExecuteReader(string query, Action<MySqlCommand> queryParameters)
        {
            // create connection
            MySqlConnection conn = CreateConnection();

            // prepare command 

            MySqlCommand cmd = new MySqlCommand(query, conn);

            // set up parameters with the caller

            // This directly invokes the queryParameters delegate without any null checks.
            // If queryParameters == null then it will throw a "NullReferenceException"

            // queryParameters(cmd);   

            // Null-checking version -- more safer
            queryParameters?.Invoke(cmd);

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            // conn.Close(); 
            // Connection can't close here because method still required the connection open while it's reading the Data if we close here it might throw error or exception.
            // Allow the caller to manage the connection closing part, It done after using the reader.

            return reader;
        }

        /// <summary>
        /// Executes a SQL query that does not return data (e.g., INSERT, UPDATE, DELETE).
        /// </summary>
        /// <param name="query">The SQL query to execute.</param>
        /// <param name="queryParameters">A delegate for adding parameters to the SQL command.</param>
        public void ExecuteNonQuery(string query, Action<MySqlCommand> queryParameters)
        {
            // use "using" clause here 
            using (MySqlConnection conn = CreateConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    // open connection
                    conn.Open();

                    // Allow the caller to set up parameters
                    queryParameters?.Invoke(cmd);

                    // Execute non-query
                    cmd.ExecuteNonQuery();

                }
            }
        }
    }
}