using Ramadhan_Digital.Models;
using Ramadhan_Digital.Services;

namespace Ramadhan_Digital.Controllers
{
    public static class KelasController
    {
        public static void MapKelas(this WebApplication app)
        {
            var publicGroup = app.MapGroup("/api/v1/kelas");
            publicGroup.MapGet("/", GetAllKelas).WithName("GetAllKelas");
            publicGroup.MapGet("/{id:int}", GetKelasById).WithName("GetKelasById");
            publicGroup.MapPost("/", CreateKelas).WithName("CreateKelas");
            publicGroup.MapPatch("/{id:int}", UpdateKelas).WithName("UpdateKelas");
            publicGroup.MapDelete("/{id:int}", DeleteKelas).WithName("DeleteKelas");
        }

        private static async Task<IResult> GetAllKelas(KelasServices kelasServices)
        {
            var kelases = await kelasServices.GetAllAsync();
            return Results.Ok(kelases);
        }

        private static async Task<IResult> GetKelasById(int id, KelasServices kelasServices)
        {
            var kelas = await kelasServices.GetByIdAsync(id);
            if (kelas is null)
                return Results.NotFound(new { message = "Kelas not found" });
            return Results.Ok(kelas);
        }

        private async static Task<IResult> CreateKelas(Kelas kelas, KelasServices kelasServices)
        {
            var result = await kelasServices.CreateAsync(kelas);
            if (!result)
                return Results.BadRequest(new { message = "Failed to create kelas" });
            return Results.Ok(new { message = "Kelas created successfully" });
        }

        private async static Task<IResult> UpdateKelas(int id, Kelas kelas, KelasServices kelasServices)
        {
            var result = await kelasServices.UpdateAsync(id, kelas);
            if (!result)
                return Results.BadRequest(new { message = "Failed to update kelas" });
            return Results.Ok(new { message = "Kelas updated successfully" });
        }

        private async static Task<IResult> DeleteKelas(int id, KelasServices kelasServices)
        {
            var result = await kelasServices.DeleteAsync(id);
            if (!result)
                return Results.BadRequest(new { message = "Failed to delete kelas" });
            return Results.Ok(new { message = "Kelas deleted successfully" });
        }
    }
}
