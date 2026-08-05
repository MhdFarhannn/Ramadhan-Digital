using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Ramadhan_Digital.Models;

namespace Ramadhan_Digital.Services
{
    public interface IJWTService
    {
        string GenerateToken(User user);
        string GenerateRefreshToken();
    }

    public class JWTService : IJWTService
    {
        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.Nama ?? "Unknown"),  // ✅ Fallback ke "Unknown"
        new Claim(JwtRegisteredClaimNames.UniqueName, user.Username ?? ""),  // ✅ Fallback ke empty string
        new Claim("IdRole", user.IdRole.ToString()),
        new Claim("IdKelas", user.IdKelas?.ToString() ?? "")
    };

            if (!string.IsNullOrEmpty(user.Role))
            {
                claims.Add(new Claim(ClaimTypes.Role, user.Role));
            }

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(Env.Value["JWT:Key"]!)
            );

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                issuer: Env.Value["JWT:Issuer"],
                audience: Env.Value["JWT:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];

            using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();

            rng.GetBytes(randomBytes);

            return Convert.ToBase64String(randomBytes);
        }
    }
}
