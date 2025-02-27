using FinalDemo.BL.Interfaces;
using FinalDemo.Filter;
using FinalDemo.Models;
using FinalDemo.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

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
        private Response _response;

        public BugController(IBugService bugService, ILogger<BugController> logger)
        {
            _bugService = bugService;
            _logger = logger;
            _response = new Response();
        }

        /// <summary>
        /// Creates or updates a bug.
        /// This endpoint is responsible for saving a new bug or updating an existing one based on the provided bug ID.
        /// </summary>
        [HttpPost]
        public IActionResult SaveBug([FromBody] DTOBugCreated bugDto, int? bugId = null)
        {
            int userId = (int)HttpContext.Items["UserId"];
            _bugService.PreSaveBug(bugDto, bugId);
            Response validationResponse = _bugService.ValidateBug();
            if (validationResponse.IsError)
                return BadRequest(validationResponse);

            _response = _bugService.SaveBug();
            return _response.IsError ? BadRequest(_response) : Ok(_response);
        }

        /// <summary>
        /// Retrieves a bug by its ID.
        /// Admin users can see all bugs, while other users can only see their own bugs.
        /// </summary>
        [HttpGet("{bugId}")]
        public IActionResult GetBugById(int bugId)
        {
            _logger.LogInformation("Fetching bug details for Bug ID: {BugId}", bugId);
            int userId = (int)HttpContext.Items["UserId"];
            _response = _bugService.GetBugById(bugId, userId);
            return _response.IsError ? NotFound(_response) : Ok(_response);
        }

        /// <summary>
        /// Deletes a bug.
        /// Only Admin users are authorized to delete bugs.
        /// </summary>
        [HttpDelete("{bugId}")]
        public IActionResult DeleteBug(int bugId)
        {
            int userId = (int)HttpContext.Items["UserId"];
            string userRole = (string)HttpContext.Items["Role"];
            string role = HttpContext.Items["Role"] as string;

            if (role != "Admin")
            {
                _logger.LogWarning("Unauthorized delete attempt on Bug {BugId} by user {UserId}", bugId, userId);
                return Unauthorized(new { Message = "Only Admins can delete bugs." });
            }

            _logger.LogInformation("User {UserId} deleting bug {BugId}", userId, bugId);
            _response = _bugService.DeleteBug(bugId, userId, userRole);
            return _response.IsError ? NotFound(_response) : NoContent();
        }

        /// <summary>
        /// Retrieves all bugs.
        /// Admin users can see all bugs, while other users can only see their own bugs.
        /// </summary>
        [HttpGet]
        public IActionResult GetAllBugs()
        {
            _logger.LogInformation("Fetching all bugs for the authenticated user.");
            int userId = (int)HttpContext.Items["UserId"];
            _response = _bugService.GetAllBugs(userId);
            return _response.IsError ? NotFound(_response) : Ok(_response);
        }

        /// <summary>
        /// Updates the status of a bug.
        /// Only Admin and Developer roles are authorized to update the status of a bug.
        /// </summary>
        [HttpPut("{bugId}/status")]
        public IActionResult UpdateBugStatus(int bugId, [FromBody] DTOBugStatusUpdateRequest request)
        {
            int userId = (int)HttpContext.Items["UserId"];
            string role = HttpContext.Items["Role"] as string;

            if (role != "Admin" && role != "Developer")
            {
                _logger.LogWarning("Unauthorized status update attempt on Bug {BugId} by user {UserId}", bugId, userId);
                return Unauthorized(new { Message = "Only Admins and Developers can update bug status." });
            }

            _logger.LogInformation("User {UserId} updating status of bug {BugId} to {Status}", userId, bugId, request.NewStatus);
            _response = _bugService.UpdateBugStatus(bugId, request.NewStatus, role);
            return _response.IsError ? BadRequest(_response) : Ok(_response);
        }
    }
}
