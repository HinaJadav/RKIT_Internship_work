using FinalDemo.DB;
using FinalDemo.Models;
using FinalDemo.Models.DTO;
using FinalDemo.Models.POCO;
using FinalDemo.Security;
using ServiceStack.OrmLite;
using System;
using ORMDemo.Models.Enums;
using System.Collections.Generic;


namespace FinalDemo.BL
{
    /// <summary>
    /// Business Logic for managing YMM01 (Member) entities.
    /// Includes methods for validating, saving, deleting, and fetching member data.
    /// </summary>
    public class BLYMM01
    {
        private YMM01 _m01Obj;
        private int _memberId; // don't make global
        private Response _response;
        public OperationType Type { get; set; }


        public BLYMM01()
        {
            _response = new Response();
        }
        /// <summary>
        /// Converts DTO (Data Transfer Object) to POCO (Plain Old CLR Object) for saving.
        /// Encrypts the password before storing it.
        /// </summary>
        /// <param name="dtoMember">The DTO containing member data.</param>
        /// <returns>A POCO (YMM01) object populated with the data from the DTO.</returns>

        public void PreSaveMember(DTOYMM01 dtoMember)
        {
            // Encrypt the password before storing it
            string encryptedPassword = EncryptPassword(dtoMember.M01109);

            _m01Obj = dtoMember.Convert<YMG01>();
            if (Type == OperationType.E || Type == OperationType.D)
            {
                if (dtoMember.G01101 > 0)
                {
                    _memberId = dtoMember.G01101;
                }
            }

        }

        private bool IsMemberExist(int memberId)
        {
            using (var db = DBConnection.OpenConnection())
            {
                return db.Exists<YMM01>(memberId);
            }
        }

        /// <summary>
        /// Validates member data before saving it to the database.
        /// </summary>
        /// <param name="member">The member object to validate.</param>
        /// <returns>A tuple containing a boolean indicating validity and a message.</returns>

        public _response Validation()
        {
            if (Type == OperationType.E || Type == OperationType.D)
            {
                if (_memberId <= 0)
                {
                    _response.IsError = true;
                    _response.Message = "Enter a valid member ID.";
                }
                else if (!IsMemberExist(_memberId))
                {
                    _response.IsError = true;
                    _response.Message = "member does not exist.";
                }
            }
            return _response;
        }

        /// <summary>
        /// Saves a member to the database.
        /// </summary>
        /// <param name="member">The member object to save.</param>
        /// <returns>A _response indicating the result of the save operation.</returns>

        public _response Save()
        {
            try
            {
                using (var db = DBConnection.OpenConnection())
                {
                    if (Type == OperationType.A)
                    {
                        db.Insert(_m01Obj);
                        _response.Message = $"Successfully saved member {_m01Obj.M01F02}.";
                    }
                    else if (Type == OperationType.E)
                    {
                        db.Update(_m01Obj);
                        _response.Message = $"Successfully updated member {_m01Obj.M01F02}.";
                    }
                }
            }
            catch (Exception ex)
            {
                _response.IsError = true;
                _response.Message = $"Error: {ex.Message}";
            }
            return _response;
        }
;
    


        /// <summary>
        /// Encrypts the password using Rijndael encryption.
        /// </summary>
        /// <param name="password">The plain password to encrypt.</param>
        /// <returns>The encrypted password.</returns>

        private string EncryptPassword(string password)
        {
            // Encrypt password using RijndaelSecurity
            string encryptedPassword = RijndaelSecurity.Encrypt(password);

            return encryptedPassword;
        }

        // add comment
        public Response Delete(int memberId)
        {
            try
            {
                using (var db = DBConnection.OpenConnection())
                {
                    db.DeleteById<YM01>(memberId);
                }
                _response.Message = "Member is deleted!";
            }
            catch (Exception ex)
            {
                _response.IsError = true;
                _response.Message = ex.Message;
            }
            return _response;
        }

        /// <summary>
        /// Fetches all members from the database.
        /// </summary>
        /// <returns>A list of DTOYMM01 objects representing all members.</returns>

        public List<YMM01> GetAll()
        {
            using (var db = DBConnection.OpenConnection())
            {
                return db.Select<YMM01>();
            }
        }

        public YMM01 GetById(int id)
        {
            using (var db = DBConnection.OpenConnection())
            {
                return db.SingleById<YMM01>(id); // entire model data
            }
        }

        /// <summary>
        /// Inserts multiple members into the database at once.
        /// </summary>
        /// <param name="members">List of YMM01 members to insert.</param>
        /// <returns>A _response indicating the result of the insert operation.</returns>
        public Response InsertAllMembers(List<YMM01> members)
        {
           
            try
            {
                using (var db = DBConnection.OpenConnection())
                {
                    db.InsertAll(members);
                }
                _response.Message = "All members inserted successfully.";
            }
            catch (Exception ex)
            {
                _response.IsError = true;
                _response.Message = $"Error: {ex.Message}";
            }
            return _response;
        }

        /// <summary>
        /// Inserts only specific fields for a new member.
        /// </summary>
        /// <param name="member">The member object containing the data.</param>
        /// <returns>A _response indicating the result of the insert operation.</returns>
        public Response InsertSelectiveFields(YMM01 member)
        {
            
            try
            {
                using (var db = DBConnection.OpenConnection())
                {
                    db.InsertOnly(member, member1 => new { member1.M01F02, member1.M01F03 });
                }
                _response.Message = "Selected fields inserted successfully.";
            }
            catch (Exception ex)
            {
                _response.IsError = true;
                _response.Message = $"Error: {ex.Message}";
            }
            return _response;
        }

        /// <summary>
        /// Updates only specific fields of a member.
        /// </summary>
        /// <param name="member">The member object with updated values.</param>
        /// <returns>A _response indicating the result of the update operation.</returns>
        public Response UpdateSelectiveFields(YMM01 member)
        {
            
            try
            {
                using (var db = DBConnection.OpenConnection())
                {
                    db.UpdateOnlyFields(member, onlyFields: member1 => member1.M01F02);
                }
                _response.Message = "Selected fields updated successfully.";
            }
            catch (Exception ex)
            {
                _response.IsError = true;
                _response.Message = $"Error: {ex.Message}";
            }
            return _response;
        }

        //char  34   
        /// <summary>
        /// Updates members based on a custom condition.
        /// </summary>
        /// <param name="updateFields">Dictionary containing field names and their new values.</param>
        /// <param name="oldValue">The condition value to match before updating.</param>
        /// <returns>A _response indicating the result of the update operation.</returns>
        public Response UpdateWithCondition(Dictionary<string, object> updateFields, string oldValue)
        {
          
            try
            {
                using (var db = DBConnection.OpenConnection())
                {
                    db.UpdateOnly<YMM01>(updateFields, member1 => member1.M01F02 == oldValue);
                }
                _response.Message = "Member updated successfully with condition.";
            }
            catch (Exception ex)
            {
                _response.IsError = true;
                _response.Message = $"Error: {ex.Message}";
            }
            return _response;
        }


        /// <summary>
        /// Updates only the non-default (changed) fields of a member.
        /// </summary>
        public Response UpdateNonDefaults(int id, DTOYMM01 memberDto)
        {
            try
            {
                using (var db = DBConnection.OpenConnection())
                {
                    YMM01 existingMember = db.ById<YMM01>(db, id);
                }
                    
                if (existingMember == null)
                { 
                    _response.IsError = true;
                    _response.Message = "Member not found." };
 
                }

                YMM01 updatedMember = new YMM01
                {
                    M01F02 = memberDto.M01102 ?? existingMember.M01F02,
                    M01F03 = memberDto.M01103 ?? existingMember.M01F03,
                    M01F04 = memberDto.M01104 != null ? decimal.Parse(memberDto.M01104) : existingMember.M01F04,
                    M01F08 = memberDto.M01108 == 1 ? true : existingMember.M01F08
                }; // obj init separate

                db.UpdateNonDefaults(updatedMember, member1 => member1.M01F01 == id);

                _response.Message = "Member updated successfully." ;
            }
            catch (Exception ex)
            {
                _response.IsError = true;
                _response.Message = "Error: {ex.Message}";
            }

            return _response;
        }

        /// <summary>
        /// Counts the total number of active members.
        /// </summary>
        public int CountActiveMembers()
        {
            using (var db = DBConnection.OpenDbConnection())
            {
                return db.Scalar<int>(db.From<YMM01>()
                    .Where(member1 => member1.M01F08 == 1)
                    .Select(Sql.Count("*")));
            }
        }

        /// <summary>
        /// Retrieves a list of all member email addresses.
        /// </summary>
        public List<string> GetAllEmails()
        {
            using (var db = DBConnection.OpenDbConnection())
            {
                return db.Column<string>(db.From<YMM01>().Select(member1 => member1.M01F03));
            }
        }

        /// <summary>
        /// Retrieves a dictionary of the number of members who joined in specified years.
        /// </summary>
        public Dictionary<int, int> GetMembersByJoiningYear(int[] years)
        {
            using (var db = DBConnection.OpenDbConnection())
            {
                return db.Dictionary<int, int>(db.From<YMM01>()
                    .Where(member1 => Sql.In(member1.M01F07.Year, years))
                    .GroupBy(member1 => member1.M01F07.Year)
                    .Select(member1 => new { member1.M01F07.Year, Total = Sql.Count("*") }));
            }
        }

        /// <summary>
        /// Deletes all inactive members from the database.
        /// </summary>
        public _response DeleteInactiveMembers()
        {
            
            try
            {
                using (var db = DBConnection.OpenDbConnection())
                {
                    YMM01 inactiveMembersQuery = db.From<YMM01>().Where(member1 => member1.M01F08 == 0);
                    db.Delete(inactiveMembersQuery);
                }
                _response.Message = "Inactive members have been deleted.";
            }
            catch (Exception ex)
            {
                _response.IsError = true;
                _response.Message = ex.Message;
            }
            return _response;
        }

        /// <summary>
        /// Updates member details by adding or modifying existing values.
        /// </summary>
        public _response UpdateMemberScore(int memberId, int activityStatus)
        {
            try
            {
                using (var db = DBConnection.OpenDbConnection())
                {
                    db.UpdateAdd(() => new YMM01 { M01F08 = activityStatus }, where: member1 => member1.M01F01 == memberId);
                }
                _response.Message = "Member score updated successfully.";
            }
            catch (Exception ex)
            {
                _response.IsError = true;
                _response.Message = ex.Message;
            }
            return _response;
        }
        
        
    }
}
