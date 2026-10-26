using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using ProductApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add Services

builder.Services.AddControllers();

// SQLite Database Path
var dbPath = Path.Combine(
    builder.Environment.ContentRootPath,
    "products.db");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite($"Data Source={dbPath}");
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Product API",
        Version = "v1",
        Description = "Products CRUD API"
    });
});

var app = builder.Build();

// Display actual database location in logs
Console.WriteLine($"SQLite Database Path: {dbPath}");

// Swagger available in IIS and local environments

app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Product API v1");
    options.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.MapControllers();

app.Run();