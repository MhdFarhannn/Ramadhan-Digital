using Microsoft.AspNetCore.Mvc;
using Ramadhan_Digital.Models;
using Ramadhan_Digital.Services;


namespace Ramadhan_Digital.Controllers
{
    public static class SetoranHafalanController
    {
        public static void MapSetoranHafalan(this WebApplication app)
        {
            var publicGroup = app.MapGroup("/api/v1/setoran-hafalan").RequireAuthorization();

            publicGroup.MapGet("/", GetAllSetoran).WithName("GetAllSetoran");
            publicGroup.MapGet("/{id:int}", GetSetoranById).WithName("GetSetoranById");
            publicGroup.MapGet("/user/{idUser:int}", GetSetoranByUserId).WithName("GetSetoranByUserId");
            publicGroup.MapPost("/", CreateSetoran).WithName("CreateSetoran");
            publicGroup.MapPut("/{id:int}", UpdateSetoran).WithName("UpdateSetoran");
            publicGroup.MapDelete("/{id:int}", DeleteSetoran).WithName("DeleteSetoran");
        }

        private static async Task<IResult> GetAllSetoran(SetoranHafalanServices service)
        {
            var data = await service.GetAllAsync();
            return Results.Ok(new { status = "success", data });
        }

        private static async Task<IResult> GetSetoranById(int id, SetoranHafalanServices service)
        {
            var data = await service.GetByIdAsync(id);
            if (data == null)
                return Results.NotFound(new { status = "error", message = "Data setoran tidak ditemukan" });

            return Results.Ok(new { status = "success", data });
        }

        private static async Task<IResult> GetSetoranByUserId(int idUser, SetoranHafalanServices service)
        {
            var data = await service.GetByUserIdAsync(idUser);
            return Results.Ok(new { status = "success", data });
        }

        private static async Task<IResult> CreateSetoran([FromBody] SetoranHafalan setoran, SetoranHafalanServices service)
        {
            var isCreated = await service.CreateAsync(setoran);
            if (!isCreated)
                return Results.BadRequest(new { status = "error", message = "Gagal menambahkan setoran hafalan" });

            return Results.Ok(new { status = "success", message = "Setoran hafalan berhasil ditambahkan" });
        }

        private static async Task<IResult> UpdateSetoran(int id, [FromBody] SetoranHafalan setoran, SetoranHafalanServices service)
        {
            var isUpdated = await service.UpdateAsync(id, setoran);
            if (!isUpdated)
                return Results.NotFound(new { status = "error", message = "Data setoran tidak ditemukan atau gagal diperbarui" });

            return Results.Ok(new { status = "success", message = "Setoran hafalan berhasil diperbarui" });
        }

        private static async Task<IResult> DeleteSetoran(int id, SetoranHafalanServices service)
        {
            var isDeleted = await service.DeleteAsync(id);
            if (!isDeleted)
                return Results.NotFound(new { status = "error", message = "Data setoran tidak ditemukan" });

            return Results.Ok(new { status = "success", message = "Setoran hafalan berhasil dihapus" });
        }
    }
}