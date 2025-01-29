using FilterDemo.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace FilterDemo.BL.Service
{
    public class AuthService
    {
        private static readonly List<YMU01> users = new()
        {
            new YMU01 { U01F01 = 1, U01F02 = "admin", U01F03 = "admin123", U01F04 = "Admin" },
            new YMU01 { U01F01 = 2, U01F02 = "manager", U01F03 = "manager123", U01F04 = "Manager" },
            new YMU01 { U01F01 = 3, U01F02 = "employee", U01F03 = "employee123", U01F04 = "Employee" }
        };

        private readonly string _key;

        public AuthService(string key)
        {
            _key = key;
        }

        public YMU01? ValidateUser(string username, string password)
        {
            return users.FirstOrDefault(u => u.U01F02 == username && u.U01F03 == password);
        }

        public string GenerateJwtToken(YMU01 user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.U01F02),
                new Claim(ClaimTypes.Role, user.U01F04)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: "yourapp",
                audience: "yourapp",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
