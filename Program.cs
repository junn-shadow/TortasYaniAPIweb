using Microsoft.EntityFrameworkCore;
using TortasYaniAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore.Diagnostics;

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (!string.IsNullOrEmpty(databaseUrl))
{
    Console.WriteLine("Usando la configuración de PostgreSQL (Railway)");
    
    // Railway puede usar postgres:// o postgresql://, normalizamos a postgresql://
    var normalizedUrl = databaseUrl.Replace("postgres://", "postgresql://");
    var uri = new Uri(normalizedUrl);
    var userInfo = uri.UserInfo.Split(':');
    var host = uri.Host;
    var port2 = uri.Port > 0 ? uri.Port : 5432;
    var database = uri.AbsolutePath.TrimStart('/');
    var username = Uri.UnescapeDataString(userInfo[0]);
    var password = userInfo.Length > 1 ? Uri.UnescapeDataString(userInfo[1]) : "";
    
    var connectionString = $"Host={host};Port={port2};Database={database};Username={username};Password={password};SSL Mode=Require;Trust Server Certificate=true";

    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(connectionString, sqlOptions => 
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5, 
                maxRetryDelay: TimeSpan.FromSeconds(10), 
                errorCodesToAdd: null);
        })
        .ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning)));
}
else
{
    Console.WriteLine("Usando la configuración de SQLite (local)");
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlite("Data Source=tortasyani.db")
        .ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning)));
}

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFlutter", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddHealthChecks();
builder.Services.AddScoped<TortasYaniAPI.Services.AuthService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtKey = builder.Configuration["Jwt:Key"] ?? "KeySuperSecretTemporal2024PorqueNoEstabaEnEnv0123";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        db.Database.EnsureCreated();
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Error al inicializar la base de datos");
    }
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowFlutter");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/health");

app.Run($"http://0.0.0.0:{port}");