//using Microsoft.Extensions.DependencyInjection;
//namespace RadarLite.Constants;

//public static class ServiceCollectionExtensions {
//    public static IHttpClientBuilder AddDadJokesApiClient(
//        this IServiceCollection services,
//        Action<HttpClient> configureClient) =>
//            services.AddHttpClient<IDadJokesApiClient, DadJokesApiClient>((httpClient) =>
//            {
//                DadJokesApiClientFactory.ConfigureHttpClientCore(httpClient);
//                configureClient(httpClient);
//            });
//}