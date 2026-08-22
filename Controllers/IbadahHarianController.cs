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
            var group = app.MapGroup("/api/v1/ibadah-harian").RequireAuthorization();
            // Endpoint Siswa: Simpan / Update data ibadah harian
            group.MapPost("/", SaveIbadahHarian).WithName("SaveIbadahHarian");

            // Endpoint Siswa: Ambil data ibadah hari tertentu
            group.MapGet("/", GetByUserAndDate).WithName("GetIbadahHarianByUserAndDate");

            // Endpoint Siswa: Riwayat ibadah siswa (rentang tanggal)
            group.MapGet("/riwayat", GetRiwayatSiswa).WithName("GetRiwayatSiswaIbadah");

            // Endpoint Guru / Admin: Monitoring ibadah siswa 1 kelas pada tanggal tertentu
            group.MapGet("/monitoring/kelas/{idKelas:int}", GetMonitoringKelas).WithName("GetMonitoringKelasIbadah");

        }

        private static async Task<IResult> GetByUserAndDate(
            [FromQuery] DateTime? tanggal,
            ClaimsPrincipal user,
            IbadahHarianServices service)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value 
                              ?? user.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int idUser))
            {
                return Results.Unauthorized();
            }

            DateTime targetDate = tanggal?.Date ?? DateTime.Today;
            var data = await service.GetByUserAndDateAsync(idUser, targetDate);

            if (data == null)
            {
                return Results.NotFound(new { 
                    status = "error", 
                    message = "Data ibadah harian tidak ditemukan" 
                });
            }

            return Results.Ok(new { status = "success", data });
        }

        private static async Task<IResult> SaveIbadahHarian(
            [FromBody] IbadahHarian model,
            ClaimsPrincipal user,
            IbadahHarianServices service)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value 
                              ?? user.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int idUser))
            {
                return Results.Unauthorized();
            }

            // Bind id_user dari token JWT demi keamanan data
            model.IdUser = idUser;

            var result = await service.SaveIbadahHarianAsync(model);
            if (!result.Success)
            {
                return Results.BadRequest(new { 
                    status = "error", 
                    message = result.Message 
                });
            }

            return Results.Ok(new { 
                status = "success", 
                message = result.Message 
            });
        }

        private static async Task<IResult> GetRiwayatSiswa(
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate,
            ClaimsPrincipal user,
            IbadahHarianServices service)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value 
                              ?? user.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int idUser))
            {
                return Results.Unauthorized();
            }

            // Default rentang: 30 hari ke belakang dari hari ini jika parameter tidak diisi
            DateTime end = endDate?.Date ?? DateTime.Today;
            DateTime start = startDate?.Date ?? end.AddDays(-30);

            var data = await service.GetRiwayatSiswaAsync(idUser, start, end);
            return Results.Ok(new { status = "success", data });
        }

        private static async Task<IResult> GetMonitoringKelas(
            int idKelas,
            [FromQuery] DateTime? tanggal,
            IbadahHarianServices service)
        {
            DateTime targetDate = tanggal?.Date ?? DateTime.Today;
            var data = await service.GetMonitoringKelasAsync(idKelas, targetDate);

            return Results.Ok(new { status = "success", data });
        }
    }
}