using dotnet_articles_api.Controllers;
using dotnet_articles_api.Infrastructure.Data;
using dotnet_articles_api.Infrastructure.Logging;
using dotnet_articles_api.Infrastructure.Repositories;
using dotnet_articles_api.Interfaces;
using dotnet_articles_api.Logger;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler
            = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles; // ✅
    });
// Register our repository and logger
builder.Services.AddSingleton<IRepository, InMemoryRepository>();
builder.Services.AddSingleton<LoggerProxy>();
builder.Services.AddSingleton<IArticles, InMemoryRepository>();

// Register your custom logger
builder.Services.AddSingleton<IAppLogger, FileLogger>(); // ✅ Add this

// Optional: Swagger for testing
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Register the DbContext with SQL Server
builder.Services.AddDbContext<ArticlesDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Switch from InMemoryRepository to SqlRepository
builder.Services.AddScoped<IRepository, SqlRepository>();
builder.Services.AddScoped<IArticles, SqlRepository>();
builder.Services.AddSingleton<LoggerProxy>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();