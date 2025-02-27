using FinalDemo.BL.Interfaces;
using FinalDemo.Extension;
using FinalDemo.Models.DTOs;
using FinalDemo.Models.POCOs;
using ServiceStack.OrmLite;
using ServiceStack.Data;
using System.Data;

namespace FinalDemo.BL.Services
{
    /// <summary>
    /// Service for managing bugs with role-based access control.
    /// </summary>
    public class BugService : IBugService
    {
        private readonly IDbConnectionFactory _dbFactory;
        private readonly IUserService _userService;
        private readonly ILogger<BugService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BugService"/> class.
        /// </summary>
        public BugService(IDbConnectionFactory dbFactory, IUserService userService, ILogger<BugService> logger)
        {
            _dbFactory = dbFactory;
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new bug.
        /// </summary>
        public DTOBugResponse CreateBug(DTOBugCreated bugDto, int userId)
        {
            var user = _userService.GetUserById(userId);
            if (user == null)
                throw new UnauthorizedAccessException("User is not authenticated.");

            var newBug = bugDto.ToPoco<YMB01>();
            newBug.B01F05 = DateTime.UtcNow;
            newBug.B01F06 = user.U01101; // Assigning to user who created it

            using var db = _dbFactory.OpenDbConnection();
            newBug.B01F01 = (int)db.Insert(newBug, selectIdentity: true);

            _logger.LogInformation("Bug {BugId} created successfully by user {UserId}.", newBug.B01F01, userId);
            return newBug.ToDto<DTOBugResponse>();
        }

        /// <summary>
        /// Updates the status of an existing bug.
        /// </summary>
        public DTOBugResponse UpdateBugStatus(DTOBugUpdated bugDto, int userId)
        {
            var user = _userService.GetUserById(userId);
            if (user == null)
                throw new UnauthorizedAccessException("User is not authenticated.");
            if (user.U01104 != "Admin" && user.U01104 != "Developer")
                throw new UnauthorizedAccessException("You do not have permission to update bug status.");

            using var db = _dbFactory.OpenDbConnection();
            var bug = db.SingleById<YMB01>(bugDto.B01101);
            if (bug == null)
                throw new KeyNotFoundException("Bug not found.");

            bug.B01F04 = bugDto.B01104;
            db.Update(bug);

            _logger.LogInformation("Bug {BugId} updated by user {UserId}.", bug.B01F01, userId);
            return bug.ToDto<DTOBugResponse>();
        }

        /// <summary>
        /// Retrieves a bug by its ID.
        /// </summary>
        public DTOBugResponse GetBugById(int bugId, int userId)
        {
            var user = _userService.GetUserById(userId);
            if (user == null)
                throw new UnauthorizedAccessException("User is not authenticated.");

            using var db = _dbFactory.OpenDbConnection();
            var bug = db.SingleById<YMB01>(bugId);
            if (bug == null)
                throw new KeyNotFoundException("Bug not found.");

            if (user.U01104 != "Admin" && bug.B01F06 != user.U01101)
                throw new UnauthorizedAccessException("You do not have permission to view this bug.");

            return bug.ToDto<DTOBugResponse>();
        }

        /// <summary>
        /// Deletes a bug (Admin only).
        /// </summary>
        public void DeleteBug(int bugId, int userId)
        {
            var user = _userService.GetUserById(userId);
            if (user == null)
                throw new UnauthorizedAccessException("User is not authenticated.");
            if (user.U01104 != "Admin")
                throw new UnauthorizedAccessException("You do not have permission to delete bugs.");

            using var db = _dbFactory.OpenDbConnection();
            var bug = db.SingleById<YMB01>(bugId);
            if (bug == null)
                throw new KeyNotFoundException("Bug not found.");

            db.DeleteById<YMB01>(bugId);
            _logger.LogInformation("Bug {BugId} deleted by admin {UserId}.", bugId, userId);
        }

        /// <summary>
        /// Retrieves all bugs (Admin sees all, others see only their own).
        /// </summary>
        public IEnumerable<DTOBugResponse> GetAllBugs(int userId)
        {
            var user = _userService.GetUserById(userId);
            if (user == null)
                throw new UnauthorizedAccessException("User is not authenticated.");

            using var db = _dbFactory.OpenDbConnection();
            var bugs = user.U01104 == "Admin"
                ? db.Select<YMB01>()
                : db.Select<YMB01>(b => b.B01F06 == user.U01101);

            return bugs.Select(b => b.ToDto<DTOBugResponse>());
        }
    }
}
// presave frmate
