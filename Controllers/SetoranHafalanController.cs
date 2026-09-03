using Microsoft.AspNetCore.Mvc;
using Ramadhan_Digital.Models;
using Ramadhan_Digital.Services;

namespace Ramadhan_Digital.Controllers
{
    public static class SetoranHafalanController
    {
        public static void MapSetoranHafalan(this WebApplication app)
        {
            // ========================================================
            // SETORAN SURAH
            // ========================================================

            var surahGroup = app
                .MapGroup("/api/v1/setoran-surah")
                .RequireAuthorization();

            surahGroup.MapGet("/",GetAllSetoranSurah).WithName("GetAllSetoranSurah");

            surahGroup.MapGet("/{id:int}", GetSetoranSurahById).WithName("GetSetoranSurahById");

            surahGroup.MapGet("/user/{userId:int}", GetSetoranSurahByUserId).WithName("GetSetoranSurahByUserId");

            surahGroup.MapPost("/", CreateSetoranSurah).WithName("CreateSetoranSurah");

            surahGroup.MapPut("/{id:int}", UpdateSetoranSurah).WithName("UpdateSetoranSurah");

            surahGroup.MapDelete("/{id:int}", DeleteSetoranSurah).WithName("DeleteSetoranSurah");


            // ========================================================
            // SETORAN BACAAN SHOLAT
            // ========================================================

            var bacaanGroup = app.MapGroup("/api/v1/setoran-bacaan-sholat").RequireAuthorization();

            bacaanGroup.MapGet("/", GetAllSetoranBacaanSholat).WithName("GetAllSetoranBacaanSholat");

            bacaanGroup.MapGet("/{id:int}", GetSetoranBacaanSholatById).WithName("GetSetoranBacaanSholatById");

            bacaanGroup.MapGet("/user/{userId:int}", GetSetoranBacaanSholatByUserId).WithName("GetSetoranBacaanSholatByUserId");

            bacaanGroup.MapPost("/", CreateSetoranBacaanSholat).WithName("CreateSetoranBacaanSholat");

            bacaanGroup.MapPut("/{id:int}", UpdateSetoranBacaanSholat).WithName("UpdateSetoranBacaanSholat");

            bacaanGroup.MapDelete("/{id:int}", DeleteSetoranBacaanSholat).WithName("DeleteSetoranBacaanSholat");
        }


        // ============================================================
        // SETORAN SURAH
        // ============================================================

        private static async Task<IResult> GetAllSetoranSurah(
            SetoranHafalanServices service)
        {
            var data = await service.GetAllSurahAsync();

            return Results.Ok(new
            {
                status = "success",
                data
            });
        }


        private static async Task<IResult> GetSetoranSurahById(
            int id,
            SetoranHafalanServices service)
        {
            var data = await service.GetSurahByIdAsync(id);

            if (data == null)
            {
                return Results.NotFound(new
                {
                    status = "error",
                    message = "Data setoran surah tidak ditemukan"
                });
            }

            return Results.Ok(new
            {
                status = "success",
                data
            });
        }


        private static async Task<IResult> GetSetoranSurahByUserId(
            int userId,
            SetoranHafalanServices service)
        {
            var data = await service.GetSurahByUserIdAsync(userId);

            return Results.Ok(new
            {
                status = "success",
                data
            });
        }


        private static async Task<IResult> CreateSetoranSurah(
            [FromBody] SetoranHafalan setoran,
            SetoranHafalanServices service)
        {
            var result = await service.CreateSurahAsync(setoran);

            if (!result.Success)
            {
                return Results.BadRequest(new
                {
                    status = "error",
                    message = result.Message
                });
            }

            return Results.Ok(new
            {
                status = "success",
                message = result.Message
            });
        }


        private static async Task<IResult> UpdateSetoranSurah(
            int id,
            [FromBody] SetoranHafalan setoran,
            SetoranHafalanServices service)
        {
            var isUpdated = await service.UpdateSurahAsync(
                id,
                setoran
            );

            if (!isUpdated)
            {
                return Results.NotFound(new
                {
                    status = "error",
                    message = "Data setoran surah tidak ditemukan atau gagal diperbarui"
                });
            }

            return Results.Ok(new
            {
                status = "success",
                message = "Setoran surah berhasil diperbarui"
            });
        }


        private static async Task<IResult> DeleteSetoranSurah(
            int id,
            SetoranHafalanServices service)
        {
            var isDeleted = await service.DeleteSurahAsync(id);

            if (!isDeleted)
            {
                return Results.NotFound(new
                {
                    status = "error",
                    message = "Data setoran surah tidak ditemukan"
                });
            }

            return Results.Ok(new
            {
                status = "success",
                message = "Setoran surah berhasil dihapus"
            });
        }


        // ============================================================
        // SETORAN BACAAN SHOLAT
        // ============================================================

        private static async Task<IResult> GetAllSetoranBacaanSholat(
            SetoranHafalanServices service)
        {
            var data = await service.GetAllBacaanSholatAsync();

            return Results.Ok(new
            {
                status = "success",
                data
            });
        }


        private static async Task<IResult> GetSetoranBacaanSholatById(
            int id,
            SetoranHafalanServices service)
        {
            var data = await service.GetBacaanSholatByIdAsync(id);

            if (data == null)
            {
                return Results.NotFound(new
                {
                    status = "error",
                    message = "Data setoran bacaan sholat tidak ditemukan"
                });
            }

            return Results.Ok(new
            {
                status = "success",
                data
            });
        }


        private static async Task<IResult> GetSetoranBacaanSholatByUserId(
            int userId,
            SetoranHafalanServices service)
        {
            var data =
                await service.GetBacaanSholatByUserIdAsync(userId);

            return Results.Ok(new
            {
                status = "success",
                data
            });
        }


        private static async Task<IResult> CreateSetoranBacaanSholat(
            [FromBody] SetoranHafalan setoran,
            SetoranHafalanServices service)
        {
            var isCreated =
                await service.CreateBacaanSholatAsync(setoran);

            if (!isCreated)
            {
                return Results.BadRequest(new
                {
                    status = "error",
                    message = "Gagal menambahkan setoran bacaan sholat"
                });
            }

            return Results.Ok(new
            {
                status = "success",
                message = "Setoran bacaan sholat berhasil ditambahkan"
            });
        }


        private static async Task<IResult> UpdateSetoranBacaanSholat(
            int id,
            [FromBody] SetoranHafalan setoran,
            SetoranHafalanServices service)
        {
            var isUpdated =
                await service.UpdateBacaanSholatAsync(
                    id,
                    setoran
                );

            if (!isUpdated)
            {
                return Results.NotFound(new
                {
                    status = "error",
                    message =
                        "Data setoran bacaan sholat tidak ditemukan atau gagal diperbarui"
                });
            }

            return Results.Ok(new
            {
                status = "success",
                message = "Setoran bacaan sholat berhasil diperbarui"
            });
        }


        private static async Task<IResult> DeleteSetoranBacaanSholat(
            int id,
            SetoranHafalanServices service)
        {
            var isDeleted =
                await service.DeleteBacaanSholatAsync(id);

            if (!isDeleted)
            {
                return Results.NotFound(new
                {
                    status = "error",
                    message = "Data setoran bacaan sholat tidak ditemukan"
                });
            }

            return Results.Ok(new
            {
                status = "success",
                message = "Setoran bacaan sholat berhasil dihapus"
            });
        }
    }
}
