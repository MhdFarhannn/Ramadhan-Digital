using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Ramadhan_Digital.Services;

namespace Ramadhan_Digital.Controllers;

public static class IbadahSunnahController
{
    public static void MapIbadahSunnah(this WebApplication app)
    {
        var publicGroup = app
            .MapGroup("/api/v1/ibadah-sunnah")
            .RequireAuthorization();

        // Endpoint Siswa / User Sendiri
        publicGroup
            .MapGet("/", GetMyIbadahSunnah)
            .WithName("GetMyIbadahSunnah");

        publicGroup
            .MapPost("/", SaveIbadahSunnah)
            .WithName("SaveIbadahSunnah");

        // Endpoint Monitoring untuk Guru
        publicGroup
            .MapGet(
                "/monitoring/siswa/{idSiswa:int}",
                GetGuruIbadahSunnahBySiswa
            )
            .WithName("GetGuruIbadahSunnahBySiswa");
    }

    // ============================================================
    // GET: Ambil data ibadah sunnah milik user yang sedang login
    // ============================================================
    private static async Task<IResult> GetMyIbadahSunnah(
        ClaimsPrincipal user,
        [FromQuery] DateTime? tanggal,
        IbadahSunnahServices service)
    {
        var userIdClaim =
            user.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? user.FindFirst("sub")?.Value;

        if (!int.TryParse(userIdClaim, out int idUser))
        {
            return Results.Unauthorized();
        }

        // Jika tanggal tidak dikirim, gunakan tanggal hari ini
        var tanggalFilter = tanggal?.Date ?? DateTime.Today;

        var data = await service.GetByUserAndDateAsync(
            idUser,
            tanggalFilter
        );

        return Results.Ok(new
        {
            status = "success",
            tanggal = tanggalFilter.ToString("dd-MM-yyyy"),
            data
        });
    }

    
    public static async Task<IResult> SaveIbadahSunnah(
        ClaimsPrincipal user,
        [FromBody] SaveIbadahSunnahRequest request,
        IbadahSunnahServices service)
    {
        var userIdClaim =
            user.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? user.FindFirst("sub")?.Value;

        if (!int.TryParse(userIdClaim, out int idUser))
        {
            return Results.Unauthorized();
        }

        
        var tanggalHariIni = DateTime.Today;

        // Validasi request
        if (request.IdKategoriSunnahList == null)
        {
            return Results.BadRequest(new
            {
                status = "error",
                message = "Daftar ibadah sunnah tidak boleh null."
            });
        }

        // Hapus ID kategori yang duplikat
        var kategoriList = request.IdKategoriSunnahList
            .Distinct()
            .ToList();

        // ========================================================
        // Cek apakah siswa sudah pernah submit hari ini
        // ========================================================
        var alreadySaved = await service.HasSavedTodayAsync(
            idUser,
            tanggalHariIni
        );

        if (alreadySaved)
        {
            return Results.Conflict(new
            {
                status = "error",
                message =
                    "Ibadah sunnah hari ini sudah diinput. " +
                    "Anda dapat melakukan input kembali setelah pukul 00:00.",
                tanggal = tanggalHariIni.ToString("dd-MM-yyyy")
            });
        }

        // ========================================================
        // Simpan
        // ========================================================
        var result = await service.SaveIbadahSunnahAsync(
            idUser,
            tanggalHariIni,
            kategoriList
        );

        // Berhasil
        if (result == SaveIbadahResult.Success)
        {
            return Results.Ok(new
            {
                status = "success",
                message = "Ibadah sunnah berhasil disimpan.",
                tanggal = tanggalHariIni.ToString("dd-MM-yyyy")
            });
        }

        // Sudah pernah submit
        if (result == SaveIbadahResult.AlreadySaved)
        {
            return Results.Conflict(new
            {
                status = "error",
                message =
                    "Ibadah sunnah hari ini sudah diinput. " +
                    "Anda dapat melakukan input kembali setelah pukul 00:00.",
                tanggal = tanggalHariIni.ToString("dd-MM-yyyy")
            });
        }

        // Gagal
        return Results.BadRequest(new
        {
            status = "error",
            message = "Gagal menyimpan ibadah sunnah."
        });
    }

    // ============================================================
    // GET: Monitoring ibadah sunnah siswa
    // ============================================================
    private static async Task<IResult> GetGuruIbadahSunnahBySiswa(
        int idSiswa,
        [FromQuery] string? tanggal,
        IbadahSunnahServices service)
    {
        DateTime tanggalFilter;

        if (string.IsNullOrWhiteSpace(tanggal))
        {
            tanggalFilter = DateTime.Today;
        }
        else if (!DateTime.TryParseExact(
            tanggal,
            "dd-MM-yyyy",
            System.Globalization.CultureInfo.InvariantCulture,
            System.Globalization.DateTimeStyles.None,
            out tanggalFilter))
        {
            return Results.BadRequest(new
            {
                status = "error",
                message =
                    "Format tanggal harus dd-MM-yyyy. " +
                    "Contoh: 29-08-2026"
            });
        }

        var data = await service.GetByUser(
            idSiswa,
            tanggalFilter
        );

        return Results.Ok(new
        {
            status = "success",
            idSiswa,
            tanggal = tanggalFilter.ToString("dd-MM-yyyy"),
            data
        });
    }
}


public class SaveIbadahSunnahRequest
{
    public DateTime Tanggal { get; set; }

    public List<int> IdKategoriSunnahList { get; set; } = new();
}
