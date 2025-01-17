using MySql.Data.MySqlClient;
using System;
using System.Web;

namespace DatabaseWithC__CRUD.DB
{
    public class DBConnection
    {
        private string _connectionString;

        public DBConnection()
        {
            _connectionString = HttpContext.Current.Application["ConnectionString"] as string;
        }

        // method to create a new connection
        public MySqlConnection CreateConnection()
        {
            return new MySqlConnection(_connectionString);
        }

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

        // ExecuteNonQuery method : It is used to execute SQL statements that do not return any data(like INSERT, UPDATE, DELETE)

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