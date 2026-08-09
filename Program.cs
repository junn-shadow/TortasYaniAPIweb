using Microsoft.EntityFrameworkCore;
using TortasYaniAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TortasYaniAPI.Models;

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var postgresUrl = Environment.GetEnvironmentVariable("DATABASE_URL") 
                  ?? Environment.GetEnvironmentVariable("POSTGRES_URL")
                  ?? builder.Configuration.GetConnectionString("PostgreSQL");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (!string.IsNullOrEmpty(postgresUrl))
    {
        Console.WriteLine("Conectando a PostgreSQL (Railway)");
        string connStr = postgresUrl;
        if (postgresUrl.StartsWith("postgres://") || postgresUrl.StartsWith("postgresql://"))
        {
            var uri = new Uri(postgresUrl);
            var userInfo = uri.UserInfo.Split(':');
            var user = userInfo[0];
            var password = userInfo.Length > 1 ? userInfo[1] : "";
            var host = uri.Host;
            var portNum = uri.Port > 0 ? uri.Port : 5432;
            var dbName = uri.AbsolutePath.TrimStart('/');
            connStr = $"Host={host};Port={portNum};Database={dbName};Username={user};Password={password};SSL Mode=Require;Trust Server Certificate=true;";
        }
        options.UseNpgsql(connStr);
    }
    else
    {
        Console.WriteLine("Usando la configuración de SQLite (local)");
        options.UseSqlite("Data Source=tortasyani.db");
    }
});

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
        var jwtKey = builder.Configuration["Jwt:Key"];
        if (string.IsNullOrEmpty(jwtKey))
        {
            throw new InvalidOperationException("La clave JWT ('Jwt:Key') no está configurada.");
        }
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

app.UseMiddleware<TortasYaniAPI.Middleware.ExceptionHandlingMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        db.Database.Migrate();

        // Sembrar usuario admin por defecto si no existe
        if (!db.Users.Any(u => u.Email == "admin@gmail.com"))
        {
            db.Users.Add(new User
            {
                NombreCompleto = "Administrador",
                Email = "admin@gmail.com",
                Password = BCrypt.Net.BCrypt.HashPassword("admin123"),
                Telefono = "999999999",
                Direccion = "Tienda Principal",
                FotoUrl = ""
            });
            db.SaveChanges();
            // Removed automatic deletion of non-admin users. This block is now disabled to preserve client accounts and sample data.
            // var nonAdmin = db.Users.Where(u => u.Email != "admin@gmail.com");
            // if (nonAdmin.Any())
            // {
            //     db.Users.RemoveRange(nonAdmin);
            //     db.SaveChanges();
            // }
        }
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