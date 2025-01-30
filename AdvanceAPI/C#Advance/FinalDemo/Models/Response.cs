using System.Data;

namespace FinalDemo.Models
{
    public class Response
    {
        /// <summary>
        /// Gets or sets the data returned by the operation in a DataTable format.
        /// </summary>
        public DataTable Data { get; set; }

        /// <summary>
        /// Indicates whether an error occurred during the operation.
        /// </summary>
        public bool IsError { get; set; } = false;

        /// <summary>
        /// Gets or sets a message providing details about the operation's result or error.
        /// </summary>
        public string Message { get; set; }

    }
}