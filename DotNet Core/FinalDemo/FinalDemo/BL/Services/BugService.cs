using FinalDemo.BL.Interfaces;
using FinalDemo.Extension;
using FinalDemo.Models;
using FinalDemo.Models.DTOs;
using FinalDemo.Models.Enums;
using FinalDemo.Models.POCOs;
using ServiceStack.OrmLite;
using System.Data;

namespace FinalDemo.BL.Services
{
    /// <summary>
    /// Service class for managing bug operations such as creation, retrieval, update, and deletion.
    /// </summary>
    public class BugService : IBugService
    {
        private readonly IDbConnection _db;
        private readonly ILogger<UserService> _logger;
        private YMB01 _bugObj;
        private int _bugId;
        private Response _response;
        public OperationType Type { get; set; }

        /// <summary>
        /// Initializes a new instance of the BugService class.
        /// </summary>
        public BugService(IDbConnection db, ILogger<UserService> logger)
        {
            _db = db;
            _logger = logger;
            _response = new Response();
        }

        /// <summary>
        /// Converts a DTO bug object to a POCO model.
        /// </summary>
        private YMB01 ToPocoBug(DTOYMB01 bugDto)
        {
            return new YMB01
            {
                B01F01 = bugDto.B01101,
                B01F02 = bugDto.B01102 ?? string.Empty,
                B01F03 = bugDto.B01103 ?? string.Empty,
                B01F04 = bugDto.B01104,
                B01F05 = DateTime.UtcNow
            };
        }

        /// <summary>
        /// Converts a bug POCO model to a DTO response.
        /// </summary>
        private DTOBugResponse ToDTOBug(YMB01 bug)
        {
            return new DTOBugResponse
            {
                B01101 = bug.B01F01,
                B01102 = bug.B01F02,
                B01103 = bug.B01F03,
                B01104 = bug.B01F04,
                B01105 = bug.B01F05,
                B01107 = bug.B01F07,
            };
        }

        /// <summary>
        /// Prepares a bug object for creation or update operations.
        /// </summary>
        public void PreSaveBug(DTOYMB01 bugDto, OperationType operationType)
        {
            Type = operationType;
            Console.WriteLine($"Received Bug DTO: {Newtonsoft.Json.JsonConvert.SerializeObject(bugDto)}");

            int bugId = bugDto.B01101;
            if (Type == OperationType.E)
            {
                _bugId = bugId;
                YMB01 existingBug = _db.SingleById<YMB01>(_bugId);

                if (existingBug == null)
                {
                    throw new KeyNotFoundException($"Bug with ID {_bugId} not found.");
                }

                _bugObj = existingBug;
                _bugObj.B01F02 = bugDto.B01102 ?? _bugObj.B01F02;
                _bugObj.B01F03 = bugDto.B01103 ?? _bugObj.B01F03;
                _bugObj.B01F04 = bugDto.B01104;
            }
            else if (Type == OperationType.A)
            {
                YMB01 newBug = ToPocoBug(bugDto);
                if (newBug == null)
                {
                    throw new ArgumentException("Conversion to POCO failed.");
                }
                _bugObj = newBug;
                _bugObj.B01F05 = DateTime.UtcNow;
            }
        }

        /// <summary>
        /// Validates if a bug exists before performing update or delete operations.
        /// </summary>
        public Response ValidateBug()
        {
            if (Type == OperationType.E || Type == OperationType.D)
            {
                if (!_db.Exists<YMB01>(_bugId))
                {
                    _response.IsError = true;
                    _response.Message = "Bug does not exist.";
                }
            }
            return _response;
        }

        /// <summary>
        /// Checks whether a bug exists in the database.
        /// </summary>
        private bool IsBugExist(int bugId)
        {
            return _db.Count<YMU01>(x => x.U01F01 == bugId) > 0;
        }

        /// <summary>
        /// Saves a bug to the database.
        /// </summary>
        public Response SaveBug()
        {
            try
            {
                if (_bugObj == null)
                {
                    _response.IsError = true;
                    _response.Message = "Bug object is null before save.";
                    return _response;
                }

                if (Type == OperationType.A)
                {
                    _db.Insert(_bugObj);
                    _response.Message = "Bug created successfully.";
                    _response.Data = _bugObj;
                }
                else if (Type == OperationType.E)
                {
                    if (!IsBugExist(_bugId))
                    {
                        _response.IsError = true;
                        _response.Message = "Bug not found.";
                        return _response;
                    }
                    _db.Update(_bugObj);
                    _response.Message = "Bug details updated successfully.";
                }
            }
            catch (Exception ex)
            {
                _response.IsError = true;
                _response.Message = $"Error: {ex.Message}";
            }
            return _response;
        }

        /// <summary>
        /// Retrieves a bug by its ID.
        /// </summary>
        public DTOBugResponse GetBugById(int bugId, int userId)
        {
            YMB01 bug = _db.SingleById<YMB01>(bugId);
            return bug == null ? null : ToDTOBug(bug);
        }

        /// <summary>
        /// Retrieves all bugs from the database.
        /// </summary>
        public Response GetAllBugs(int userId)
        {
            _response.Data = _db.Select<YMB01>();
            _response.Message = "Bugs retrieved successfully.";
            return _response;
        }

        /// <summary>
        /// Deletes a bug from the database.
        /// </summary>
        public Response DeleteBug(int bugId, int userId, string role)
        {
            if (role != "Admin")
            {
                _response.IsError = true;
                _response.Message = "Only Admin can delete a bug.";
            }
            _db.DeleteById<YMB01>(bugId);
            _response.Message = "Bug deleted successfully.";
            return _response;
        }

        /// <summary>
        /// Updates the status of a bug.
        /// </summary>
        public Response UpdateBugStatus(int bugId, BugStatus newStatus, string role)
        {
            if (role != "Admin" && role != "Developer")
            {
                _response.IsError = true;
                _response.Message = "Only Admin and Developer can update the bug status.";
            }
            YMB01 bug = _db.SingleById<YMB01>(bugId);
            bug.B01F04 = newStatus;
            _db.Update(bug);
            _response.Message = "Bug status updated successfully.";
            return _response;
        }
    }
}
