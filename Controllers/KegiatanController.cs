using Microsoft.AspNetCore.Mvc;
using Ramadhan_Digital.Models;
using Ramadhan_Digital.Services;
using System.Security.Claims;

namespace Ramadhan_Digital.Controllers
{
    public static class KegiatanController
    {
        public static void MapKegiatan(this WebApplication app)
        {
            var group = app.MapGroup("/api/v1/kegiatan")
                           .RequireAuthorization();

            //----ADMIN----
            // DELETE
            group.MapDelete("/{id:int}", DeleteKegiatanById)
                 .WithName("DeleteKegiatanById");

            //POST KEGIATAN
            group.MapPost("/", CreateKegiatan)
                 .WithName("CreateKegiatan");

            //----GURU----
            //GET by User ID
            group.MapGet("/user/{idUser:int}", GetKegiatanByUserId)
                 .WithName("GetKegiatanByUserId");

            //----SISWA----
            //POST MENGISI KEGIATAN
            group.MapPost("/register", RegisterUserKegiatan)
                 .WithName("RegisterUserKegiatan");
            // GET 
            group.MapGet("/", GetAllKegiatan)
                 .WithName("GetAllKegiatan");
            // GET by ID
            group.MapGet("/{id:int}", GetKegiatanById)
                 .WithName("GetKegiatanById");


        }

        private static async Task<IResult> GetAllKegiatan(
            KegiatanServices service)
        {
            var data = await service.GetAllAsync();

            return Results.Ok(new
            {
                status = "success",
                data
            });
        }

        private static async Task<IResult> GetKegiatanById(
            int id,
            KegiatanServices service)
        {
            var data = await service.GetByIdAsync(id);

            if (data == null)
            {
                return Results.NotFound(new
                {
                    status = "error",
                    message = "Kegiatan tidak ditemukan"
                });
            }

            return Results.Ok(new
            {
                status = "success",
                data
            });
        }

        private static async Task<IResult> GetKegiatanByUserId(
            int idUser,
            KegiatanServices service)
        {
            var data = await service.GetByUserIdAsync(idUser);

            return Results.Ok(new
            {
                status = "success",
                data
            });
        }

        private static async Task<IResult> CreateKegiatan(
            [FromBody] Kegiatan kegiatan,
            KegiatanServices service)
        {
            var isCreated = await service.CreateAsync(kegiatan);

            if (!isCreated)
            {
                return Results.BadRequest(new
                {
                    status = "error",
                    message = "Gagal menambahkan kegiatan"
                });
            }

            return Results.Ok(new
            {
                status = "success",
                message = "Kegiatan berhasil ditambahkan"
            });
        }

        private static async Task<IResult> RegisterUserKegiatan(
         [FromBody] KegiatanUser kegiatanUser,
         KegiatanServices service,
         HttpContext httpContext)
        {
            var userIdClaim = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Results.Unauthorized();
            }

            if (!int.TryParse(userIdClaim, out var userId))
            {
                return Results.Unauthorized();
            }

            kegiatanUser.IdUser = userId;

            var isRegistered = await service.RegisterUserAsync(kegiatanUser);

            if (!isRegistered)
            {
                return Results.BadRequest(new
                {
                    status = "error",
                    message = "Gagal Mengisi Kegiatan"
                });
            }

            return Results.Ok(new
            {
                status = "success",
                message = "Berhasil mengisi kegiatan"
            });
        }

        private static async Task<IResult> DeleteKegiatanById(
            int id,
            KegiatanServices service)
        {
            var isDeleted = await service.DeleteAsync(id);

            if (!isDeleted)
            {
                return Results.NotFound(new
                {
                    status = "error",
                    message = "Kegiatan tidak ditemukan atau gagal dihapus"
                });
            }

            return Results.Ok(new
            {
                status = "success",
                message = "Kegiatan berhasil dihapus"
            });
        }
    }
}