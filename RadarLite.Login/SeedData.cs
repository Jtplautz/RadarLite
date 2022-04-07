using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Duende.IdentityServer.EntityFramework.Storage;
using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using RadarLite.Identity.Models;

namespace RadarLite.Login;
public class SeedData {
    public static void EnsureSeedData(string connectionString)
    {
        try
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddDbContext<RadarLiteIdentityContext>(options =>
               options.UseSqlServer(connectionString, o => o.MigrationsAssembly(typeof(Program).Assembly.FullName)));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<RadarLiteIdentityContext>()
                .AddDefaultTokenProviders();

            services.AddOperationalDbContext(
                options =>
                {
                    options.ConfigureDbContext = db =>
                    db.UseSqlServer(connectionString,
                    sql => sql.MigrationsAssembly(typeof(SeedData).Assembly.FullName));
                });
            services.AddConfigurationDbContext(
                options =>
                {
                    options.ConfigureDbContext = db =>
                    db.UseSqlServer(connectionString,
                    sql => sql.MigrationsAssembly(typeof(SeedData).Assembly.FullName));
                });
            using (var serviceProvider = services.BuildServiceProvider())
            {
                using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    scope.ServiceProvider.GetService<PersistedGrantDbContext>().Database.Migrate();
                    var configContext = scope.ServiceProvider.GetService<ConfigurationDbContext>();
                    configContext.Database.Migrate();

                    EnsureSeedData(configContext);


                    scope.ServiceProvider.GetService<RadarLiteIdentityContext>().Database.Migrate();
                    EnsureUsers(scope);
                }
            }

        }
        catch(Exception ex)
        {
            Log.Fatal(ex, "unhandled exception during start up.");

        }
        finally
        {
            Log.Information("Terminating...");
            Log.CloseAndFlush();
        }
    }
    public static void EnsureUsers(IServiceScope scope) 
    {
        var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var sys = userMgr.FindByNameAsync("thesystem").Result;
        if (sys == null)
        {
            sys = new IdentityUser
            {
                UserName = "thesystem",
                Email = "radarlite@email.com",
                EmailConfirmed = true,
            };
            var result = userMgr.CreateAsync(sys, "Pass123$").Result;

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            result = userMgr.AddClaimsAsync(sys, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "The System"),
                            new Claim(JwtClaimTypes.GivenName, "The"),
                            new Claim(JwtClaimTypes.FamilyName, "System"),
                            new Claim(JwtClaimTypes.WebSite, "http://sys.com"),
                        }).Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            Log.Debug("the system created");
        }
        else
        {
            Log.Debug("the system already exists");
        }
    }
    public static void EnsureSeedData(ConfigurationDbContext context) 
    {
        if (!context.Clients.Any())
        {
            foreach (var client in Configuration.Clients.ToList())
            {
                context.Clients.Add(client.ToEntity());
            }
            context.SaveChanges();
        }

        if (!context.IdentityResources.Any())
        {
            foreach (var resource in Configuration.IdentityResources.ToList())
            {
                context.IdentityResources.Add(resource.ToEntity());

            }
            context.SaveChanges();

        }
        if (!context.ApiScopes.Any())
        {
            foreach (var apiScope in Configuration.ApiScopes.ToList())
            {
                context.ApiScopes.Add(apiScope.ToEntity());
            }
            context.SaveChanges();
        }
    }
}

