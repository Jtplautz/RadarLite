using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RadarLite.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarLite.Deploying;
public class JCRadarLiteContextFactory : IDesignTimeDbContextFactory<RadarLiteContext> {
    public RadarLiteContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
                                            .SetBasePath(Directory.GetCurrentDirectory())
                                            .AddJsonFile("appsettings.json")
                                            .Build();

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
