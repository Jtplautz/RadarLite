

using RadarLite.Database.Models.Entities;
using RadarLite.Database.Models.Responses;

namespace RadarLite.Interfaces;
public interface INationalWeatherServiceAPIClient {

    /// <summary>
    /// Searches Location by zip.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> IsNWSAPIHealthy(
        CancellationToken cancellationToken = default);


    /// <summary>
    /// Searches Location by zip.
    /// </summary>
    /// <param name="term"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<LocationResponseModel> SearchAsync(
            int zip,
            CancellationToken cancellationToken = default);
    
}

