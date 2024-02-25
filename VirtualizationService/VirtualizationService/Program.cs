using VirtualizationService.Factories;
using VirtualizationService.Factories.Base;
using VirtualizationService.Services;
using VirtualizationService.DatabaseInfrastucture;
using Microsoft.Extensions.Caching;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IFactory, DatabaseConnectionFactory>();
builder.Services.AddSingleton<IDataVirtualizationService, DataVirtualizationService>();
builder.Services.AddStackExchangeRedisCache(options =>
            options.Configuration = builder.Configuration.GetConnectionString("Cache"));



var app = builder.Build();

var configuration = app.Services.GetRequiredService<IConfiguration>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    /// To Check if the application needs to apply the migration to setup the Database. 
    /// [Only in Docker Setting]
    bool.TryParse(configuration["ApplyMigrations"], out bool applyMigrationFlag);

    if (applyMigrationFlag)
    {
        app.ApplyMigrations();
    }
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
