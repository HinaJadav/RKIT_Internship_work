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
        private readonly BLYMM01 memberService;
        private readonly BLSecurity loginService;

        public YMM01Controller()
        {
            memberService = new BLYMM01();
            loginService = new BLSecurity();
        }

        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login(DTOLogin loginDto)
        {
            var response = loginService.Login(loginDto);
            if (response.IsError)
                return BadRequest(response.Message);

            return Ok(response.Message);
        }


        // Add Member
        [HttpPost]
        [Route("add")] // POST api/member/add
        public IHttpActionResult AddMember(DTOYMM01 memberDto)
        {
            YMM01 memberModel = memberService.PreSaveMember(memberDto);
            var (isValidMember, memberValidationMessage) = memberService.ValidateOnSaveMember(memberModel);

            if (isValidMember)
            {
                Response response = memberService.SaveMember(memberModel);
                return Ok(response.Message);
            }

            return BadRequest(memberValidationMessage);
        }

        // Update Member
        [HttpPut]
        [Route("update/{id}")] // PUT api/member/update/{id}
        public IHttpActionResult UpdateMember(int id, DTOYMM01 memberDto)
        {
            YMM01 editMemberModel = memberService.PreDeleteMember(id);
            if (editMemberModel != null)
            {
                editMemberModel.M01F02 = memberDto.M01102;
                editMemberModel.M01F03 = memberDto.M01103;
                editMemberModel.M01F04 = decimal.Parse(memberDto.M01104);
                
                editMemberModel.M01F08 = memberDto.M01108 == 1;

                var (isValidUpdateMember, updateMemberValidationMessage) = memberService.ValidateOnSaveMember(editMemberModel);

                if (isValidUpdateMember)
                {
                    Response response = memberService.SaveMember(editMemberModel);
                    return Ok(response.Message);
                }

                return BadRequest(updateMemberValidationMessage);
            }

            return NotFound();
        }

        // Delete Member
        [HttpDelete]
        [Route("delete/{id}")] // DELETE api/member/delete/{id}
        public IHttpActionResult DeleteMember(int id)
        {
            YMM01 preDeleteMemberModel = memberService.PreDeleteMember(id);
            var (isValidDeleteMember, deleteMemberValidationMessage) = memberService.ValidateOnDeleteMember(preDeleteMemberModel);

            if (isValidDeleteMember)
            {
                Response response = memberService.DeleteMember(id);
                return Ok(response.Message);
            }

            return BadRequest(deleteMemberValidationMessage);
        }

        [HttpGet]
        [Route("download-data")]
        public IHttpActionResult DownloadData()
        {
            try
            {
                // Fetch all the data
                List<DTOYMM01> allMembers = memberService.GetAllMembers();  // Fetches all member data

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
                return Ok(new { fileUrl });  // Returning the URL so the user can download the file
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);  // Return error details
            }
        }

    }
}
