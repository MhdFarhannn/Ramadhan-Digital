using Ramadhan_Digital.Services;
using Ramadhan_Digital.Models;

namespace Ramadhan_Digital.Controllers
{
    public static class StatusController
    {
        public static void MapStatus(this WebApplication app)
        {
            var publicGroup = app.MapGroup("api/v1/status");

            // GET ALL STATUS ABSENSI
            publicGroup.MapGet("/absensi", async (StatusServices statusServices) =>
            {
                var result = await statusServices.GetAllStatusAbsensi();
                return Results.Ok(result);
            });

            // GET ALL STATUS SETORAN HAFALAN
            publicGroup.MapGet("/setoran-hafalan", async (StatusServices statusServices) =>
            {
                var result = await statusServices.GetAllStatusSetoranHafalan();
                return Results.Ok(result);
            });

            // GET ALL STATUS SHOLAT WAJIB
            publicGroup.MapGet("/sholat-wajib", async (StatusServices statusServices) =>
            {
                var result = await statusServices.GetAllStatusSholatWajib();
                return Results.Ok(result);
            });
        }
    }
}