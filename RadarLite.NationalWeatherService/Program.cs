using Microsoft.EntityFrameworkCore;
using RadarLite.Buisness.Services.LocationService;
using RadarLite.Database.Models;
using RadarLite.Interfaces;
using RadarLite.NationalWeatherService.EndPoints;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
RegisterServices(builder.Services);

builder.Host.UseSerilog((ctx, lc) => lc
        .ReadFrom.Configuration(ctx.Configuration)
        .WriteTo.Seq(builder.Configuration.GetConnectionString("Seq")));

var app = builder.Build();
var apis = app.Services.GetServices<IApiEndPoints>();

foreach (var api in apis)
{
    if (api is null) { throw new InvalidProgramException("Apis not found"); }
    api.MapEndPoints(app);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("RadarLiteCorsOrigins");
app.UseHttpsRedirection();
app.Run();

void RegisterServices(IServiceCollection services) {
    // Add services to the container.

    services.AddDbContext<RadarLiteContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("RadarLiteContextConnection")));
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddCors(options =>
    {
        options.AddPolicy("RadarLiteCorsOrigins",
                              builder =>
                              {
                                  builder
                                  .WithHeaders("*")
                                  .WithMethods("*")
                                  .WithOrigins("*");
                              });
    });

    services.AddScoped<ILocationService, LocationService>();
    services.AddTransient<IApiEndPoints, LocationEndpoints>();
}