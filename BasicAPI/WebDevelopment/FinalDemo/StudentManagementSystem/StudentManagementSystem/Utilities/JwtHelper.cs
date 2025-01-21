using System;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace StudentManagementSystem.Utilities
{
    public static class JwtHelper
    {
        private static string _key = "aGVsbG9BbnlFeGNlcHRpb25hbGx5U2VjdXJlU3VwZXJTZWNyZXRLZXk=";

        public static string GenerateJwtToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("role", "Admin")
            };

            var token = new JwtSecurityToken(
                issuer: "https://localhost:44304",
                audience: "https://localhost:44304",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}