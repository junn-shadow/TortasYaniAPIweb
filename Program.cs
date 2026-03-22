using Microsoft.EntityFrameworkCore;
using TortasYaniAPI.Data;

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (databaseUrl != null)
{
    // PostgreSQL para Railway
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(databaseUrl));
}
else
{
    // SQLite para desarrollo local
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlite("Data Source=tortasyani.db"));
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

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowFlutter");
app.UseAuthorization();
app.MapControllers();

app.Run($"http://0.0.0.0:{port}");