using Ramadhan_Digital.Services;
using Ramadhan_Digital.Controllers;
using Ramadhan_Digital.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

Env.Value = builder.Configuration;
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAndroid", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddSingleton<Database>();
builder.Services.AddScoped<AuthServices>();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<IJWTService, JWTService>();
builder.Services.AddScoped<ExcelImportService>();
builder.Services.AddScoped<SurahServices>();
builder.Services.AddScoped<AyatServices>();
builder.Services.AddScoped<DzikirServices>();
builder.Services.AddScoped<KelasServices>();
builder.Services.AddScoped<BacaanSholatServices>();
builder.Services.AddScoped<KegiatanServices>();
builder.Services.AddScoped<TausiahServices>();
builder.Services.AddScoped<SetoranHafalanServices>();
builder.Services.AddScoped<IbadahHarianServices>();
builder.Services.AddScoped<IbadahSunnahServices>();
builder.Services.AddScoped<AbsensiServices>();
builder.Services.AddScoped<StatusServices>();



// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = true;
        options.SaveToken = true;

        // Read JWT values from nested Database:JWT section in appsettings.json
        var jwtKey = Env.Value["JWT:Key"];
        var jwtIssuer = Env.Value["JWT:Issuer"];
        var jwtAudience = Env.Value["JWT:Audience"];

        if (string.IsNullOrWhiteSpace(jwtKey))
        {
            throw new InvalidOperationException("Database:JWT:Key configuration is missing. Set Database:JWT:Key in appsettings.json or environment variables.");
        }

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtIssuer,
            ValidateAudience = true,
            ValidAudience = jwtAudience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ValidateLifetime = true
        };
    });

builder.Services.AddAuthorization(options => Policies.Register(options));

var app = builder.Build();

// In development skip automatic HTTPS redirection to avoid "Failed to determine the https port" when
// no HTTPS endpoint is configured. This makes testing with Postman easier on the HTTP URL.
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}
else
{
    Console.WriteLine("Development environment: skipping HTTPS redirection.");
}

// Add a simple health endpoint to verify routing without authentication
app.MapGet("/ping", () => Results.Ok("pong")).AllowAnonymous();

// Log registered endpoints for easier troubleshooting
var endpoints = app.Services.GetRequiredService<Microsoft.AspNetCore.Routing.EndpointDataSource>().Endpoints;
Console.WriteLine("Registered endpoints:");
foreach (var e in endpoints)
{
    Console.WriteLine(e.DisplayName ?? e.ToString());
}

app.UseAuthentication();
app.UseAuthorization();

app.MapAuth();
app.MapSurahAyat();
app.MapDzikir();
app.MapKelas();
app.MapBacaanSholat();
app.MapKegiatan();
app.MapTausiah();
app.MapSetoranHafalan();
app.MapIbadahHarian();
app.MapIbadahSunnah();
app.MapAbsensi();
app.MapStatus();

app.UseCors("AllowAndroid");
app.Use(async (context, next) =>
{    var sw = Stopwatch.StartNew();

    try
    {
        await next();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
        throw;
    }
    finally
    {
        sw.Stop();

        var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";

        var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        Console.WriteLine(
            $"{timestamp} INFO: {ip} - \"{context.Request.Method} {context.Request.Path} {context.Response.StatusCode}\" {sw.ElapsedMilliseconds}ms"
        );
    }
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
