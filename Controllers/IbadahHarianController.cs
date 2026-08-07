using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Ramadhan_Digital.Models;
using Ramadhan_Digital.Services;

namespace Ramadhan_Digital.Controllers
{
    public static class IbadahHarianController
    {
        public static void MapIbadahHarian(this WebApplication app)
        {
            var publicGroup = app.MapGroup("/api/v1/ibadah-harian").RequireAuthorization();

            publicGroup.MapGet("/", GetIbadahByUserAndDate).WithName("GetIbadahByUserAndDate");
            publicGroup.MapPost("/", SaveIbadah).WithName("SaveIbadah");
        }

        // GET /api/v1/ibadah-harian?tanggal=2026-03-20
        private static async Task<IResult> GetIbadahByUserAndDate(
            ClaimsPrincipal user,
            [FromQuery] DateTime tanggal,
            IbadahHarianServices service)
        {
            // Ambil idUser otomatis dari Claim JWT Token (support ClaimTypes.NameIdentifier & 'sub')
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? user.FindFirst("sub")?.Value;
            if (!int.TryParse(userIdClaim, out int idUser))
            {
                return Results.Unauthorized();
            }

            var data = await service.GetByUserAndDateAsync(idUser, tanggal);
            if (data == null)
            {
                return Results.NotFound(new { status = "error", message = "Data ibadah harian tidak ditemukan" });
            }

            return Results.Ok(new { status = "success", data });
        }

        // POST /api/v1/ibadah-harian
        private static async Task<IResult> SaveIbadah(
            ClaimsPrincipal user,
            [FromBody] IbadahHarian ibadah,
            IbadahHarianServices service)
        {
            // Ambil idUser otomatis dari Claim JWT Token
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? user.FindFirst("sub")?.Value;
            if (!int.TryParse(userIdClaim, out int idUser))
            {
                return Results.Unauthorized();
            }

            // Assign idUser ke model IbadahHarian
            ibadah.IdUser = idUser;

            var isSaved = await service.SaveIbadahHarianAsync(ibadah);
            if (!isSaved)
            {
                return Results.BadRequest(new { status = "error", message = "Gagal menyimpan data ibadah harian" });
            }

            return Results.Ok(new { status = "success", message = "Data ibadah harian berhasil disimpan" });
        }
    }
}