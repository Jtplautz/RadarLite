using Newtonsoft.Json;
using RadarLite.Buisness.Clients;
using RadarLite.Constants;
using RadarLite.Constants.URLs;
using RadarLite.Database.Models.Entities;
using RadarLite.Database.Models.Responses;
using RadarLite.Interfaces;
using System.Net.Http.Json;
//using static JCRadarLite.Constants.Enums.State;

namespace RadarLite.Buisness.Helpers.Clients.NationalWeatherService;
//https://www.weather.gov/documentation/services-web-api

public class NationalWeatherServiceClient : INationalWeatherServiceAPIClient {

    private readonly HttpClient httpClient;

    public NationalWeatherServiceClient(HttpClient httpClient) =>
        this.httpClient = httpClient
            ?? throw new ArgumentNullException(nameof(httpClient));

    //public async Task<Location> GetJokeByIdAsync(
    //    string id, CancellationToken cancellationToken = default)
    //{
    //    var path = ApiUrlConstants.GetJokeById.AppendPathSegment(id);

    //    var joke = await this.httpClient
    //        .GetFromJsonAsync<Joke>(path, cancellationToken);

    //    return joke ?? new();
    //}

    //public async Task<Joke> GetRandomJokeAsync(CancellationToken cancellationToken = default)
    //{
    //    var jokes = await this.httpClient.GetFromJsonAsync<JokeSearchResponse>(
    //        ApiUrlConstants.GetRandomJoke, cancellationToken);

    //    if (jokes is null or { Body.Count: 0 } or { Success: false })
    //    {
    //        throw new InvalidOperationException("This API is no joke.");
    //    }

    //    return jokes.Body.First();
    //}

    //public async Task<JokeSearchResponse> SearchAsync(
    //    string term, CancellationToken cancellationToken = default)
    //{
        
    //}

    public async Task<LocationResponseModel> SearchAsync(int zip, CancellationToken cancellationToken = default)
    {
        //var locations = await this.httpClient
        //    .GetFromJsonAsync<LocationResponseModel>(
        //        NationalWeatherServiceAPIConstants.HostHeader, cancellationToken);

        bool loc = await IsNWSAPIHealthy(cancellationToken);
        if (loc)
        { return new LocationResponseModel { Success = true }; }
        return new LocationResponseModel { Success = false };
    }


    //TODO: Get this to communicate with the NWS.
    public async Task<bool> IsNWSAPIHealthy(CancellationToken cancellationToken = default) {
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, NationalWeatherServiceURLs.BASE_URI);

        HttpResponseMessage response = httpClient.SendAsync(request).Result;

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<bool>(json);
            return result;
        }
        return default(bool);
    }
}
