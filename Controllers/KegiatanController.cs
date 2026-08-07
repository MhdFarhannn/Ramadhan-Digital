using Microsoft.AspNetCore.Mvc;
using Ramadhan_Digital.Models;
using Ramadhan_Digital.Services;

namespace Ramadhan_Digital.Controllers
{
    public static class KegiatanController
    {
        public static void MapKegiatan(this WebApplication app)
        {
            var publicGroup = app.MapGroup("/api/v1/kegiatan").RequireAuthorization();

            publicGroup.MapGet("/", GetAllKegiatan).WithName("GetAllKegiatan");
            publicGroup.MapGet("/{id:int}", GetKegiatanById).WithName("GetKegiatanById");
            publicGroup.MapPost("/", CreateKegiatan).WithName("CreateKegiatan");
            publicGroup.MapPost("/register", RegisterUserKegiatan).WithName("RegisterUserKegiatan");
            publicGroup.MapGet("/user/{idUser:int}", GetKegiatanByUserId).WithName("GetKegiatanByUserId");
        }

        private static async Task<IResult> GetAllKegiatan(KegiatanServices service)
        {
            var data = await service.GetAllAsync();
            return Results.Ok(new { status = "success", data });
        }

        private static async Task<IResult> GetKegiatanById(int id, KegiatanServices service)
        {
            var data = await service.GetByIdAsync(id);
            if (data == null)
                return Results.NotFound(new { status = "error", message = "Kegiatan tidak ditemukan" });

            return Results.Ok(new { status = "success", data });
        }

        private static async Task<IResult> CreateKegiatan([FromBody] Kegiatan kegiatan, KegiatanServices service)
        {
            var isCreated = await service.CreateAsync(kegiatan);
            if (!isCreated)
                return Results.BadRequest(new { status = "error", message = "Gagal menambahkan kegiatan" });

            return Results.Ok(new { status = "success", message = "Kegiatan berhasil ditambahkan" });
        }

        private static async Task<IResult> RegisterUserKegiatan([FromBody] KegiatanUser kegiatanUser, KegiatanServices service)
        {
            var isRegistered = await service.RegisterUserAsync(kegiatanUser);
            if (!isRegistered)
                return Results.BadRequest(new { status = "error", message = "Gagal mendaftarkan user ke kegiatan" });

            return Results.Ok(new { status = "success", message = "Berhasil mendaftar kegiatan" });
        }

        private static async Task<IResult> GetKegiatanByUserId(int idUser, KegiatanServices service)
        {
            var data = await service.GetByUserIdAsync(idUser);
            return Results.Ok(new { status = "success", data });
        }
    }
}