using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Ramadhan_Digital.Services;

namespace Ramadhan_Digital.Controllers
{
    public static class AbsensiController
    {
        public static void MapAbsensi(this WebApplication app)
        {
            var publicGroup = app.MapGroup("/api/v1/absensi")
                                 .RequireAuthorization();

            // ========================================================
            // GURU
            // ========================================================

            // GET:
            // Mengambil daftar siswa + status absensi berdasarkan kelas
            publicGroup.MapGet(
                "/kelas/{idKelas:int}",
                GetAbsensiKelas
            ).WithName("GetAbsensiKelas");

            // GET:
            // Rekap seluruh absensi kelas
            publicGroup.MapGet(
                "/kelas/{idKelas:int}/rekap",
                GetRekapAbsensiKelas
            ).WithName("GetRekapAbsensiKelas");

            // POST:
            // Guru menyimpan absensi seluruh siswa dalam kelas
            // Hanya boleh 1 kali per hari
            publicGroup.MapPost(
                "/kelas",
                SaveAbsensiKelasByGuru
            ).WithName("SaveAbsensiKelasByGuru");
        }

        // ============================================================
        // GET ABSENSI KELAS
        // ============================================================

        private static async Task<IResult> GetAbsensiKelas(
            int idKelas,
            [FromQuery] DateTime? tanggal,
            AbsensiServices service)
        {
            DateTime targetDate = tanggal?.Date ?? DateTime.Today;

            var data = await service.GetAbsensiByKelasAndDateAsync(
                idKelas,
                targetDate
            );

            return Results.Ok(new
            {
                status = "success",
                data
            });
        }

        // ============================================================
        // POST ABSENSI KELAS OLEH GURU
        //
        // RULE:
        // 1. Hanya guru yang memiliki kelas tersebut
        // 2. Hanya bisa input untuk tanggal hari ini
        // 3. Satu kelas hanya bisa input 1 kali sehari
        // ============================================================

        private static async Task<IResult> SaveAbsensiKelasByGuru(
            ClaimsPrincipal user,
            [FromBody] SaveAbsensiKelasRequest request,
            AbsensiServices service)
        {
            // ========================================================
            // VALIDASI DAFTAR SISWA
            // ========================================================

            if (request.SiswaList == null || !request.SiswaList.Any())
            {
                return Results.BadRequest(new
                {
                    status = "error",
                    message = "Daftar absensi siswa tidak boleh kosong"
                });
            }

            // ========================================================
            // VALIDASI ID KELAS
            // ========================================================

            if (request.IdKelas <= 0)
            {
                return Results.BadRequest(new
                {
                    status = "error",
                    message = "ID kelas tidak valid"
                });
            }

            // ========================================================
            // AMBIL ID KELAS DARI TOKEN
            // ========================================================

            var idKelasClaim = user.FindFirst("IdKelas")?.Value;

            if (!int.TryParse(idKelasClaim, out int idKelasUser))
            {
                return Results.Unauthorized();
            }

            // Guru hanya boleh menginput kelas yang dimilikinya
            if (idKelasUser != request.IdKelas)
            {
                return Results.Forbid();
            }

            // ========================================================
            // HANYA BOLEH INPUT UNTUK HARI INI
            // ========================================================

            var today = DateTime.Today;

            if (request.Tanggal.Date != today)
            {
                return Results.BadRequest(new
                {
                    status = "error",
                    message = "Absensi hanya dapat diinput untuk tanggal hari ini"
                });
            }

            // ========================================================
            // CEK APAKAH KELAS SUDAH ABSEN HARI INI
            // ========================================================

            var alreadySaved =
                await service.IsAbsensiKelasSudahAdaAsync(
                    request.IdKelas,
                    request.Tanggal
                );

            if (alreadySaved)
            {
                return Results.BadRequest(new
                {
                    status = "error",
                    message = "Absensi kelas hari ini sudah diinput"
                });
            }

            // ========================================================
            // SIMPAN ABSENSI
            // ========================================================

            var isSaved =
                await service.SaveAbsensiKelasAsync(
                    request.IdKelas,
                    request.Tanggal,
                    request.SiswaList
                );

            if (!isSaved)
            {
                return Results.BadRequest(new
                {
                    status = "error",
                    message = "Gagal menyimpan data absensi kelas"
                });
            }

            return Results.Ok(new
            {
                status = "success",
                message = "Absensi kelas berhasil disimpan"
            });
        }

        // ============================================================
        // GET REKAP ABSENSI KELAS
        // ============================================================

        private static async Task<IResult> GetRekapAbsensiKelas(
            int idKelas,
            AbsensiServices service)
        {
            var data =
                await service.GetRekapAbsensiKelasAsync(idKelas);

            return Results.Ok(new
            {
                status = "success",
                data
            });
        }
    }

    // ================================================================
    // REQUEST ABSENSI KELAS
    // ================================================================

    public class SaveAbsensiKelasRequest
    {
        public int IdKelas { get; set; }

        public DateTime Tanggal { get; set; }

        public List<DetailAbsensiSiswa> SiswaList { get; set; } = new();
    }
}
