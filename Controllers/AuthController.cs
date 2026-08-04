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

            publicGroup.MapPost("/register", async (
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
                    var result = await services.Register(user);

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

                    if (user == null)
                    {
                        return Results.BadRequest(new
                        {
                            message = "Invalid username or password."
                        });
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
                        IdRole = user.IdRole,
                        IdKelas = user.IdKelas ?? 0,
                        RefreshToken = refresh
                    };

                    return Results.Ok(response);
                }
                catch (Exception ex)
                {
                    return Results.Problem(
                        title: "Internal Server Error",
                        detail: ex.Message,
                        statusCode: StatusCodes.Status500InternalServerError);
                }

            }).DisableAntiforgery();

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
