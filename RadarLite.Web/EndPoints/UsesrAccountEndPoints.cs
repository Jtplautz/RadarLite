using Duende.Bff;

namespace RadarLite.Web.EndPoints;
public class UsesrAccountEndPoints: IUserAccountEndPoints, IBffEndpointService {
    private ILogger<UsesrAccountEndPoints> logger;

    public UsesrAccountEndPoints(ILogger<UsesrAccountEndPoints> logger)
    {
        this.logger = logger;
    }

    public void MapEndPoints(WebApplication app)
    {
        app.MapGet("/bff/signup", Get);
    }


    public void Get(HttpContext context)
    {
        logger.LogInformation("Redirecting...");

        //Should I just call the "OnGet" controller enpoint of a new page I add to the identity server?
        context.Response.Redirect("https://localhost:7056/Signup/Create");
    }

    public Task ProcessRequestAsync(HttpContext context)
    {
        throw new NotImplementedException();
    }
}

