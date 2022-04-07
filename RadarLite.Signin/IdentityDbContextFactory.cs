using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;
using RadarLite.Identity.Models;

namespace RadarLite.Signin;
public class RadarLiteIdentityContextFactory : IDesignTimeDbContextFactory<RadarLiteIdentityContext> {
    public RadarLiteIdentityContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
                                            .SetBasePath(Directory.GetCurrentDirectory())
                                            .AddJsonFile("appsettings.json")
                                            .Build();

        IServiceCollection services = new ServiceCollection();
        services.AddDbContextPool<RadarLiteIdentityContext>(options =>
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
                sqlOptions.MigrationsAssembly(typeof(RadarLiteIdentityContext).Assembly.GetName().Name);
            });
        });

        ServiceProvider serviceProvider = services.BuildServiceProvider();
        return serviceProvider.GetService<RadarLiteIdentityContext>();
    }
}
