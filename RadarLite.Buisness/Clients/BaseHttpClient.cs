using RadarLite.Interfaces;
using System.Net.Http.Headers;

namespace RadarLite.Buisness.Clients;
//Mozilla/5.0 (Windows NT 10.0; <64-bit tags>) AppleWebKit/<WebKit Rev> (KHTML, like Gecko) Chrome/<Chrome Rev> Safari/<WebKit Rev> Edge/<EdgeHTML Rev>.<Windows Build>"
public abstract class BaseHttpClient : IBaseHttpClient {
    protected readonly HttpClient httpClient;

    public BaseHttpClient(HttpClient httpClient, string url)
    {
        httpClient.BaseAddress = new Uri(url);
        //httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.107 Safari/537.36 Edg/92.0.902.62"));
        //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("JCRadarLite Weather, jtplautz@gmail.com");
        httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("JCRadarLite/1.0.0 (jtplautz@gmail.com)");
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        this.httpClient = httpClient;
    }

}

