using ServiceStack.OrmLite;
using FinalDemo.DB;
using FinalDemo.Models;
using FinalDemo.Models.DTO;
using FinalDemo.Models.POCO;
using FinalDemo.Security;
using System;
using System.Linq;

namespace FinalDemo.BL
{
    public class BLSecurity
    {
        private static readonly byte[] key = Convert.FromBase64String("U2VjcmV0S2V5MTIzNDU2Nzg5MDEyMw=="); // Base64 encoded 256-bit key
        private static readonly byte[] iv = Convert.FromBase64String("Q3RjY3BvMTIzNDU2Nzg5MDEyMw==");   // Base64 encoded 128-bit IV
        private static readonly int blockSize = 128; // Common block size for Rijndael

        public YMM01 GetMemberByEmail(string email)
        {
            using (var db = DBConnection.OpenConnection())
            {
                var query = "SELECT * FROM YMM01 WHERE M01F03 = @Email";
                return db.SqlList<YMM01>(query, new { Email = email }).FirstOrDefault();
            }
        }


        public bool ValidatePassword(string plainPassword, string encryptedPassword)
        {
            // Decrypt and compare password
            var decryptedPassword = RijndaelSecurity.Decrypt(Convert.FromBase64String(encryptedPassword), key, iv, blockSize);
            return plainPassword == decryptedPassword;
        }

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
