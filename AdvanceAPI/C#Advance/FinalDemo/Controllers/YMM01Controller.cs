using FinalDemo.BL;
using FinalDemo.Models;
using FinalDemo.Models.DTO;
using FinalDemo.Models.POCO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System;
using System.Web.Http;

namespace FinalDemo.Controllers
{
    [RoutePrefix("api/member")] // Base route for the MemberController
    public class YMM01Controller : ApiController
    {
        private readonly BLYMM01 _memberService;
        private readonly BLSecurity _loginService;
        private Response _response;

        // Constructor to initialize services
        public YMM01Controller()
        {
            _memberService = new BLYMM01();
            _loginService = new BLSecurity();
            _response = new Response();
        }

        /// <summary>
        /// Handles user login requests.
        /// </summary>
        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login(DTOLogin loginDto)
        {
            _response = _loginService.Login(loginDto);
            if (_response.IsError)
                return BadRequest(_response.Message);

            return Ok(_response.Message);
        }

        /// <summary>
        /// Adds a new member to the system.
        /// </summary>
        [HttpPost]
        [Route("add")] // POST api/member/add
        public IHttpActionResult AddMember(DTOYMM01 memberDto)
        {
            _memberService.Type = OperationType.A;
            _memberService.PreSave(memberDto);
            _response = _memberService.Validation();

            if (_response.IsError)
            {
                return BadRequest(_response.Message);
            }

            _response = _memberService.Save();
            return Ok(_response);
        }

        /// <summary>
        /// Updates an existing member's information.
        /// </summary>
        [HttpPut]
        [Route("update/{id}")] // PUT api/member/update/{id}
        public IHttpActionResult UpdateMember(int id, DTOYMM01 memberDto)
        {
            _memberService.Type = OperationType.E;
            _memberService.PreSave(memberDto);
            _response = _memberService.Validation();

            if (_response.IsError)
            {
                return BadRequest(_response.Message);
            }

            _response = _memberService.Save();
            return Ok(_response);
        }

        /// <summary>
        /// Deletes an existing member from the system.
        /// </summary>
        [HttpDelete]
        [Route("delete/{id}")] // DELETE api/member/delete/{id}
        public IHttpActionResult DeleteMember(int id)
        {
            var response = _memberService.Delete(id);

            if (response.IsError)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }


        /// <summary>
        /// Downloads all member data as a JSON file.
        /// </summary>
        [HttpGet]
        [Route("download-data")]
        public IHttpActionResult DownloadData()
        {
            try
            {
                // Fetch all the data
                List<DTOYMM01> allMembers = _memberService.GetAll();

                // Serialize the data to JSON format
                string serializedData = JsonConvert.SerializeObject(allMembers);

                // Define the directory and file name
                string appDataPath = HttpContext.Current.Server.MapPath("~/App_Data");
                string fileName = "members_data_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".json";
                string filePath = Path.Combine(appDataPath, fileName);

                // Ensure the directory exists
                if (!Directory.Exists(appDataPath))
                {
                    Directory.CreateDirectory(appDataPath);
                }

                // Write the serialized data to the file
                File.WriteAllText(filePath, serializedData, Encoding.UTF8);

                // Return the URL of the file for downloading
                var fileUrl = Url.Content("~/App_Data/" + fileName);
                return Ok(new { fileUrl });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);  // Return error details
            }
        }


        /// <summary>
        /// Inserts multiple members at once.
        /// </summary>
        [HttpPost]
        [Route("insert-multiple")] // POST api/member/insert-multiple
        public IHttpActionResult InsertMultipleMembers(List<YMM01> members)
        {
             _response = additionalService.InsertAllMembers(members);
            if (_response.IsError)
                return BadRequest(_response.Message);
            return Ok(_response.Message);
        }

        /// <summary>
        /// Inserts only specific fields of a member.
        /// </summary>
        [HttpPost]
        [Route("insert-selective")] // POST api/member/insert-selective
        public IHttpActionResult InsertSelectiveFields(YMM01 member)
        {
             _response = additionalService.InsertSelectiveFields(member);
            if (_response.IsError)
                return BadRequest(_response.Message);
            return Ok(_response.Message);
        }

        /// <summary>
        /// Updates only selected fields of a member.
        /// </summary>
        [HttpPut]
        [Route("update-selective")] // PUT api/member/update-selective
        public IHttpActionResult UpdateSelectiveFields(YMM01 member)
        {
             _response = additionalService.UpdateSelectiveFields(member);
            if (_response.IsError)
                return BadRequest(_response.Message);
            return Ok(_response.Message);
        }

        /// <summary>
        /// Updates a member using a dictionary of field values.
        /// </summary>
        [HttpPut]
        [Route("update-by-dictionary/{id}")] // PUT api/member/update-by-dictionary/{id}
        public IHttpActionResult UpdateByDictionary(int id, Dictionary<string, object> updateFields)
        {
             _response = additionalService.UpdateByDictionary(id, updateFields);
            if (_response.IsError)
                return BadRequest(_response.Message);
            return Ok(_response.Message);
        }

        /// <summary>
        /// Updates members based on a custom condition.
        /// </summary>
        [HttpPut]
        [Route("update-with-condition")] // PUT api/member/update-with-condition
        public IHttpActionResult UpdateWithCondition(Dictionary<string, object> updateFields, string oldValue)
        {
             _response = additionalService.UpdateWithCondition(updateFields, oldValue);
            if (_response.IsError)
                return BadRequest(_response.Message);
            return Ok(_response.Message);
        }

        /// <summary>
        /// Updates only the non-default (changed) fields of a member.
        /// </summary>
        [HttpPatch]
        [Route("update-non-defaults/{id}")]
        public IHttpActionResult UpdateNonDefaults(int id, DTOYMM01 memberDto)
        {
             _response = memberService.UpdateNonDefaults(id, memberDto);
            if (_response.IsError)
                return BadRequest(_response.Message);

            return Ok(_response.Message);
        }

        /// <summary>
        /// Counts the total number of active members.
        /// </summary>
        [HttpGet]
        [Route("count-active-members")]
        public IHttpActionResult CountActiveMembers()
        {
            int count = _memberService.CountActiveMembers();
            return Ok(count);
        }

        /// <summary>
        /// Retrieves a list of all member email addresses.
        /// </summary>
        [HttpGet]
        [Route("all-emails")]
        public IHttpActionResult GetAllEmails()
        {
            List<string> emails = _memberService.GetAllEmails();
            return Ok(emails);
        }

        /// <summary>
        /// Retrieves a dictionary of the number of members who joined in specified years.
        /// </summary>
        [HttpPost]
        [Route("members-by-year")]
        public IHttpActionResult GetMembersByJoiningYear([FromBody] int[] years)
        {
            Dictionary<int, int> result = _memberService.GetMembersByJoiningYear(years);
            return Ok(result);
        }

        /// <summary>
        /// Deletes all inactive members from the database.
        /// </summary>
        [HttpDelete]
        [Route("delete-inactive-members")]
        public IHttpActionResult DeleteInactiveMembers()
        {
            _response = _memberService.DeleteInactiveMembers();
            if (_response.IsError)
                return BadRequest(_response.Message);

            return Ok(_response.Message);
        }

        /// <summary>
        /// Updates member details by adding or modifying existing values.
        /// </summary>
        [HttpPatch]
        [Route("update-member-score/{memberId}/{scoreIncrease}")]
        public IHttpActionResult UpdateMemberScore(int memberId, int scoreIncrease)
        {
            _response = _memberService.UpdateMemberScore(memberId, scoreIncrease);
            if (_response.IsError)
                return BadRequest(_response.Message);

            return Ok(_response.Message);
        }
    }
}
