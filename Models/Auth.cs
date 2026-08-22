using Microsoft.AspNetCore.Authorization;

namespace Ramadhan_Digital.Models
{
    public class RegisterRequest
    {
        public int IdRole { get; set; }
        public int IdKelas { get; set; }
        public string Nama { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Nama { get; set; } = string.Empty;
        public string Role { get; set; }
        public string Kelas { get; set; }
        public int? IdKelas { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
    }

    public class RefreshRequest
    {
        public string RefreshToken { get; set; } = string.Empty;
    }

    public class ExcelImportResponse
    {
        public bool Success { get; set; }
        public int ImportedCount { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public string Message { get; set; } = string.Empty;
    }


    public static class Policies
    {
        public const string Admin = "Admin";

        public static void Register(AuthorizationOptions options)
        {
            options.AddPolicy(
                Admin,
                p => p.RequireRole("Admin")
            );
        }
    }
}