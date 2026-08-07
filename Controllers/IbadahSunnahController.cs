using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Ramadhan_Digital.Services;

namespace Ramadhan_Digital.Controllers
{
    public static class IbadahSunnahController
    {
        public static void MapIbadahSunnah(this WebApplication app)
        {
            var publicGroup = app.MapGroup("/api/v1/ibadah-sunnah").RequireAuthorization();

            // Endpoint Siswa / User Sendiri
            publicGroup.MapGet("/", GetMyIbadahSunnah).WithName("GetMyIbadahSunnah");
            publicGroup.MapPost("/", SaveIbadahSunnah).WithName("SaveIbadahSunnah");

            // Endpoint Monitoring untuk Guru
            publicGroup.MapGet("/monitoring/user/{idSantri:int}", GetSantriIbadahSunnahByGuru)
                       .WithName("GetSantriIbadahSunnahByGuru");
        }

        // GET: Ambil data ibadah sunnah milik user yang sedang login
        private static async Task<IResult> GetMyIbadahSunnah(
            ClaimsPrincipal user,
            [FromQuery] DateTime tanggal,
            IbadahSunnahServices service)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? user.FindFirst("sub")?.Value;
            if (!int.TryParse(userIdClaim, out int idUser))
                return Results.Unauthorized();

            var data = await service.GetByUserAndDateAsync(idUser, tanggal);
            return Results.Ok(new { status = "success", data });
        }

        // POST: Simpan daftar ibadah sunnah yang dikerjakan hari ini
        private static async Task<IResult> SaveIbadahSunnah(
            ClaimsPrincipal user,
            [FromBody] SaveIbadahSunnahRequest request,
            IbadahSunnahServices service)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? user.FindFirst("sub")?.Value;
            if (!int.TryParse(userIdClaim, out int idUser))
                return Results.Unauthorized();

            var isSaved = await service.SaveIbadahSunnahAsync(idUser, request.Tanggal, request.IdKategoriSunnahList);
            if (!isSaved)
                return Results.BadRequest(new { status = "error", message = "Gagal menyimpan ibadah sunnah" });

            return Results.Ok(new { status = "success", message = "Ibadah sunnah berhasil disimpan" });
        }

        // GET: Guru melihat ibadah sunnah milik santri tertentu
        private static async Task<IResult> GetSantriIbadahSunnahByGuru(
            int idSantri,
            [FromQuery] DateTime tanggal,
            IbadahSunnahServices service)
        {
            var data = await service.GetByUserAndDateAsync(idSantri, tanggal);
            return Results.Ok(new { status = "success", data });
        }
    }

    // DTO Request Body untuk simpan/update
    public class SaveIbadahSunnahRequest
    {
        public DateTime Tanggal { get; set; }
        public List<int> IdKategoriSunnahList { get; set; } = new();
    }
}