﻿
using RadarLite.Database.Models.Entities;

namespace RadarLite.Interfaces;
public interface ILocationService {
    Task<Location> GetLocationAsync(int zip);
    Task<IEnumerable<Location>> GetLocationsAsync();
    Task<ValidatedHealthModelResponse> GetHealth();
    void Add<T>(T entity) where T : class;
    void Delete<T>(T entity) where T : class;
    Task<bool> SaveAll();
    void Update(Location existingLocation);
}

