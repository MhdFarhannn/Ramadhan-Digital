using System.Diagnostics;
using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using Ramadhan_Digital.Controllers;
using Ramadhan_Digital.Models;
using Ramadhan_Digital.Services;

var builder = WebApplication.CreateBuilder(args);

// ================================================================
// CONFIGURATION
// ================================================================

Env.Value = builder.Configuration;


// ================================================================
// CORS
// ================================================================

builder.Services.AddCors(options =>
{
    // Android / Mobile
    options.AddPolicy("AllowAndroid", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });

    // Web Frontend
    options.AddPolicy("AllowWebFrontend", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:3000",
                "http://localhost:5174",
                "http://localhost:5173",
                "http://localhost:4200",
                "http://192.168.69.50:5173",
                "http://192.168.69.50:5174",
                "https://yourdomain.com"
            )
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });

    // Jika memang diperlukan untuk development/testing
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});


// ================================================================
// DEPENDENCY INJECTION
// ================================================================

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


// ================================================================
// DAPPER - DATEONLY HANDLER
//
// Satu-satunya tempat pendaftaran type handler tanggal.
//
//     PostgreSQL date <-> C# DateOnly
//
// Jangan mendaftarkan handler DateOnly di service manapun.
// ================================================================

DateOnlyTypeHandler.Register();


// ================================================================
// JWT AUTHENTICATION
// ================================================================

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = true;
        options.SaveToken = true;

        var jwtKey = Env.Value["JWT:Key"];
        var jwtIssuer = Env.Value["JWT:Issuer"];
        var jwtAudience = Env.Value["JWT:Audience"];

        if (string.IsNullOrWhiteSpace(jwtKey))
        {
            throw new InvalidOperationException(
                "JWT:Key configuration is missing. " +
                "Set JWT:Key in appsettings.json or environment variables."
            );
        }

        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtIssuer,

                ValidateAudience = true,
                ValidAudience = jwtAudience,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtKey)
                    ),

                ValidateLifetime = true
            };
    });


// ================================================================
// AUTHORIZATION
// ================================================================

builder.Services.AddAuthorization(
    options => Policies.Register(options)
);


// ================================================================
// BUILD APPLICATION
// ================================================================

var app = builder.Build();


// ================================================================
// REQUEST LOGGING
// ================================================================

app.Use(async (context, next) =>
{
    var stopwatch = Stopwatch.StartNew();

    try
    {
        await next();
    }
    catch (Exception ex)
    {
        Console.WriteLine();
        Console.WriteLine("================================================");
        Console.WriteLine("UNHANDLED EXCEPTION");
        Console.WriteLine("================================================");
        Console.WriteLine(ex);
        Console.WriteLine("================================================");
        Console.WriteLine();

        throw;
    }
    finally
    {
        stopwatch.Stop();

        var ip =
            context.Connection.RemoteIpAddress?.ToString()
            ?? "unknown";

        var timestamp =
            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        Console.WriteLine(
            $"{timestamp} INFO: {ip} - " +
            $"\"{context.Request.Method} {context.Request.Path} " +
            $"{context.Response.StatusCode}\" " +
            $"{stopwatch.ElapsedMilliseconds}ms"
        );
    }
});


// ================================================================
// CORS
// ================================================================

// Pilih policy sesuai client.
//
// Web:
// app.UseCors("AllowWebFrontend");
//
// Android:
// app.UseCors("AllowAndroid");

app.UseCors("AllowWebFrontend");


// ================================================================
// HTTPS
// ================================================================

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}
else
{
    Console.WriteLine(
        "Development environment: " +
        "skipping HTTPS redirection."
    );
}


// ================================================================
// PUBLIC HEALTH CHECK
// ================================================================

app.MapGet(
    "/ping",
    () => Results.Ok("pong")
)
.AllowAnonymous();


// ================================================================
// AUTHENTICATION & AUTHORIZATION
// ================================================================

app.UseAuthentication();
app.UseAuthorization();


// ================================================================
// API ENDPOINTS
// ================================================================

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


// ================================================================
// LOG REGISTERED ENDPOINTS
// ================================================================

var endpoints =
    app.Services
        .GetRequiredService<
            Microsoft.AspNetCore.Routing.EndpointDataSource
        >()
        .Endpoints;

Console.WriteLine();
Console.WriteLine("================================================");
Console.WriteLine("REGISTERED ENDPOINTS");
Console.WriteLine("================================================");

foreach (var endpoint in endpoints)
{
    Console.WriteLine(
        endpoint.DisplayName
        ?? endpoint.ToString()
    );
}

Console.WriteLine("================================================");
Console.WriteLine();


// ================================================================
// RUN APPLICATION
// ================================================================

app.Run();
