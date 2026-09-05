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

            // Endpoint Guru / Admin: Monitoring ibadah siswa 1 siswa pada rentang tanggal tertentu
            group.MapGet("/monitoring/siswa/{idSiswa:int}", GetMonitoringSiswa).WithName("GetMonitoringSiswaIbadah");

            // Endpoint Guru / Admin: Monitoring seluruh ibadah 1 siswa  
            group.MapGet("/monitoring/siswa/{idSiswa:int}/rekap", GetMonitoringSiswaSemua).WithName("GetMonitoringSiswaSemuaIbadah");
        }

        private static async Task<IResult> GetByUserAndDate(
            [FromQuery] DateOnly? tanggal,
            ClaimsPrincipal user,
            IbadahHarianServices service)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value 
                              ?? user.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int idUser))
            {
                return Results.Unauthorized();
            }

            // Kolom ibadah_harian.tanggal bertipe date.
            // Default: tanggal lokal server (tanpa konversi UTC).
            DateOnly targetDate =
                tanggal ?? DateOnly.FromDateTime(DateTime.Today);

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
            [FromQuery] DateOnly? startDate,
            [FromQuery] DateOnly? endDate,
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
            DateOnly end = endDate ?? DateOnly.FromDateTime(DateTime.Today);
            DateOnly start = startDate ?? end.AddDays(-30);

            var data = await service.GetRiwayatSiswaAsync(idUser, start, end);
            return Results.Ok(new { status = "success", data });
        }

        private static async Task<IResult> GetMonitoringKelas(
            int idKelas,
            [FromQuery] DateOnly? tanggal,
            IbadahHarianServices service)
        {
            DateOnly targetDate =
                tanggal ?? DateOnly.FromDateTime(DateTime.Today);

            var data = await service.GetMonitoringKelasAsync(idKelas, targetDate);

            return Results.Ok(new { status = "success", data });
        }

        private static async Task<IResult> GetMonitoringSiswa(
            int idSiswa,
            [FromQuery] DateOnly? startDate,
            [FromQuery] DateOnly? endDate,
            IbadahHarianServices service)
        {
            var data = await service.GetRiwayatSiswaAsync(idSiswa, startDate, endDate);
            return Results.Ok(new { status = "success", data });
        }

        private static async Task<IResult> GetRiwayatPerSiswa(
            int idSiswa,
            [FromQuery] DateOnly? startDate,
            [FromQuery] DateOnly? endDate,
            IbadahHarianServices service)
        {
            var data = await service.GetRiwayatPerSiswaAsync(idSiswa, startDate, endDate);
            return Results.Ok(new { status = "success", data });
        }

        private static async Task<IResult> GetMonitoringSiswaSemua(
            int idSiswa,
            [FromQuery] DateOnly? startDate,
            [FromQuery] DateOnly? endDate,
            IbadahHarianServices service)
        {
            var data = await service.GetMonitoringSiswaSemuaAsync(
                idSiswa,
                startDate,
                endDate
            );
        
            return Results.Ok(new
            {
                status = "success",
                data
            });
        }


    }
}