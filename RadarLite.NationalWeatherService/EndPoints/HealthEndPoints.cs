using RadarLite.Interfaces;

namespace RadarLite.NationalWeatherService.EndPoints;
public class HealthEndPoints : IApiEndPoints {
    private ILogger<LocationEndpoints> logger;

    public HealthEndPoints(ILogger<LocationEndpoints> logger)
    {
        this.logger = logger;
    }

    public void MapEndPoints(WebApplication app)
    {

        app.MapGet("/nws/healthy", Get);
    }


    public async Task<IResult> Get(ILocationService locationService)
    {
        return Results.Ok(await locationService.GetHealth());
    }

}

