using RadarLite.Buisness.Services.User;
using RadarLite.Identity.Areas.Identity.Data;
using RadarLite.Interfaces.User;

namespace RadarLite.Identity.EndPoints;

public static class LoginEndpoints
{
    public static void MapUsersEndpoints(this WebApplication app)
    {
        
        app.MapGet("/v1/users", GetAllUsers);
        app.MapGet("/v1/users/{id}", GetUserById);
       
    }

    public static void AddUserServices(this IServiceCollection service)
    {
        service.AddHttpClient<IUserConfigService, UserConfigurationService>();
    }

    internal static IResult GetAllUsers(IUserConfigService service, RadarLiteIdentityContext context)
    {
        var users = service.GetAllUsersAsync();//.Result.Users;

        return users is not null ? Results.Ok(users) : Results.NotFound();
    }

    internal static IResult GetUserById(IUserConfigService service, int id)
    {
        var user = service.GetUserByIdAsync(id);//.Result.Users.SingleOrDefault();

        return user is not null ? Results.Ok(user) : Results.NotFound();
    }
    internal static IResult CreateUser(IUserConfigService service)
    {
        service.CreateUser();

        return Results.Ok();
    }
}


