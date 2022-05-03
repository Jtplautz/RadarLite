using Newtonsoft.Json;
using RadarLite.Constants;
using RadarLite.Constants.URLs;
using RadarLite.Database.Models.Responses;
using RadarLite.Interfaces;
using System.Net.Http.Json;

namespace RadarLite.Buisness.Helpers.Clients.NationalWeatherService;

//API documentation
//https://www.weather.gov/documentation/services-web-api

public class NationalWeatherServiceClient : INationalWeatherServiceAPIClient {

    private readonly HttpClient httpClient;

    public NationalWeatherServiceClient(HttpClient httpClient) =>
        this.httpClient = httpClient
            ?? throw new ArgumentNullException(nameof(httpClient));

    internal async Task<T> HandleResult<T>(HttpResponseMessage response, CancellationToken cancellationToken = default) {
        
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(json);
            return result;
        }

        return default(T);
    }

    public async Task<LocationResponseModel> SearchAsync(int zip, CancellationToken cancellationToken = default)
    {
        var locations = await this.httpClient
            .GetFromJsonAsync<LocationResponseModel>(NationalWeatherServiceURLs.LOCATION_API, cancellationToken);

        return locations;
    }

    public async Task<HealthResponseModel> GetHealthAsync(CancellationToken cancellationToken = default) {

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, NationalWeatherServiceURLs.BASE_URI);

        HttpResponseMessage response = httpClient.SendAsync(request).Result;

        return await HandleResult<HealthResponseModel>(response, cancellationToken);
    }

    //public async Task<T> Test<T>(CancellationToken cancellationToken = default)
    //{

    //    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, NationalWeatherServiceURLs.BASE_URI);

    //    HttpResponseMessage response = httpClient.SendAsync(request).Result;

    //    if (response.IsSuccessStatusCode)
    //    {
    //        var json = await response.Content.ReadAsStringAsync();
    //        var result = JsonConvert.DeserializeObject<T>(json);
    //        return result;
    //    }

    //    return default(T);


    //}

}
