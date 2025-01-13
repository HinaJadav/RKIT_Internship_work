using System.Data;

namespace ORM
{
    public class Response
    {
        // Holds the data that is returned from the operation (e.g., DataTable, Model)
        public DataTable Data { get; set; }

        // A message to indicate the result of the operation (e.g., success or error)
        public string Message { get; set; }

        // A flag to indicate whether the operation resulted in an error
        public bool IsError { get; set; } = false;
    }
}
