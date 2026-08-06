using Microsoft.AspNetCore.Mvc;
using Ramadhan_Digital.Models;
using Ramadhan_Digital.Services;
using ModelRegisterRequest = Ramadhan_Digital.Models.RegisterRequest;
using System.Security.Claims;
using System.Linq;

namespace Ramadhan_Digital.Controllers
{
    public static class AuthController
    {
        public static void MapAuth(this WebApplication app)
        {
            var publicGroup = app.MapGroup("/api/v1/auth");
            var adminGroup = app.MapGroup("/api/v1/auth").RequireAuthorization(Policies.Admin);



            publicGroup.MapPost("/register-admin", async (
                ModelRegisterRequest request,
                AuthServices services,
                IPasswordService passwordService,
                IJWTService jwtService) =>
            {
                try
                {
                    // Cek apakah user sudah terdaftar
                    if (await services.IsRegistered())
                    {
                        return Results.BadRequest(new
                        {
                            message = "Registration is disabled."
                        });
                    }

                    // Hash password
                    var hashedPassword = passwordService.HashPassword(request.Password);

                    var user = new User
                    {
                        IdRole = request.IdRole,
                        IdKelas = request.IdKelas,
                        Nama = request.Nama,
                        Username = request.Username,
                        Password = hashedPassword
                    };

                    // Simpan user
                    var result = await services.RegisterAdmin(user);

                    if (!result)
                    {
                        return Results.BadRequest(new
                        {
                            message = "Register failed."
                        });
                    }

                    // Buat JWT
                    var token = jwtService.GenerateToken(user);

                    return Results.Ok(new
                    {
                        message = "Register success.",
                        token
                    });
                }
                catch (Exception ex)
                {
                    return Results.Problem(
                        title: "Internal Server Error",
                        detail: ex.Message,
                        statusCode: StatusCodes.Status500InternalServerError);
                }

            }).DisableAntiforgery();

            publicGroup.MapPost("/login", async (
                LoginRequest request,
                AuthServices services,
                IPasswordService passwordService,
                IJWTService jwtService) =>
            {
                

                try
                {
                    // Ambil user berdasarkan username
                    var user = await services.Login(request.Username);

                    if (string.IsNullOrEmpty(user.Password))
                    {
                        return Results.Unauthorized();
                    }

                    // Verifikasi password
                    if (!passwordService.VerifyPassword(request.Password, user.Password))
                    {
                        return Results.Unauthorized();
                    }

                    var token = jwtService.GenerateToken(user);
                    var refresh = jwtService.GenerateRefreshToken();

                    var response = new LoginResponse
                    {
                        Token = token,
                        Username = user.Username,
                        Nama = user.Nama,
                        Role = user.Role,
                        Kelas = user.Kelas,
                        RefreshToken = refresh
                    };

                    return Results.Ok(response);
                }
                catch (Exception ex)
                {
                    return Results.InternalServerError(ex.Message);
                }

            }).DisableAntiforgery();

            adminGroup.MapPost("/register-bulk-excel-siswa", async (
                IFormFile file,
                int idKelas,
                ExcelImportService excelService) =>
            {
                try
                {
                    if (file == null || file.Length == 0)
                    {
                        return Results.BadRequest(new ExcelImportResponse
                        {
                            Success = false,
                            Message = "File tidak ditemukan atau kosong"
                        });
                    }

                    var allowedExtensions = new[] { ".xlsx", ".xls" };
                    var fileExtension = Path.GetExtension(file.FileName).ToLower();

                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        return Results.BadRequest(new ExcelImportResponse
                        {
                            Success = false,
                            Message = "Format file harus .xlsx atau .xls"
                        });
                    }

                    using var stream = file.OpenReadStream();
                    var (success, importedCount, errors) = await excelService.ImportSiswaFromExcel(stream, idKelas);

                    var response = new ExcelImportResponse
                    {
                        Success = success,
                        ImportedCount = importedCount,
                        Errors = errors,
                        Message = success
                            ? $"Berhasil mengimpor {importedCount} siswa ke kelas {idKelas}"
                            : "Gagal mengimpor data siswa dari Excel"
                    };

                    return success ? Results.Ok(response) : Results.BadRequest(response);
                }
                catch (Exception ex)
                {
                    return Results.Problem(
                        title: "Internal Server Error",
                        detail: ex.Message,
                        statusCode: StatusCodes.Status500InternalServerError);
                }
            }).DisableAntiforgery().WithName("ImportSiswaFromExcel");

            adminGroup.MapPost("/register-bulk-excel-guru", async (
                IFormFile file,
                ExcelImportService excelService) =>
            {
                try
                {
                    if (file == null || file.Length == 0)
                    {
                        return Results.BadRequest(new ExcelImportResponse
                        {
                            Success = false,
                            Message = "File tidak ditemukan atau kosong"
                        });
                    }

                    var allowedExtensions = new[] { ".xlsx", ".xls" };
                    var fileExtension = Path.GetExtension(file.FileName).ToLower();

                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        return Results.BadRequest(new ExcelImportResponse
                        {
                            Success = false,
                            Message = "Format file harus .xlsx atau .xls"
                        });
                    }

                    using var stream = file.OpenReadStream();
                    var (success, importedCount, errors) = await excelService.ImportGuruFromExcel(stream);

                    var response = new ExcelImportResponse
                    {
                        Success = success,
                        ImportedCount = importedCount,
                        Errors = errors,
                        Message = success
                            ? $"Berhasil mengimpor {importedCount} guru"
                            : "Gagal mengimpor data guru dari Excel"
                    };

                    return success ? Results.Ok(response) : Results.BadRequest(response);
                }
                catch (Exception ex)
                {
                    return Results.Problem(
                        title: "Internal Server Error",
                        detail: ex.Message,
                        statusCode: StatusCodes.Status500InternalServerError);
                }
            }).DisableAntiforgery().WithName("ImportGuruFromExcel");

            adminGroup.MapGet("/me", (ClaimsPrincipal user) =>
            {
                try
                {
                    var username = user.Identity?.Name ?? string.Empty;
                    var roles = user.Claims
                        .Where(c => c.Type == ClaimTypes.Role)
                        .Select(c => c.Value)
                        .ToArray();

                    return Results.Ok(new
                    {
                        username,
                        roles
                    });
                }
                catch (Exception ex)
                {
                    return Results.Problem(
                        title: "Internal Server Error",
                        detail: ex.Message,
                        statusCode: StatusCodes.Status500InternalServerError);
                }
            });
        }
    }
}
