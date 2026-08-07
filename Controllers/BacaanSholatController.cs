using System.Runtime.CompilerServices;
using Ramadhan_Digital.Models;
using Ramadhan_Digital.Services;
namespace Ramadhan_Digital.Controllers
{
    public static class BacaanSholatController
    {
        public static void MapBacaanSholat(this WebApplication app)
        {
            var publicGroup = app.MapGroup("/api/v1/bacaan-sholat");
            publicGroup.MapGet("/", async (BacaanSholatServices service) =>
            {
                var bacaanSholat = await service.GetAllAsync();
                return Results.Ok(bacaanSholat);
            });

            publicGroup.MapGet("/{id}", async (int id, BacaanSholatServices service) =>
            {
                var bacaanSholat = await service.GetByIdAsync(id);
                if (bacaanSholat == null)
                    return Results.NotFound();
                return Results.Ok(bacaanSholat);
            });

            publicGroup.MapPost("/", async (BacaanSholat bacaanSholat, BacaanSholatServices service) =>
            {   
                var created = await service.CreateAsync(bacaanSholat);
                if (!created)
                    return Results.BadRequest();
                return Results.Created($"/api/v1/bacaan-sholat/{bacaanSholat.Id}", bacaanSholat);
            });
        }
    }
}
