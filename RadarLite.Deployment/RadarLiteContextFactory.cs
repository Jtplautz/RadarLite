using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;
using RadarLite.Database.Models;

namespace RadarLite.Deployment;

public class RadarLiteContextFactory : IDesignTimeDbContextFactory<RadarLiteContext>
{
    public RadarLiteContext CreateDbContext(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;
        configuration.SetBasePath(Directory.GetCurrentDirectory());
        configuration.AddJsonFile("appsettings.json");

        IServiceCollection services = new ServiceCollection();

        services.AddDbContextPool<RadarLiteContext>(options =>
        {
            options.EnableSensitiveDataLogging();
            options.ConfigureWarnings(warnings =>
            {
                warnings.Log(RelationalEventId.TransactionCommitted);
                warnings.Log(RelationalEventId.TransactionError);
                warnings.Log((RelationalEventId.CommandExecuting, LogLevel.Information));

                warnings.Log((SqlServerEventId.SavepointsDisabledBecauseOfMARS, LogLevel.Warning));
                warnings.Log((RelationalEventId.MultipleCollectionIncludeWarning, LogLevel.Warning));

                warnings.Throw(RelationalEventId.QueryPossibleUnintendedUseOfEqualsWarning);
            });

            options.UseSqlServer(configuration.GetConnectionString("RadarLiteContextConnection"), sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure();
                sqlOptions.MigrationsAssembly(typeof(RadarLiteContext).Assembly.GetName().Name);
            });
        });

        ServiceProvider serviceProvider = services.BuildServiceProvider();
        return serviceProvider.GetService<RadarLiteContext>();
    }
}

