using System;
using System.Linq;
using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RadarLite.Identity.Models;
using Serilog;

namespace RadarLite.Signin; 
public class SeedData {
    public static void EnsureSeedData(string identityConnectionString)
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddDbContext<RadarLiteIdentityContext>(b =>
           b.UseSqlServer(identityConnectionString, opt =>
        opt.MigrationsAssembly(typeof(Program).Assembly.FullName)));

        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<RadarLiteIdentityContext>()
            .AddDefaultTokenProviders();

        using (var serviceProvider = services.BuildServiceProvider())
        {
            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<RadarLiteIdentityContext>();
                context.Database.Migrate();

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
                            new Claim(JwtClaimTypes.WebSite, "http://thesystem.com"),
                        }).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                    Log.Information("system created!");
                }
                else
                {
                    Log.Information("system already exists");
                }
            }
        }
    }
}

