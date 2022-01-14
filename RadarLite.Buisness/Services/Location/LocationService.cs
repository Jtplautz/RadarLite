using RadarLite.Database.Models;
using RadarLite.Database.Models.Entities;
using RadarLite.Interfaces;

namespace RadarLite.Buisness.Services.LocationService;
public class LocationService : ILocationService {
    private RadarLiteContext context;

    public LocationService(RadarLiteContext context) {
        this.context = context;
    }
    public async Task<IEnumerable<Location>> GetLocationsAsync()
    {
        //return the Locations from context.Locations.
        List<Location> locations = new List<Location>();
        locations.Add(new Location { Name = "Baz" });
        locations.Add(new Location { Name = "Shaz" });

        return locations;
    }

    public async Task<Location> GetLocationAsync(int zip)
    {
        //return the Location from context.Locations.
        return new Location { Name = "Faz" };
    }

    public void Add<T>(T entity) where T : class => context.Add(entity);

    public void Delete<T>(T entity) where T : class => context.Remove(entity);

    public async Task<bool> SaveAll() => (await context.SaveChangesAsync()) > 0;

    public void Update(Location existingLocation) => context.Update(existingLocation);
}

