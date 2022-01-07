using Microsoft.AspNetCore.Mvc;
using RadarLite.Buisness.Services.Location;
using RadarLite.Database.Models;
using RadarLite.Database.Models.Entities;
using RadarLite.Interfaces;

namespace RadarLite.NationalWeatherService.EndPoints;
public static class LocationEndpoints {
    
    public static void MapLocationEndpoints(this WebApplication app) {
        app.MapGet("/Cities",  ([FromServices] RadarLiteContext context) =>
        {
            List<Location> locations = new List<Location>();
            locations.Add(new Location { Name = "Baz" });
            return locations;
        }
        );
    }
    public static void AddLocationService(this IServiceCollection services) {
        services.AddSingleton<ILocationService, LocationService>();
    
    }
}
