using RadarLite.Buisness.Services.Location;
using RadarLite.Interfaces;

namespace RadarLite.NationalWeatherService.EndPoints;
public static class LocationEndpoints {
    public static void MapLocationEndpoints(this WebApplication app) { 
    
    }
    public static void AddLocationService(this IServiceCollection services) {
        services.AddSingleton<ILocationService, LocationService>();
    
    }
}
