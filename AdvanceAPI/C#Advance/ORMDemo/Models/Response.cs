namespace ORMDemo.Models
{
    public class Response
    {
        /// <summary>
        /// A message indicating the result of the operation (e.g., success or error).
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// A flag indicating if the operation resulted in an error.
        /// </summary>
        public bool IsError { get; set; } = false;
    }
}