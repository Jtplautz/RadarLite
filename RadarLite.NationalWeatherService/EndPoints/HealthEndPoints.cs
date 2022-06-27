using RadarLite.Interfaces;

namespace RadarLite.NationalWeatherService.EndPoints;
public class HealthEndPoints : IApiEndPoints {
    private ILogger<HealthEndPoints> logger;

    public HealthEndPoints(ILogger<HealthEndPoints> logger)
    {
        this.logger = logger;
    }

    public void MapEndPoints(WebApplication app)
    {

        app.MapGet("/nws/healthy", Get).RequireAuthorization("ApiScope");
    }


    public async Task<IResult> Get(ILocationService locationService)
    {
        return Results.Ok(await locationService.GetHealth());
    }

}

