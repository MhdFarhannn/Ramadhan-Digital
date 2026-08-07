using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Ramadhan_Digital.Models;
using Ramadhan_Digital.Services;

namespace Ramadhan_Digital.Controllers
{
    public static class TausiahController
    {
        public static void MapTausiah(this WebApplication app)

        {
            var publicGroup = app.MapGroup("/api/v1/tausiah").RequireAuthorization();

            publicGroup.MapGet("/", GetAllTausiah).WithName("GetAllTausiah");
            publicGroup.MapGet("/{id:int}", GetTausiahById).WithName("GetTausiahById");
            publicGroup.MapPost("/", CreateTausiah).WithName("CreateTausiah");
        }

        private static async Task<IResult> GetAllTausiah(TausiahServices service)
        {
            var data = await service.GetAllAsync();
            return Results.Ok(new { status = "success", data });
        }

        private static async Task<IResult> GetTausiahById(int id, TausiahServices service)
        {
            var data = await service.GetByIdAsync(id);
            if (data == null)
                return Results.NotFound(new { status = "error", message = "Tausiah tidak ditemukan" });

            return Results.Ok(new { status = "success", data });
        }

        private static async Task<IResult> CreateTausiah(
            [FromBody] Tausiah tausiah,
            ClaimsPrincipal user,
            TausiahServices service)
        {
            // Ambil IdUser dari Claim Token JWT
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                           ?? user.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Results.Unauthorized();
            }

            // Set IdUser secara otomatis
            tausiah.IdUser = int.Parse(userIdClaim);

            var isCreated = await service.CreateAsync(tausiah);
            if (!isCreated)
                return Results.BadRequest(new { status = "error", message = "Gagal menambahkan tausiah" });

            return Results.Ok(new { status = "success", message = "Tausiah berhasil ditambahkan" });
        }
    }
}