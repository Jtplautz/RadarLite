using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using RadarLite.Buisness.Clients;
using RadarLite.Buisness.Helpers.Clients.NationalWeatherService;
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
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
           {
               // base-address of your identityserver

               options.Authority = "https://localhost:7056";

               // audience is optional, make sure you read the following paragraphs
               // to understand your options

               options.TokenValidationParameters.ValidateAudience = false;

               // it's recommended to check the type header to avoid "JWT confusion" attacks

               options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };

           });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "NWS.Temperature");
    });
});

builder.Services.AddHealthChecks();

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
app.UseAuthorization();
app.UseAuthentication();
app.UseSerilogRequestLogging();
app.UseHealthChecks("/health");
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
                                  .WithHeaders("Access-Control-Allow-Origin")
                                  .WithMethods("*")
                                  .WithOrigins("http://RadarLite.Web.me:7505", "http://192.168.254.125:7505", "http://192.168.1.192:7505", "http://192.168.1.192:3000");
                              });
    });

    services.AddScoped<ILocationService, LocationService>();
    services.AddTransient<IApiEndPoints, LocationEndpoints>();
    services.AddTransient<IApiEndPoints, HealthEndPoints>();
    services.AddHttpClient<INationalWeatherServiceAPIClient, NationalWeatherServiceClient>((httpClient) =>
    {
        NationalWeatherServiceClientFactory.ConfigureHttpClientCore(httpClient);
    });
}