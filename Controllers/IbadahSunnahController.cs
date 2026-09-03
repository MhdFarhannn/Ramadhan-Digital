using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Ramadhan_Digital.Models;
using Ramadhan_Digital.Services;

namespace Ramadhan_Digital.Controllers;

public static class IbadahSunnahController
{
    public static void MapIbadahSunnah(this WebApplication app)
    {
        var publicGroup = app
            .MapGroup("/api/v1/ibadah-sunnah")
            .RequireAuthorization();


        // ========================================================
        // SISWA / USER SENDIRI
        // ========================================================

        publicGroup
            .MapGet("/", GetMyIbadahSunnah)
            .WithName("GetMyIbadahSunnah");

        publicGroup
            .MapPost("/", SaveIbadahSunnah)
            .WithName("SaveIbadahSunnah");


        // ========================================================
        // MONITORING GURU
        // ========================================================

        publicGroup
            .MapGet(
                "/monitoring/siswa/{idSiswa:int}",
                GetGuruIbadahSunnahBySiswa
            )
            .WithName("GetGuruIbadahSunnahBySiswa");
    }


    // ============================================================
    // GET:
    // Ambil data ibadah sunnah milik user yang sedang login
    // ============================================================
    private static async Task<IResult> GetMyIbadahSunnah(
        ClaimsPrincipal user,
        [FromQuery] DateTime? tanggal,
        IbadahSunnahServices service)
    {
        // ========================================================
        // AMBIL USER ID DARI JWT
        // ========================================================
        var userIdClaim =
            user.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? user.FindFirst("sub")?.Value;

        if (!int.TryParse(userIdClaim, out int idUser))
        {
            return Results.Unauthorized();
        }


        // ========================================================
        // JIKA TANGGAL TIDAK DIKIRIM
        // GUNAKAN TANGGAL HARI INI
        // ========================================================
        var tanggalFilter =
            tanggal?.Date ?? DateTime.Today;


        // ========================================================
        // AMBIL DATA
        // ========================================================
        var data =
            await service.GetByUserAndDateAsync(
                idUser,
                tanggalFilter
            );


        // ========================================================
        // RESPONSE
        // ========================================================
        return Results.Ok(new
        {
            status = "success",

            tanggal =
                tanggalFilter.ToString("dd-MM-yyyy"),

            data
        });
    }


    // ============================================================
    // POST:
    // SIMPAN IBADAH SUNNAH
    // ============================================================
    public static async Task<IResult> SaveIbadahSunnah(
        ClaimsPrincipal user,
        [FromBody] SaveIbadahSunnahRequest request,
        IbadahSunnahServices service)
    {
        // ========================================================
        // AMBIL USER ID
        // ========================================================
        var userIdClaim =
            user.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? user.FindFirst("sub")?.Value;

        if (!int.TryParse(userIdClaim, out int idUser))
        {
            return Results.Unauthorized();
        }


        // ========================================================
        // TANGGAL SELALU HARI INI
        // ========================================================
        var tanggalHariIni =
            DateTime.Today;


        // ========================================================
        // VALIDASI REQUEST
        // ========================================================
        if (request == null)
        {
            return Results.BadRequest(new
            {
                status = "error",
                message = "Request tidak boleh null."
            });
        }


        if (request.IdKategoriSunnahList == null)
        {
            return Results.BadRequest(new
            {
                status = "error",
                message =
                    "Daftar ibadah sunnah tidak boleh null."
            });
        }


        // ========================================================
        // HAPUS ID KATEGORI DUPLIKAT
        // ========================================================
        var kategoriList =
            request.IdKategoriSunnahList
                .Distinct()
                .ToList();


        // ========================================================
        // CEK APAKAH SUDAH SUBMIT HARI INI
        // ========================================================
        var alreadySaved =
            await service.HasSavedTodayAsync(
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
                    "Anda dapat melakukan input kembali " +
                    "setelah pukul 00:00.",

                tanggal =
                    tanggalHariIni.ToString("dd-MM-yyyy")
            });
        }


        // ========================================================
        // SIMPAN
        // ========================================================
        var result =
            await service.SaveIbadahSunnahAsync(
                idUser,
                tanggalHariIni,
                kategoriList
            );


        // ========================================================
        // BERHASIL
        // ========================================================
        if (result == SaveIbadahResult.Success)
        {
            return Results.Ok(new
            {
                status = "success",

                message =
                    "Ibadah sunnah berhasil disimpan.",

                tanggal =
                    tanggalHariIni.ToString("dd-MM-yyyy")
            });
        }


        // ========================================================
        // SUDAH PERNAH SUBMIT
        // ========================================================
        if (result ==
            SaveIbadahResult.AlreadySaved)
        {
            return Results.Conflict(new
            {
                status = "error",

                message =
                    "Ibadah sunnah hari ini sudah diinput. " +
                    "Anda dapat melakukan input kembali " +
                    "setelah pukul 00:00.",

                tanggal =
                    tanggalHariIni.ToString("dd-MM-yyyy")
            });
        }


        // ========================================================
        // GAGAL
        // ========================================================
        return Results.BadRequest(new
        {
            status = "error",

            message =
                "Gagal menyimpan ibadah sunnah."
        });
    }


    // ============================================================
    // GET:
    // MONITORING IBADAH SUNNAH SISWA
    //
    // Contoh:
    //
    // GET
    // /api/v1/ibadah-sunnah/monitoring/siswa/13
    //
    // atau:
    //
    // GET
    // /api/v1/ibadah-sunnah/monitoring/siswa/13?tanggal=02-09-2026
    // ============================================================
    private static async Task<IResult> GetGuruIbadahSunnahBySiswa(
        int idSiswa,
        [FromQuery] string? tanggal,
        IbadahSunnahServices service)
    {
        DateTime tanggalFilter;


        // ========================================================
        // JIKA TANGGAL KOSONG
        // GUNAKAN HARI INI
        // ========================================================
        if (string.IsNullOrWhiteSpace(tanggal))
        {
            tanggalFilter =
                DateTime.Today;
        }

        // ========================================================
        // PARSE TANGGAL dd-MM-yyyy
        // ========================================================
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


        // ========================================================
        // AMBIL DATA MONITORING
        // ========================================================
        var data =
            await service.GetByUser(
                idSiswa,
                tanggalFilter
            );


        // ========================================================
        // HITUNG RINGKASAN
        // ========================================================
        var dataList =
            data.ToList();


        var totalKategori =
            dataList.Count;


        var sudahDilakukan =
            dataList.Count(x =>
                x.SudahDilakukan);


        var belumDilakukan =
            totalKategori -
            sudahDilakukan;


        var persentase =
            totalKategori > 0
                ? Math.Round(
                    (double)sudahDilakukan /
                    totalKategori *
                    100,
                    2
                )
                : 0;


        // ========================================================
        // RESPONSE
        // ========================================================
        return Results.Ok(new
        {
            status = "success",

            idSiswa,

            tanggal =
                tanggalFilter.ToString("dd-MM-yyyy"),

            ringkasan = new
            {
                totalKategori,

                sudahDilakukan,

                belumDilakukan,

                persentase
            },

            data = dataList
        });
    }
}
