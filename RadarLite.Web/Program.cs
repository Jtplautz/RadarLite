using Serilog;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using RadarLite.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
try

{
    // Add services to the container.
#pragma warning disable CS8604 // We will catch if null.
    builder.Host.UseSerilog((ctx, lc) => lc
            .ReadFrom.Configuration(ctx.Configuration)
            .WriteTo.Seq(builder.Configuration.GetConnectionString("Seq")));
#pragma warning restore CS8604

    builder.WebHost.UseWebRoot(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"));
    builder.Services.AddAuthorization();
    builder.Services.AddControllers();
    builder.Services.AddBff();
    JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

    builder.Services.AddSpaStaticFiles(configuration =>
    {
        configuration.RootPath = "Client/dist";
    });

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = "Cookies";
        options.DefaultChallengeScheme = "oidc";
        options.DefaultSignOutScheme = "oidc";
    })
       .AddCookie("Cookies")
       .AddOpenIdConnect("oidc", options =>
       {
           options.Authority = "https://localhost:7056";

           options.ClientId = "RadarLiteClient";
           options.ClientSecret = "0/6t7wnncRj4pwHTXkh6tGF8vpIYsr2YQsMWIB4sTbY=";
           options.ResponseType = "code";
           options.CallbackPath = "/signin-oidc";
           options.Scope.Add("openid");
           options.Scope.Add("profile");
           options.Scope.Add("NWS.Wind");
           options.Scope.Add("NWS.Temperature");

           options.SaveTokens = true;
           options.GetClaimsFromUserInfoEndpoint = true;
       });

    var app = builder.Build();
    app.Logger.LogWarning("RadarLite.Web Started.");

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseSpaStaticFiles();
    app.UseRouting();
    app.UseAuthentication();
    app.UseBff();
    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapBffManagementEndpoints();
    });

    if (app.Environment.IsDevelopment())
    {
        app.UseSpa(spa =>
        {
                // use 'build-dev' npm script
                spa.Options.StartupTimeout = TimeSpan.FromSeconds(10);
            spa.Options.SourcePath = "Client";
            spa.UseViteDevelopmentServer();
        });

    }

    app.Run();
}

catch (Exception ex)
{
    Log.Logger.Error("Vite server failed to start.", ex);
    Log.CloseAndFlush();
}