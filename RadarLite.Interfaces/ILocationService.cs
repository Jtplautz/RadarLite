using RadarLite.Database.Models.Entities;
using RadarLite.Database.Models.Responses;

namespace RadarLite.Interfaces;
public interface ILocationService {
    Task<Location> GetLocationAsync(int zip);
    Task<IEnumerable<Location>> GetLocationsAsync();
    Task<HealthResponseModel> GetHealth();

    void Add<T>(T entity) where T : class;
    void Delete<T>(T entity) where T : class;
    Task<bool> SaveAll();
    void Update(Location existingLocation);
}

