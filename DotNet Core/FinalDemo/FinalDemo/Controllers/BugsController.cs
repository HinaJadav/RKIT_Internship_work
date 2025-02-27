using FinalDemo.BL.Interfaces;
using FinalDemo.Models.DTOs;
using FinalDemo.Filter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FinalDemo.Controllers
{
    [ApiController]
    [Route("api/bugs")]
    [ServiceFilter(typeof(CustomAuthenticationFilter))] // Apply custom authentication filter
    [ServiceFilter(typeof(CustomExceptionFilter))] // Apply custom exception filter
    public class BugController : ControllerBase
    {
        private readonly IBugService _bugService;
        private readonly ILogger<BugController> _logger;

        public BugController(IBugService bugService, ILogger<BugController> logger)
        {
            _bugService = bugService;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new bug (Any authenticated user).
        /// </summary>
        [HttpPost]
        public IActionResult CreateBug([FromBody] DTOBugCreated bugDto)
        {
            _logger.LogInformation("Creating a new bug with title: {Title}", bugDto.B01102);
            int userId = (int)HttpContext.Items["UserId"];
            var bug = _bugService.CreateBug(bugDto, userId);
            _logger.LogInformation("Bug created successfully with ID: {BugId}", bug.B01101);
            return CreatedAtAction(nameof(GetBugById), new { bugId = bug.B01101 }, bug);
        }

        /// <summary>
        /// Updates a bug's status (Only Admin & Developer).
        /// </summary>
        [HttpPut]
        public IActionResult UpdateBugStatus([FromBody] DTOBugUpdated bugDto)
        {
            int userId = (int)HttpContext.Items["UserId"];
            var role = HttpContext.Items["Role"] as string;

            if (role != "Admin" && role != "Developer")
            {
                _logger.LogWarning("Unauthorized attempt to update bug {BugId} by user {UserId}", bugDto.B01101, userId);
                return Unauthorized(new { Message = "You do not have permission to update bugs." });
            }

            _logger.LogInformation("User {UserId} updating bug {BugId}", userId, bugDto.B01101);
            var updatedBug = _bugService.UpdateBugStatus(bugDto, userId);
            _logger.LogInformation("Bug {BugId} updated successfully", bugDto.B01101);
            return Ok(updatedBug);
        }

        /// <summary>
        /// Retrieves a bug by its ID (Admin sees all, others see their own).
        /// </summary>
        [HttpGet("{bugId}")]
        public IActionResult GetBugById(int bugId)
        {
            _logger.LogInformation("Fetching bug details for Bug ID: {BugId}", bugId);
            int userId = (int)HttpContext.Items["UserId"];
            var bug = _bugService.GetBugById(bugId, userId);
            _logger.LogInformation("Retrieved bug details for Bug ID: {BugId}", bugId);
            return Ok(bug);
        }

        /// <summary>
        /// Deletes a bug (Only Admin).
        /// </summary>
        [HttpDelete("{bugId}")]
        public IActionResult DeleteBug(int bugId)
        {
            int userId = (int)HttpContext.Items["UserId"];
            var role = HttpContext.Items["Role"] as string;

            if (role != "Admin")
            {
                _logger.LogWarning("Unauthorized delete attempt on Bug {BugId} by user {UserId}", bugId, userId);
                return Unauthorized(new { Message = "Only Admins can delete bugs." });
            }

            _logger.LogInformation("User {UserId} deleting bug {BugId}", userId, bugId);
            _bugService.DeleteBug(bugId, userId);
            _logger.LogInformation("Bug {BugId} deleted successfully", bugId);
            return NoContent();
        }

        /// <summary>
        /// Retrieves all bugs (Admin sees all, others see their own).
        /// </summary>
        [HttpGet]
        public IActionResult GetAllBugs()
        {
            _logger.LogInformation("Fetching all bugs for the authenticated user.");
            int userId = (int)HttpContext.Items["UserId"];
            var bugs = _bugService.GetAllBugs(userId);
            _logger.LogInformation("Retrieved {Count} bugs.", bugs.Count());
            return Ok(bugs);
        }
    }
}
