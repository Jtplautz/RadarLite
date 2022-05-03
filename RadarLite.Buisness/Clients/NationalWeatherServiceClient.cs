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
    
    public async Task<LocationResponseModel> SearchAsync(int zip, CancellationToken cancellationToken = default)
    {
        //var locations = await this.httpClient
        //    .GetFromJsonAsync<LocationResponseModel>(
        //        NationalWeatherServiceAPIConstants.HostHeader, cancellationToken);

        bool loc = await GetHealthAsync(cancellationToken);

        if (loc) { 
            return new LocationResponseModel { Success = true };
        }
        return new LocationResponseModel { Success = false };
    }



    public async Task<bool> GetHealthAsync(CancellationToken cancellationToken = default) {
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, NationalWeatherServiceURLs.BASE_URI);

        HttpResponseMessage response =  httpClient.SendAsync(request).Result;

        if (response.IsSuccessStatusCode)
        {
           //keep this for santies sake.
           var json = await response.Content.ReadAsStringAsync(); 
           return true; //this just means we a got a response, not necessary healthy...
        }
        return default(bool);
    }

}
