using Microsoft.Extensions.Logging;
using RadarLite.Database.Models;
using RadarLite.Interfaces;

namespace RadarLite.Buisness.Services.Location;
internal class LocationService : ILocationService {
    private readonly ILogger logger;
    private RadarLiteContext context;

    public LocationService(ILogger logger, RadarLiteContext context) {
        this.logger = logger;
        this.context = context;
    }

    public void AddLocation()
    {
        throw new NotImplementedException();
    }
}

