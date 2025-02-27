using FinalDemo.Models.Enums;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FinalDemo.Models.DTOs
{
    /// <summary>
    /// DTO for updating the status of a bug.
    /// </summary>
    public class DTOBugStatusUpdateRequest
    {
        /// <summary>
        /// The new status of the bug.
        /// </summary>
        [Required(ErrorMessage = "Status is required.")]
        [JsonProperty("newStatus")]
        public BugStatus NewStatus { get; set; }
    }
}
