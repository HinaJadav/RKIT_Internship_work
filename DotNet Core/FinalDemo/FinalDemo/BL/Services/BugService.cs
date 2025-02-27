using FinalDemo.BL.Interfaces;
using FinalDemo.Extension;
using FinalDemo.Models;
using FinalDemo.Models.DTOs;
using FinalDemo.Models.Enums;
using FinalDemo.Models.POCOs;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Data;

namespace FinalDemo.BL.Services
{
    public class BugService : IBugService
    {
        private readonly IDbConnectionFactory _dbFactory;
        private readonly IUserService _userService;
        private readonly ILogger<BugService> _logger;
        private YMB01 _bugObj;
        private int _bugId;
        private Response _response;
        public OperationType Type { get; set; }

        public BugService(IDbConnectionFactory dbFactory, IUserService userService, ILogger<BugService> logger)
        {
            _dbFactory = dbFactory;
            _userService = userService;
            _logger = logger;
            _response = new Response();
        }

        /// <summary>
        /// Prepares the bug object for saving or updating based on the provided data.
        /// </summary>
        public void PreSaveBug(DTOBugCreated bugDto, int? bugId = null)
        {
            if (bugId.HasValue)
            {
                _bugId = bugId.Value;
                using IDbConnection db = _dbFactory.Open();
                YMB01 existingBug = db.SingleById<YMB01>(_bugId);

                if (existingBug == null)
                    throw new KeyNotFoundException("Bug not found.");

                _bugObj = existingBug;
                _bugObj.B01F02 = bugDto.B01102 ?? _bugObj.B01F02;
                _bugObj.B01F03 = bugDto.B01103 ?? _bugObj.B01F03;
                _bugObj.B01F04 = bugDto.B01104 ?? _bugObj.B01F04;
            }
            else
            {
                _bugObj = bugDto.ToPoco<YMB01>();
                _bugObj.B01F05 = DateTime.UtcNow;
            }
        }

        /// <summary>
        /// Validates the bug data based on the current operation type (Add, Edit, Delete).
        /// </summary>
        public Response ValidateBug()
        {
            if (Type == OperationType.E || Type == OperationType.D)
            {
                using IDbConnection db = _dbFactory.Open();
                if (!db.Exists<YMB01>(_bugId))
                {
                    _response.IsError = true;
                    _response.Message = "Bug does not exist.";
                }
            }
            return _response;
        }

        /// <summary>
        /// Saves the bug data based on the operation type (Add, Edit, Delete).
        /// </summary>
        public Response SaveBug()
        {
            try
            {
                using IDbConnection db = _dbFactory.Open();
                if (Type == OperationType.A)
                {
                    db.Insert(_bugObj);
                    _response.Message = "Bug created successfully.";
                }
                else if (Type == OperationType.E)
                {
                    db.Update(_bugObj);
                    _response.Message = "Bug updated successfully.";
                }
                else if (Type == OperationType.D)
                {
                    db.DeleteById<YMB01>(_bugId);
                    _response.Message = "Bug deleted successfully.";
                }
            }
            catch (Exception ex)
            {
                _response.IsError = true;
                _response.Message = ex.Message;
            }
            return _response;
        }

        /// <summary>
        /// Retrieves a bug by its ID.
        /// </summary>
        public Response GetBugById(int bugId, int userId)
        {
            using IDbConnection db = _dbFactory.Open();
            YMB01 bug = db.SingleById<YMB01>(bugId);
            if (bug == null)
            {
                _response.IsError = true;
                _response.Message = "Bug not found.";
            }
            else
            {
                _response.Data = bug.ToDto<DTOBugResponse>();
                _response.Message = "Bug retrieved successfully.";
            }
            return _response;
        }

        /// <summary>
        /// Retrieves all bugs, depending on user permissions.
        /// </summary>
        public Response GetAllBugs(int userId)
        {
            using IDbConnection db = _dbFactory.Open();
            var bugs = db.Select<YMB01>().Select(b => b.ToDto<DTOBugResponse>());
            _response.Data = bugs;
            _response.Message = "Bugs retrieved successfully.";
            return _response;
        }

        /// <summary>
        /// Deletes a bug based on its ID, only accessible by Admin.
        /// </summary>
        public Response DeleteBug(int bugId, int userId, string role)
        {
            if (role != "Admin")
            {
                return new Response { IsError = true, Message = "Only Admin can delete a bug." };
            }

            using IDbConnection db = _dbFactory.Open();
            YMB01 bug = db.SingleById<YMB01>(bugId);
            if (bug == null)
            {
                return new Response { IsError = true, Message = "Bug not found." };
            }

            db.DeleteById<YMB01>(bugId);
            return new Response { Message = "Bug deleted successfully." };
        }

        /// <summary>
        /// Updates the status of a bug based on role (Admin or Developer).
        /// </summary>
        public Response UpdateBugStatus(int bugId, BugStatus newStatus, string role)
        {
            if (role != "Admin" && role != "Developer")
            {
                return new Response { IsError = true, Message = "Only Admin and Developer can update the bug status." };
            }

            using IDbConnection db = _dbFactory.Open();
            YMB01 bug = db.SingleById<YMB01>(bugId);
            if (bug == null)
            {
                return new Response { IsError = true, Message = "Bug not found." };
            }

            bug.B01F04 = newStatus;
            db.Update(bug);

            return new Response { Message = "Bug status updated successfully." };
        }
    }
}
