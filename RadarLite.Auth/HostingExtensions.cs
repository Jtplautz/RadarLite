using Duende.IdentityServer;
using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using IdentityServerHost.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RadarLite.Identity.Models;
using Serilog;

namespace IdentityServerAspNetIdentity;

internal static class HostingExtensions {
    private static void InitializeDatabase(IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
        {
            serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

            var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
            context.Database.Migrate();
            if (!context.Clients.Any())
            {
                foreach (var client in Config.Clients)
                {
                    context.Clients.Add(client.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var resource in Config.IdentityResources)
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.ApiScopes.Any())
            {
                foreach (var resource in Config.ApiScopes)
                {
                    context.ApiScopes.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
        }
    }

    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        var migrationsAssembly = typeof(Program).Assembly.GetName().Name;
        builder.Services.AddRazorPages();

        RadarLite.Constants.Options.IdentityOptions identityOptions = new();

        builder.Configuration.GetSection(nameof(RadarLite.Constants.Options.IdentityOptions)).Bind(identityOptions);

        builder.Services.AddDbContext<RadarLiteIdentityContext>(
            options => options.UseSqlServer(
                builder.Configuration.GetConnectionString("RadarLiteIdentityContextConnection"), 
            b => b.MigrationsAssembly(migrationsAssembly)));

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<RadarLiteIdentityContext>()
            .AddDefaultTokenProviders();

        builder.Services
            .AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/
                options.EmitStaticAudienceClaim = true;
            })
            .AddAspNetIdentity<ApplicationUser>()
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlServer(builder.Configuration.GetConnectionString("RadarLiteIdentityContextConnection"),
                    sql => sql.MigrationsAssembly(migrationsAssembly));
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlServer(builder.Configuration.GetConnectionString("RadarLiteIdentityContextConnection"),
                    sql => sql.MigrationsAssembly(migrationsAssembly));
            })
            .AddDeveloperSigningCredential();

        //builder.Services.AddCors(options =>
        //{
        //    options.AddPolicy("RadarLiteCorsOrigins",
        //                          builder =>
        //                          {
        //                              builder
        //                              .WithHeaders("Access-Control-Allow-Origin")
        //                              .WithMethods("*")
        //                              .WithOrigins("http://RadarLite.Web.me:7505", "http://192.168.254.125:7505", "http://192.168.1.192:7505", "http://192.168.1.192:3000");
        //                          });
        //});

        builder.Services.AddAuthentication()
            .AddOpenIdConnect("oidc", "RadarLiteIdentityServer", options =>
            {
                options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                options.SignOutScheme = IdentityServerConstants.SignoutScheme;
                options.SaveTokens = true;

                //radarliteidentity
                options.Authority = "https://localhost:7056";
                options.ClientId = "RadarLiteClient";
                options.ClientSecret = "secret";
                options.ResponseType = "code";
                options.CallbackPath = "/signin-oidc";

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = "name",
                    RoleClaimType = "role"
                };
            });
        builder.ConfigureCustomCorsPolicy();
        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseCors("RadarLiteIdentityCorsOrigins");
        app.UseStaticFiles();
        app.UseRouting();
        app.UseIdentityServer();
        app.UseAuthorization();
        
        app.MapRazorPages()
            .RequireAuthorization();

        return app;
    }

    public static WebApplicationBuilder ConfigureCustomCorsPolicy(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("RadarLiteIdentityCorsOrigins",
                                  builder =>
                                  {
                                      builder
                                      .WithHeaders("*")
                                      .WithMethods("*")
                                      .WithOrigins("http://RadarLite.Web.me:7505", "http://192.168.254.125:7505", "http://192.168.1.192:7505", "https://192.168.1.192:5000", "https://localhost:5000");
                                  });
        });

        return builder;
    }
}