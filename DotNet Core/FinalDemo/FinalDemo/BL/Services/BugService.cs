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
    public class BugService : IBugService
    {
        private readonly IDbConnection _db;
        private YMB01 _bugObj;
        private int _bugId;
        private Response _response;
        public OperationType Type { get; set; }

        public BugService(IDbConnection db)
        {
            _db = db;
            _response = new Response();
        }

        public void PreSaveBug(DTOBugCreated bugDto, int? bugId = null)
        {
            if (bugId.HasValue)
            {
                _bugId = bugId.Value;
                YMB01 existingBug = _db.SingleById<YMB01>(_bugId);

                if (existingBug == null)
                    throw new KeyNotFoundException("Bug not found.");

                _bugObj = existingBug;
                _bugObj.B01F02 = bugDto.B01102 ?? _bugObj.B01F02;
                _bugObj.B01F03 = bugDto.B01103 ?? _bugObj.B01F03;
                _bugObj.B01F04 = bugDto.B01104 ?? _bugObj.B01F04;
            }
            else
            {
                //_bugObj = bugDto.ConvertTo<YMB01>();
                _bugObj.B01F05 = DateTime.UtcNow;
            }
        }

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

        public Response SaveBug()
        {
            try
            {
                if (Type == OperationType.A)
                {
                    _db.Insert(_bugObj);
                    _response.Message = "Bug created successfully.";
                }
                else if (Type == OperationType.E)
                {
                    _db.Update(_bugObj);
                    _response.Message = "Bug updated successfully.";
                }
                else if (Type == OperationType.D)
                {
                    _db.DeleteById<YMB01>(_bugId);
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

        public Response GetBugById(int bugId, int userId)
        {
            YMB01 bug = _db.SingleById<YMB01>(bugId);
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

        public Response GetAllBugs(int userId)
        {
            var bugs = _db.Select<YMB01>().Select(b => b.ToDto<DTOBugResponse>());
            _response.Data = bugs;
            _response.Message = "Bugs retrieved successfully.";
            return _response;
        }

        public Response DeleteBug(int bugId, int userId, string role)
        {
            if (role != "Admin")
            {
                return new Response { IsError = true, Message = "Only Admin can delete a bug." };
            }

            YMB01 bug = _db.SingleById<YMB01>(bugId);
            if (bug == null)
            {
                return new Response { IsError = true, Message = "Bug not found." };
            }

            _db.DeleteById<YMB01>(bugId);
            return new Response { Message = "Bug deleted successfully." };
        }

        public Response UpdateBugStatus(int bugId, BugStatus newStatus, string role)
        {
            if (role != "Admin" && role != "Developer")
            {
                return new Response { IsError = true, Message = "Only Admin and Developer can update the bug status." };
            }

            YMB01 bug = _db.SingleById<YMB01>(bugId);
            if (bug == null)
            {
                return new Response { IsError = true, Message = "Bug not found." };
            }

            bug.B01F04 = newStatus;
            _db.Update(bug);

            return new Response { Message = "Bug status updated successfully." };
        }
    }
}
