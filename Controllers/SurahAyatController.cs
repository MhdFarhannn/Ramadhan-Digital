using Ramadhan_Digital.Models;
using Ramadhan_Digital.Services;

namespace Ramadhan_Digital.Controllers;

public static class SurahAyatController
{
    public static void MapSurahAyat(this WebApplication app)
    {
        var publicGroup = app.MapGroup("/api/v1/quran");

        var adminGroup = app.MapGroup("/api/v1/quran")
                            .RequireAuthorization(Policies.Admin);
        


        // =========================
        // SURAH
        // =========================

        publicGroup.MapGet("/surah", GetAllSurah)
            .WithName("GetAllSurah");

        publicGroup.MapGet("/surah/{id:int}", GetSurahById)
            .WithName("GetSurahById");

        publicGroup.MapGet("/surah/nomor/{nomor:int}", GetSurahByNomor)
            .WithName("GetSurahByNomor");


        adminGroup.MapPost("/surah", CreateSurah)
            .WithName("CreateSurah");

        adminGroup.MapPut("/surah/{id:int}", UpdateSurah)
            .WithName("UpdateSurah");

        adminGroup.MapDelete("/surah/{id:int}", DeleteSurah)
            .WithName("DeleteSurah");



        // =========================
        // AYAT
        // =========================

        publicGroup.MapGet("/ayat", GetAllAyat)
            .WithName("GetAllAyat");

        publicGroup.MapGet("/ayat/{id:int}", GetAyatById)
            .WithName("GetAyatById");

        publicGroup.MapGet("/ayat/surah/{idSurah:int}", GetAyatBySurah)
            .WithName("GetAyatBySurah");

        publicGroup.MapGet(
                "/ayat/surah/{idSurah:int}/nomor/{nomor:int}",
                GetAyatBySurahAndNomor)
            .WithName("GetAyatBySurahAndNomor");


        adminGroup.MapPost("/ayat", CreateAyat)
            .WithName("CreateAyat");

        adminGroup.MapPost("/ayat/bulk", CreateAyatBulk)
    .WithName("CreateAyatBulk");

        adminGroup.MapPut("/ayat/{id:int}", UpdateAyat)
            .WithName("UpdateAyat");

        adminGroup.MapDelete("/ayat/{id:int}", DeleteAyat);
    }



    // =========================
    // SURAH HANDLER
    // =========================

    private static async Task<IResult> GetAllSurah(
        SurahServices service)
    {
        try
        {
            var data = await service.GetAllAsync();
            return Results.Ok(data);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }


    private static async Task<IResult> GetSurahById(
        int id,
        SurahServices service)
    {
        try
        {
            var data = await service.GetByIdAsync(id);

            return data == null
                ? Results.NotFound()
                : Results.Ok(data);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }


    private static async Task<IResult> GetSurahByNomor(
        int nomor,
        SurahServices service)
    {
        try
        {
            var data = await service.GetByNomorAsync(nomor);

            return data == null
                ? Results.NotFound()
                : Results.Ok(data);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }


    private static async Task<IResult> CreateSurah(
        Surah surah,
        SurahServices service)
    {
        try
        {
            var result = await service.CreateAsync(surah);

            return result
                ? Results.Created("", surah)
                : Results.BadRequest();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }


    private static async Task<IResult> UpdateSurah(
        int id,
        Surah surah,
        SurahServices service)
    {
        try
        {
            surah.Id = id;

            var result = await service.UpdateAsync(surah);

            return result
                ? Results.Ok(new
                {
                    message = "Surah berhasil diperbarui"
                })
                : Results.NotFound();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }


    private static async Task<IResult> DeleteSurah(
        int id,
        SurahServices service)
    {
        try
        {
            var result = await service.DeleteAsync(id);

            return result
                ? Results.Ok(new
                {
                    message = "Surah berhasil dihapus"
                })
                : Results.NotFound();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }



    // =========================
    // AYAT HANDLER
    // =========================

    private static async Task<IResult> GetAllAyat(
        AyatServices service)
    {
        try
        {
            var data = await service.GetAllAsync();
            return Results.Ok(data);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }


    private static async Task<IResult> GetAyatById(
        int id,
        AyatServices service)
    {
        try
        {
            var data = await service.GetByIdAsync(id);

            return data == null
                ? Results.NotFound()
                : Results.Ok(data);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }


    private static async Task<IResult> GetAyatBySurah(
        int idSurah,
        AyatServices service)
    {
        try
        {
            var data = await service.GetBySurahAsync(idSurah);

            return Results.Ok(data);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }


    private static async Task<IResult> GetAyatBySurahAndNomor(
        int idSurah,
        int nomor,
        AyatServices service)
    {
        try
        {
            var data = await service
                .GetBySurahAndNomorAsync(idSurah, nomor);

            return data == null
                ? Results.NotFound()
                : Results.Ok(data);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }


    private static async Task<IResult> CreateAyat(
        Ayat ayat,
        AyatServices service)
    {
        try
        {
            var result = await service.CreateAsync(ayat);

            return result
                ? Results.Created("", ayat)
                : Results.BadRequest();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }


    private static async Task<IResult> UpdateAyat(
        int id,
        Ayat ayat,
        AyatServices service)
    {
        try
        {
            ayat.Id = id;

            var result = await service.UpdateAsync(ayat);

            return result
                ? Results.Ok(new
                {
                    message = "Ayat berhasil diperbarui"
                })
                : Results.NotFound();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> CreateAyatBulk(
    List<Ayat> ayatList,
    AyatServices service)
    {
        try
        {
            if (ayatList == null || ayatList.Count == 0)
            {
                return Results.BadRequest(new
                {
                    message = "List ayat tidak boleh kosong",
                    status = 400
                });
            }

            int successCount = 0;
            int failureCount = 0;

            foreach (var ayat in ayatList)
            {
                try
                {
                    if (ayat.IdSurah <= 0 ||
                        ayat.Nomor <= 0 ||
                        string.IsNullOrWhiteSpace(ayat.Arab) ||
                        string.IsNullOrWhiteSpace(ayat.Terjemah))
                    {
                        failureCount++;
                        continue;
                    }

                    var result = await service.CreateAsync(ayat);
                    if (result)
                        successCount++;
                    else
                        failureCount++;
                }
                catch
                {
                    failureCount++;
                }
            }

            return Results.Ok(new
            {
                message = $"Berhasil menambahkan {successCount} ayat, gagal {failureCount}",
                successCount = successCount,
                failureCount = failureCount,
                totalRequested = ayatList.Count,
                status = 200
            });
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> DeleteAyat(
        int id,
        AyatServices service)
    {
        try
        {
            var result = await service.DeleteAsync(id);

            return result
                ? Results.Ok(new
                {
                    message = "Ayat berhasil dihapus"
                })
                : Results.NotFound();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
}
