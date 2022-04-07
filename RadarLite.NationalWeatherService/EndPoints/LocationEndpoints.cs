using RadarLite.Database.Models.Entities;
using RadarLite.Interfaces;

namespace RadarLite.NationalWeatherService.EndPoints;
public class LocationEndpoints : IApiEndPoints {

    private ILogger<LocationEndpoints> logger;

    public LocationEndpoints(ILogger<LocationEndpoints> logger)
    {
        this.logger = logger;
    }

    public void MapEndPoints(WebApplication app) {

        app.MapGet("/location/cities", GetAll);//.RequireAuthorization();
        app.MapGet("/location/city/{zip}", Get);
    }

    public async Task<IResult> GetAll(ILocationService locationService) {

        return Results.Ok(await locationService.GetLocationsAsync());
    }

    public async Task<IResult> Get(int zip, ILocationService locationService) {
        return Results.Ok(await locationService.GetLocationAsync(zip));
    }

    public async Task<IResult> Post(Location location, ILocationService locationService)
    {
        try {
            if (await locationService.SaveAll()) {
                return Results.Created($"/locations/{location.Id}", location);
            }
        }
        catch (Exception ex) {
            logger.LogError($"Error Occurred while posting Location: {ex}");
            return Results.BadRequest($"Error Occurred while posting to Location: {ex}");
        }

        return Results.BadRequest("Failed to Save Location!");
    }
}
