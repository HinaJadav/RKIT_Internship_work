using ServiceStack.OrmLite;
using FinalDemo.DB;
using FinalDemo.Models;
using FinalDemo.Models.DTO;
using FinalDemo.Models.POCO;
using FinalDemo.Security;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace FinalDemo.BL
{
    /// <summary>
    /// Provides business logic related to member login and password validation.
    /// </summary>
    public class BLSecurity
    {
        /// <summary>
        /// Retrieves a member by their email address.
        /// </summary>
        /// <param name="email">The email of the member.</param>
        /// <returns>A member object if found, otherwise null.</returns>
        public YMM01 GetMemberByEmail(string email)
        {
            using (var db = DBConnection.OpenConnection())
            {
                var query = "SELECT * FROM YMM01 WHERE M01F03 = @Email";
                return db.SqlList<YMM01>(query, new { Email = email }).FirstOrDefault();
            }
        }

        /// <summary>
        /// Validates the plain password by comparing it to the decrypted stored password.
        /// </summary>
        /// <param name="plainPassword">The plain text password provided by the user.</param>
        /// <param name="encryptedPassword">The encrypted password stored in the database.</param>
        /// <returns>True if the passwords match, otherwise false.</returns>
        public bool ValidatePassword(string plainPassword, string encryptedPassword)
        {
            // Decrypt the stored encrypted password
            string decryptedPassword = RijndaelSecurity.Decrypt(encryptedPassword);

            // Compare the decrypted password with the plain text password
            return plainPassword == decryptedPassword;
        }

        /// <summary>
        /// Logs in a member by validating their email and password.
        /// </summary>
        /// <param name="loginDto">The login credentials (email and password).</param>
        /// <returns>A response indicating the login result.</returns>
        public Response Login(DTOLogin loginDto)
        {
            var member = GetMemberByEmail(loginDto.Email);
            if (member != null)
            {
                if (ValidatePassword(loginDto.Password, member.M01F09))
                {
                    return new Response { Message = "Login successful." };
                }
                else
                {
                    return new Response { IsError = true, Message = "Invalid password." };
                }
            }
            else
            {
                return new Response { IsError = true, Message = "Member not found." };
            }
        }
    }
}
