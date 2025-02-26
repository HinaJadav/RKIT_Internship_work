using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FinalDemo.Models.DTOs
{
    public class DTOBugUpdated
    {
        /// <summary>
        /// Unique Identifier for the bug
        /// </summary>
        [Required(ErrorMessage = "B01F01 (Bug ID) is required.")]
        [JsonProperty("B01F01")]
        public int B01101 { get; set; }

        /// <summary>
        /// Status of the issue: Open, In Progress, Resolved, Closed
        /// </summary>
        [Required(ErrorMessage = "B01F04 (Status) is required.")]
        [JsonProperty("B01F04")]
        public string B01104 { get; set; }
    }
}
