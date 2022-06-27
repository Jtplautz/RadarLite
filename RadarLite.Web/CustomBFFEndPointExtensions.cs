using Duende.Bff;

namespace RadarLite.Web;
public static class CustomBFFEndPointExtensions {
    private static Task ProcessWith<T>(HttpContext context) where T : IBffEndpointService
    {
        return context.RequestServices.GetRequiredService<T>().ProcessRequestAsync(context);
    }

}
