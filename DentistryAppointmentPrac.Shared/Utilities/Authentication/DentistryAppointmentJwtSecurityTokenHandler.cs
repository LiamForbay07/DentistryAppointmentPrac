using DentistryAppointmentPrac.Shared.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DentistryAppointmentPrac.Shared.Utilities.Authentication
{
    public static class DentistryAppointmentJwtSecurityTokenHandler
    {
        public const string TechSupportIdClaim = "super_user_id"; //Tech support is the only super user
        public const string TechSupportUserNameClaim = "super_user_name";
        public static string GenerateToken(IConfiguration configuration, string userId, Guid? dentalDepartmentId, string? superUserId = null, string? superUserName = null)
        {
            var jwtSection = configuration["JWT:Key"];
            var expires = configuration["JWT:ExpiresInMinutes"];
            var expiresIn = string.IsNullOrEmpty(configuration["JWT:ExpiresInMinute"]) ? 60 : int.Parse(expires ?? "60");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection ?? ""));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, userId),
            };

            if (dentalDepartmentId != null)
                claims = [.. claims, new Claim(ClaimTypes.Role, dentalDepartmentId.ToString() ?? "")];

            if (!string.IsNullOrEmpty(superUserId))
            {
                claims.Add(new Claim(TechSupportIdClaim, superUserId));
                claims.Add(new Claim(TechSupportUserNameClaim, superUserName ?? ""));
            }

            var token = new JwtSecurityToken(
                claims:claims,
                expires: DateTime.UtcNow.AddMinutes(expiresIn),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
