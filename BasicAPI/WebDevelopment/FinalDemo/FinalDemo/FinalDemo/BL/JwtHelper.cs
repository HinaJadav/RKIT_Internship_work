using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinalDemo.BL
{
    public class JwtHelper
    {
        /// <summary>
        /// Generates a JWT token for the specified username.
        /// </summary>
        /// <param name="username">The username to include as a claim in the token.</param>
        /// <returns>A JWT token string.</returns>
        public static string GenerateToken(string username)
        {
            // Secret key for signing the JWT token 
            var secretKey = "9c1a19a1bc0ed85788a01293fa94d483ba05c882020fe1bef6a7ab0e1911bd2d!";

            // Convert the secret key into a SymmetricSecurityKey
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            // Create signing credentials using HMACSHA256 algorithm
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create a list of claims for the token 
            var jwtClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username)
            };

            // Create the JWT token with claims, expiration time, and signing credentials
            var token = new JwtSecurityToken(
                issuer: "YourIssuer",            // Replace with your issuer (e.g., your app domain)
                audience: "YourAudience",        // Replace with your audience
                claims: jwtClaims,         // Contains the user-specific information
                expires: DateTime.Now.AddMinutes(30), // Set token expiration to 30 minutes from now
                signingCredentials: creds
            );

            // Write the token as a string
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            // Return the JWT token string
            return jwtToken;
        }
    }
}
