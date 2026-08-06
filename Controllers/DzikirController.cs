using Ramadhan_Digital.Models;
using Ramadhan_Digital.Services;

namespace Ramadhan_Digital.Controllers
{
    public static class DzikirController
    {
        public static void MapDzikir(this WebApplication app)
        {
            var publicGroup = app.MapGroup("/api/v1/dzikir");
            publicGroup.MapGet("/", GetAllDzikir).WithName("GetAllDzikir");
            publicGroup.MapGet("/{id:int}", GetDzikirById).WithName("GetDzikirById");
            publicGroup.MapPost("/", CreateDzikir).WithName("CreateDzikir");
            publicGroup.MapPut("/{id:int}", UpdateDzikir).WithName("UpdateDzikir");
            publicGroup.MapDelete("/{id:int}", DeleteDzikir).WithName("DeleteDzikir");

        }
        private static async Task<IResult> GetAllDzikir(DzikirServices dzikirServices)
        {
            var dzikirs = await dzikirServices.GetAllAsync();
            return Results.Ok(dzikirs);
        }
        private static async Task<IResult> GetDzikirById(int id, DzikirServices dzikirServices)
        {
            var dzikir = await dzikirServices.GetByIdAsync(id);
            if (dzikir is null)
                return Results.NotFound(new { message = "Dzikir not found" });
            return Results.Ok(dzikir);
        }

        private static async Task<IResult> CreateDzikir(DzikirSetelahSholat dzikir, DzikirServices dzikirServices)
        {
            var result = await dzikirServices.CreateAsync(dzikir);
            if (!result)
                return Results.BadRequest(new { message = "Failed to create dzikir" });
            return Results.Ok(new { message = "Dzikir created successfully" });
        }

        private static async Task<IResult> UpdateDzikir(int id, DzikirSetelahSholat dzikir, DzikirServices dzikirServices)
        {
            var result = await dzikirServices.UpdateAsync(id, dzikir);
            if (!result)
                return Results.BadRequest(new { message = "Failed to update dzikir" });
            return Results.Ok(new { message = "Dzikir updated successfully" });
        }

        private static async Task<IResult> DeleteDzikir(int id, DzikirServices dzikirServices)
        {
            var result = await dzikirServices.DeleteAsync(id);
            if (!result)
                return Results.BadRequest(new { message = "Failed to delete dzikir" });
            return Results.Ok(new { message = "Dzikir deleted successfully" });
        }

    }
}
