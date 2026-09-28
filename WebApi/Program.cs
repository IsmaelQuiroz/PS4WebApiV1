using BusinessLogic.Data;
using BusinessLogic.Logic;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApi.Dtos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

//After prepare Usuario Entity Migration but before apply
//builder = new IdentityBuilder(builder.UserType, builder.Services); //esto es lo que necesita el objeto para poder construir las tablas desde el modelo del IdentityCore

//para implementar seguridad
//builder.Services.AddIdentityCore<Usuario>()
//    .AddEntityFrameworkStores<SeguridadDbContext>()
//    .AddSignInManager<SignInManager<Usuario>>();
//    //.AddDefaultTokenProviders();


//for  Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MappingProfiles));


//var connString = builder.Configuration.GetConnectionString("DefaultConnection");
var connString = builder.Configuration.GetValue<string>("ConnectionStrings:DefaultConnection");

builder.Services.AddDbContext<PS4DbContext>(options =>
    options.UseNpgsql(connString));

//Para implementar seguridad
//builder.Services.AddDbContext<SeguridadDbContext>(options =>
//{
//    options.UseNpgsql(builder.Configuration.GetConnectionString("IdentitySeguridad"));

//});


//builder.Services.AddSwaggerGen()


builder.Services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsRule", rule =>
    {
        rule.AllowAnyHeader().AllowAnyMethod().WithOrigins("*");
    });
});

builder.Services.AddSingleton(TimeProvider.System);

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


        //otra forma de crar el scope 
        //using var scope = builder.Services.BuildServiceProvider().CreateScope();
        //var services = scope.ServiceProvider;

        //Esta seccion se habilitara cuando se agregguen las migraciones de seguridad
        //Obtener los servicios normalmente
        //var userManager = services.GetRequiredService<UserManager<Usuario>>();
        //var identityContext = services.GetRequiredService<SeguridadDbContext>();
        //await identityContext.Database.MigrateAsync();
        //await SeguridadDbContextData.SeedUserAsync(userManager);

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
