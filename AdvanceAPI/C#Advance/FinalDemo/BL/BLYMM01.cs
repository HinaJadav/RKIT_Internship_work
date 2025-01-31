using FinalDemo.DB;
using FinalDemo.Models;
using FinalDemo.Models.DTO;
using FinalDemo.Models.POCO;
using FinalDemo.Security;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;

namespace FinalDemo.BL
{
    public class BLYMM01
    {
        // Convert DTO to POCO for saving
        public YMM01 PreSaveMember(DTOYMM01 dtoMember)
        {
            // Encrypt the password before storing it
            string encryptedPassword = EncryptPassword(dtoMember.M01109);

            return new YMM01
            {
                M01F01 = dtoMember.M01101,
                M01F02 = dtoMember.M01102,
                M01F03 = dtoMember.M01103,
                M01F04 = decimal.Parse(dtoMember.M01104),

                M01F07 = dtoMember.M01107,
                M01F08 = dtoMember.M01108 == 1,
                M01F09 = encryptedPassword // Store encrypted password
            };
        }

        // Validate POCO before saving
        public (bool IsValid, string Message) ValidateOnSaveMember(YMM01 member)
        {
            if (string.IsNullOrEmpty(member.M01F02))
                return (false, "Full name cannot be empty.");
            if (string.IsNullOrEmpty(member.M01F03))
                return (false, "Email cannot be empty.");
            if (member.M01F04 <= 0)
                return (false, "Contact number must be a positive number.");
            return (true, "Member data validation passed.");
        }

        // Save Member to Database
        public Response SaveMember(YMM01 member)
        {
            Response response = new Response();
            try
            {
                using (var db = DBConnection.OpenConnection())
                using (var trans = db.OpenTransaction())
                {
                    db.Save(member);
                    trans.Commit();
                }
                response.Message = $"Successfully saved member {member.M01F02}.";
            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.Message = $"Error: {ex.Message}";
            }
            return response;
        }

        // Encrypt password using Rijndael encryption
        private string EncryptPassword(string password)
        {
            // Encrypt password using RijndaelSecurity
            string encryptedPassword = RijndaelSecurity.Encrypt(password);

            return encryptedPassword;
        }





        // Fetch Member before deletion
        public YMM01 PreDeleteMember(int id)
        {
            using (var db = DBConnection.OpenConnection())
            {
                return db.SingleById<YMM01>(id);
            }
        }

        // Validate Member before deletion
        public (bool IsValid, string Message) ValidateOnDeleteMember(YMM01 member)
        {
            if (member == null)
                return (false, "Member not found.");
            return (true, "Member can be deleted.");
        }

        // Delete Member from Database
        public Response DeleteMember(int id)
        {
            Response response = new Response();
            try
            {
                using (var db = DBConnection.OpenConnection())
                using (var trans = db.OpenTransaction())
                {
                    var member = db.SingleById<YMM01>(id);
                    if (member != null)
                    {
                        db.Delete(member);
                        trans.Commit();
                        response.Message = $"Member {member.M01F02} successfully deleted.";
                    }
                    else
                    {
                        response.IsError = true;
                        response.Message = "Member not found.";
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.Message = $"Error: {ex.Message}";
            }
            return response;
        }

        public List<DTOYMM01> GetAllMembers()
        {
            // Fetching all member data from the database
            using (var db = DBConnection.OpenConnection())
            {
                // Select all YMM01 records
                var members = db.Select<YMM01>();  // Select all rows of YMM01

                // Manually map the results from YMM01 to DTOYMM01
                var memberDtos = new List<DTOYMM01>();

                foreach (var m in members)
                {
                    var dto = new DTOYMM01
                    {
                        M01101 = m.M01F01,  // Id field
                        M01102 = m.M01F02,  // Full name
                        M01103 = m.M01F03,  // Email
                        M01104 = m.M01F04.ToString(),  // Contact number as string

                        M01107 = m.M01F07,  // Joining date
                        M01108 = m.M01F08 ? 1 : 0,  // Active status (converted to 1/0)
                        M01109 = m.M01F09  // Password (encrypted, not recommended to expose)
                    };
                    memberDtos.Add(dto);
                }

                return memberDtos;
            }
        }




    }
}
