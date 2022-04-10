using Microsoft.EntityFrameworkCore;
using RadarLite.Buisness.Helpers.Clients.NationalWeatherService;
using RadarLite.Database.Models;
using RadarLite.Database.Models.Entities;
using RadarLite.Interfaces;

namespace RadarLite.Buisness.Services.LocationService;
public class LocationService : ILocationService {
    private RadarLiteContext context;
    private INationalWeatherServiceAPIClient nwsClient;

    public LocationService(RadarLiteContext context, INationalWeatherServiceAPIClient nwsClient) {
        this.context = context;
        this.nwsClient = nwsClient;
    }
    public async Task<IEnumerable<Location>> GetLocationsAsync()
    {
        var data = await context.Locations.ToListAsync();
        if (data.Count > 0) { return data; }

        List<Location> locations = new List<Location>();
        locations.Add(new Location { Name = "Baz" });
        locations.Add(new Location { Name = "Shaz" });

        return locations;
        
    }

    public async Task<Location> GetLocationAsync(int zip)
    {
        //return the Location from context.Locations.

        var response = await nwsClient.SearchAsync(zip);

        if (response.Success == true) { return new Location { Name = "Healthy!" }; }

        return new Location { Name = "Failed!" };
    }

    public async Task<bool> GetHealth()
    {
        return await nwsClient.GetHealthAsync();
    }

    public void Add<T>(T entity) where T : class => context.Add(entity);

    public void Delete<T>(T entity) where T : class => context.Remove(entity);

    public async Task<bool> SaveAll() => (await context.SaveChangesAsync()) > 0;

    public void Update(Location existingLocation) => context.Update(existingLocation);


}

