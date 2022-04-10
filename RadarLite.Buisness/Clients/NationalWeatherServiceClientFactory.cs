using RadarLite.Buisness.Helpers.Clients.NationalWeatherService;
using RadarLite.Extensions;
using RadarLite.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarLite.Buisness.Clients;
public class NationalWeatherServiceClientFactory {
    public static INationalWeatherServiceAPIClient Create(
        HttpClient httpClient,
        string host,
        string apiKey)
    {
        httpClient.BaseAddress = new Uri(host);

        ConfigureHttpClient(httpClient, host, apiKey);

        return new NationalWeatherServiceClient(httpClient);
    }

    public static INationalWeatherServiceAPIClient Create(
        string host, string apiKey, params DelegatingHandler[] handlers)
    {
        var httpClient = new HttpClient();

        if (handlers.Length > 0)
        {
            _ = handlers.Aggregate((a, b) =>
            {
                a.InnerHandler = b;
                return b;
            });
            httpClient = new(handlers[0]);
        }
        httpClient.BaseAddress = new Uri(host);

        ConfigureHttpClient(httpClient, host, apiKey);

        return new NationalWeatherServiceClient(httpClient);
    }

    public static void ConfigureHttpClientCore(HttpClient httpClient)
    {
        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(new("application/json"));
        httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("RadarLite/1.0.0 (jtplautz@gmail.com)");
    }

    internal static void ConfigureHttpClient(
        HttpClient httpClient,
        string host,
        string apiKey)
    {
        ConfigureHttpClientCore(httpClient);
        httpClient.AddRadarLiteHeaders(host, apiKey);
    }
}

