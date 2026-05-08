using BusinessLogic.Data;
using BusinessLogic.Logic;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

    //for  Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PS4DbContext>(options =>
    options.UseSqlServer(connString));

//builder.Services.AddSwaggerGen()

builder.Services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsRule", rule =>
    {
        rule.AllowAnyHeader().AllowAnyMethod().WithOrigins("*");
    });
});


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();

    try
    {
        var context = services.GetRequiredService<PS4DbContext>();
        //Seecion de prueba checar si hay migraciones pendientes
        if(await context.HasPendingMigrtionsAsync())
        {
            Console.WriteLine("There are pending Migrations. InProgress...");
            await context.Database.MigrateAsync();
            Console.WriteLine("Migrations Applied");
        }
        else
        {
            Console.WriteLine("La base de datos está sincronizada con el modelo.");
        }

        //Create file in case of use.
        await PS4DbContextData.AsyncDataLoading(context, loggerFactory);
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "Error in Migration process :(");
    }
}

// Configure the HTTP request pipeline.
//for Swagger too
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsRule");

//var summaries = new[]
//{
//    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//};

//app.MapGet("/weatherforecast", () =>
//{
//    var forecast = Enumerable.Range(1, 5).Select(index =>
//        new WeatherForecast
//        (
//            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//            Random.Shared.Next(-20, 55),
//            summaries[Random.Shared.Next(summaries.Length)]
//        ))
//        .ToArray();
//    return forecast;
//});

app.MapControllers();
app.Run();

//internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
//{
//    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
//}
