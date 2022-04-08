
using RadarLite.Constants;
using System.Net.Http.Headers;

namespace RadarLite.Extensions;

public static class HttpClientExtensions {
    public static HttpClient AddRadarLiteHeaders(
        this HttpClient httpClient,
        string host,
        string apiKey)
    {
        var headers = httpClient.DefaultRequestHeaders;
        //headers.Add(NationalWeatherServiceAPIConstants.HostHeader, new Uri(host).Host);
        //headers.Add(NationalWeatherServiceAPIConstants.ApiKeyHeader, apiKey);
        //httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.107 Safari/537.36 Edg/92.0.902.62"));

        headers.UserAgent.TryParseAdd("RadarLite/1.0.0 (jtplautz@gmail.com)");
        headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        return httpClient;
    }
}