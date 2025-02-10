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

        // Constructor to initialize services
        public YMM01Controller()
        {
            _memberService = new BLYMM01();
            _loginService = new BLSecurity();
        }

        /// <summary>
        /// Handles user login requests.
        /// </summary>
        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login(DTOLogin loginDto)
        {
            Response response = _loginService.Login(loginDto);
            if (response.IsError)
                return BadRequest(response.Message);

            return Ok(response.Message);
        }

        /// <summary>
        /// Adds a new member to the system.
        /// </summary>
        [HttpPost]
        [Route("add")] // POST api/member/add
        public Response AddMember(DTOYMM01 memberDto)
        {
            _memberService.Type = OperationType.A;
            _memberService.PreSave(gameDto);
            _response = _memberService.Validation();
            if (!_response.IsError)
            {
                _response = _memberService.Save();
            }
            return _response;
        }

        /// <summary>
        /// Updates an existing member's information.
        /// </summary>
        [HttpPut]
        [Route("update")] // PUT api/member/update
        public Response UpdateMember(int id, DTOYMM01 memberDto)
        {
            _memberService.Type = OperationType.E;
            _memberService.PreSave(gameDto);
            _response = _memberService.Validation();
            if (!_response.IsError)
            {
                _response = _memberService.Save();
            }
            return _response;
        }

        /// <summary>
        /// Deletes an existing member from the system.
        /// </summary>
        [HttpDelete]
        [Route("delete/{id}")] // DELETE api/member/delete/{id}
        public Response DeleteMember(int id)
        {
            return _memberService.Delete(id);
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
            Response response = additionalService.InsertAllMembers(members);
            if (response.IsError)
                return BadRequest(response.Message);
            return Ok(response.Message);
        }

        /// <summary>
        /// Inserts only specific fields of a member.
        /// </summary>
        [HttpPost]
        [Route("insert-selective")] // POST api/member/insert-selective
        public IHttpActionResult InsertSelectiveFields(YMM01 member)
        {
            Response response = additionalService.InsertSelectiveFields(member);
            if (response.IsError)
                return BadRequest(response.Message);
            return Ok(response.Message);
        }

        /// <summary>
        /// Updates only selected fields of a member.
        /// </summary>
        [HttpPut]
        [Route("update-selective")] // PUT api/member/update-selective
        public IHttpActionResult UpdateSelectiveFields(YMM01 member)
        {
            Response response = additionalService.UpdateSelectiveFields(member);
            if (response.IsError)
                return BadRequest(response.Message);
            return Ok(response.Message);
        }

        /// <summary>
        /// Updates a member using a dictionary of field values.
        /// </summary>
        [HttpPut]
        [Route("update-by-dictionary/{id}")] // PUT api/member/update-by-dictionary/{id}
        public IHttpActionResult UpdateByDictionary(int id, Dictionary<string, object> updateFields)
        {
            Response response = additionalService.UpdateByDictionary(id, updateFields);
            if (response.IsError)
                return BadRequest(response.Message);
            return Ok(response.Message);
        }

        /// <summary>
        /// Updates members based on a custom condition.
        /// </summary>
        [HttpPut]
        [Route("update-with-condition")] // PUT api/member/update-with-condition
        public IHttpActionResult UpdateWithCondition(Dictionary<string, object> updateFields, string oldValue)
        {
            Response response = additionalService.UpdateWithCondition(updateFields, oldValue);
            if (response.IsError)
                return BadRequest(response.Message);
            return Ok(response.Message);
        }

        /// <summary>
        /// Updates only the non-default (changed) fields of a member.
        /// </summary>
        [HttpPatch]
        [Route("update-non-defaults/{id}")]
        public IHttpActionResult UpdateNonDefaults(int id, DTOYMM01 memberDto)
        {
            Response response = memberService.UpdateNonDefaults(id, memberDto);
            if (response.IsError)
                return BadRequest(response.Message);

            return Ok(response.Message);
        }
    }
}
