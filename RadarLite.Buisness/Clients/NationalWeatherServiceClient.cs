using Newtonsoft.Json;
using RadarLite.Buisness.Clients;
//using static JCRadarLite.Constants.Enums.State;

namespace JCRadarLite.Buisness.Helpers.Clients.NationalWeatherService;
//https://www.weather.gov/documentation/services-web-api

public class NationalWeatherServiceClient : BaseHttpClient {

    private const string BASE_URI = "https://api.weather.gov/";
    private const string GEOPOSITION = "points/";
    private const string STATIONS_BY_STATE = "stations?state=";
    private const string LOCATION_API = "locations/v1/";
    private const string FORECAST_API = "forecasts/v1/";

    public NationalWeatherServiceClient(HttpClient httpClient)
        : base(httpClient, BASE_URI)
    {
    }

    public async Task<T> HealthCheck<T>()
    {
        HttpRequestMessage request = CreateRequest();

        HttpResponseMessage response = httpClient.SendAsync(request).Result;

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(json);
            return result;
        }
        return default(T);
    }

    public async Task<T> GetStateStations<T>(string state)
    {
        HttpRequestMessage request = CreateEnumRequest(state);

        //request.UserAgent = "MyAppName/1.0.0 (someone@example.com)";

        HttpResponseMessage response = httpClient.SendAsync(request).Result;

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(json);
            return result;
        }
        return default(T);
    }

    public async Task<T> GetGeopositionalWeather<T>(double latitude, double longitude)
    {
        HttpRequestMessage request = CreateGeopositionRequest(latitude, longitude);

        //request.UserAgent = "MyAppName/1.0.0 (someone@example.com)";

        HttpResponseMessage response = httpClient.SendAsync(request).Result;

        if (true)//response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(json);
            return result;
        }
        return default(T);
    }

    public async Task<T> GetWeather<T>(string formattedUrl)
    {
        HttpRequestMessage request = CreateGenericRequest(formattedUrl);

        HttpResponseMessage response = httpClient.SendAsync(request).Result;

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(json);
            return result;
        }
        return default(T);
    }

    private HttpRequestMessage CreateRequest()
    {
        return new HttpRequestMessage(HttpMethod.Get, BASE_URI);
    }

    private HttpRequestMessage CreateEnumRequest(string state)
    {
        return new HttpRequestMessage(HttpMethod.Get, BASE_URI + STATIONS_BY_STATE + state);
    }

    private HttpRequestMessage CreateGeopositionRequest(double latitude, double longitude)
    {
        return new HttpRequestMessage(HttpMethod.Get, BASE_URI + GEOPOSITION + latitude + "," + longitude);
    }

    private HttpRequestMessage CreateGenericRequest(string formattedUrl)
    {
        return new HttpRequestMessage(HttpMethod.Get, formattedUrl);
    }



}
