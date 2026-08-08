using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Ramadhan_Digital.Services;

namespace Ramadhan_Digital.Controllers
{
    public static class AbsensiController
    {
        public static void MapAbsensi(this WebApplication app)
        {
            var publicGroup = app.MapGroup("/api/v1/absensi").RequireAuthorization();

            // GET: Guru mengambil daftar siswa + status absensi di kelas tertentu
            publicGroup.MapGet("/kelas/{idKelas:int}", GetAbsensiKelas)
                       .WithName("GetAbsensiKelas");

            // POST: Guru menyimpan/menginput absensi seluruh siswa dalam kelas
            publicGroup.MapPost("/kelas", SaveAbsensiKelasByGuru)
                       .WithName("SaveAbsensiKelasByGuru");
        }

        // GET: Ambil daftar absensi siswa di kelas tertentu
        private static async Task<IResult> GetAbsensiKelas(
            int idKelas,
            [FromQuery] DateTime? tanggal,
            AbsensiServices service)
        {
            DateTime targetDate = tanggal ?? DateTime.Today;

            var data = await service.GetAbsensiByKelasAndDateAsync(idKelas, targetDate);
            return Results.Ok(new { status = "success", data });
        }

        // POST: Simpan absensi massal siswa oleh Guru
        private static async Task<IResult> SaveAbsensiKelasByGuru(
            ClaimsPrincipal user,
            [FromBody] SaveAbsensiKelasRequest request,
            AbsensiServices service)
        {
            if (request.SiswaList == null || !request.SiswaList.Any())
                return Results.BadRequest(new { status = "error", message = "Daftar absensi siswa tidak boleh kosong" });

            var isSaved = await service.SaveAbsensiKelasAsync(request.Tanggal, request.SiswaList);
            if (!isSaved)
                return Results.BadRequest(new { status = "error", message = "Gagal menyimpan data absensi kelas" });

            return Results.Ok(new { status = "success", message = "Absensi kelas berhasil disimpan" });
        }
    }

    // DTO Request Body untuk Guru
    public class SaveAbsensiKelasRequest
    {
        public DateTime Tanggal { get; set; }
        public List<DetailAbsensiSiswa> SiswaList { get; set; } = new();
    }
}