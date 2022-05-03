using IdentityServerAspNetIdentity;
using Microsoft.EntityFrameworkCore;
using RadarLite.Identity;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Seq(builder.Configuration.GetConnectionString("Seq"))
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(ctx.Configuration));

    Log.Information("RadarLite.Auth starting up...");

    var app = builder
        .ConfigureServices()
        .ConfigurePipeline();
    // this seeding is only for the template to bootstrap the DB and users.
    // in production you will likely want a different approach.

        //Log.Information("Seeding database...");
        //SeedData.EnsureSeedData(app);
        //Log.Information("Done seeding database. Exiting.");
        //return;
    

    app.Run();
}
catch (Exception ex) when (ex.GetType().Name is not "StopTheHostException") // https://github.com/dotnet/runtime/issues/60600
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}