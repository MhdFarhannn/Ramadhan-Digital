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

        publicGroup
            .MapGet(
                "/monitoring/siswa/{idSiswa:int}/rekap",
                GetRiwayatIbadahSunnahSiswa
            )
            .WithName("GetRiwayatIbadahSunnahSiswa");
    }


    // ============================================================
    // GET:
    // Ambil data ibadah sunnah milik user yang sedang login
    // ============================================================
    private static async Task<IResult> GetMyIbadahSunnah(
        ClaimsPrincipal user,
        [FromQuery] DateOnly? tanggal,
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
        //
        // Kolom ibadah_sunnah.tanggal bertipe date.
        // Default: tanggal lokal server (tanpa konversi UTC).
        // ========================================================
        var tanggalFilter =
            tanggal ?? DateOnly.FromDateTime(DateTime.Today);


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
        //
        // DateOnly diserialisasi otomatis menjadi "yyyy-MM-dd".
        // ========================================================
        return Results.Ok(new
        {
            status = "success",

            tanggal = tanggalFilter,

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
        //
        // Tanggal lokal server, tanpa konversi UTC,
        // agar tanggal tidak bergeser.
        // ========================================================
        var tanggalHariIni =
            DateOnly.FromDateTime(DateTime.Today);


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

                tanggal = tanggalHariIni
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

                tanggal = tanggalHariIni
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

                tanggal = tanggalHariIni
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
    // Kontrak tanggal date-only: yyyy-MM-dd
    //
    // Contoh:
    //
    // GET
    // /api/v1/ibadah-sunnah/monitoring/siswa/13
    //
    // atau:
    //
    // GET
    // /api/v1/ibadah-sunnah/monitoring/siswa/13?tanggal=2026-09-02
    // ============================================================
    private static async Task<IResult> GetGuruIbadahSunnahBySiswa(
        int idSiswa,
        [FromQuery] DateOnly? tanggal,
        IbadahSunnahServices service)
    {
        // ========================================================
        // JIKA TANGGAL KOSONG
        // GUNAKAN HARI INI
        //
        // Tanggal lokal server, tanpa konversi UTC.
        // ========================================================
        var tanggalFilter =
            tanggal ?? DateOnly.FromDateTime(DateTime.Today);


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
        //
        // DateOnly diserialisasi otomatis menjadi "yyyy-MM-dd".
        // ========================================================
        return Results.Ok(new
        {
            status = "success",

            idSiswa,

            tanggal = tanggalFilter,

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
    // ============================================================
    // GET:
    // SELURUH RIWAYAT IBADAH SUNNAH SISWA
    //
    // Contoh:
    // GET /api/v1/ibadah-sunnah/monitoring/siswa/154/rekap
    // ============================================================
    private static async Task<IResult> GetRiwayatIbadahSunnahSiswa(
            int idSiswa,
            IbadahSunnahServices service)
        {
            // ========================================================
            // AMBIL SELURUH RIWAYAT IBADAH SUNNAH SISWA
            // ========================================================
            var data = await service.GetByUserAsync(idSiswa);
        
            var dataList = data.ToList();
        
            // ========================================================
            // RESPONSE
            // ========================================================
            return Results.Ok(new
            {
                status = "success",
        
                idSiswa,
        
                total = dataList.Count,
        
                data = dataList
            });
        }

    
}
