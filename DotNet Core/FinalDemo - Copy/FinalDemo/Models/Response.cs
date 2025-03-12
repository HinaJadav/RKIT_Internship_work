using System.Data;

namespace FinalDemo.Models
{
    public class Response
    {
        
        /// <summary>
        /// Indicates whether an error occurred during the operation.
        /// </summary>
        public bool IsError { get; set; } = false;

        /// <summary>
        /// Gets or sets a message providing details about the operation's result or error.
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Holds additional data related to the response.
        /// </summary>
        public object? Data { get; set; }
    }
}