using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RadarLite.Identity.Models;
using RadarLite.Signin;
using Serilog;
var builder = WebApplication.CreateBuilder(args);

var identityConnectionString = builder.Configuration.GetConnectionString("RadarLiteIdentityContextConnection");

SeedData.EnsureSeedData(identityConnectionString);
// Add services to the container.
builder.Host.UseSerilog((ctx, lc) => lc
        .ReadFrom.Configuration(ctx.Configuration)
        .WriteTo.Seq(builder.Configuration.GetConnectionString("Seq")));

//builder.WebHost.UseWebRoot(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"));

builder.Services.AddDbContext<RadarLiteIdentityContext>(options =>
        options.UseSqlServer(identityConnectionString,
        b => b.MigrationsAssembly(typeof(Program).Assembly.GetName().Name)));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<RadarLiteIdentityContext>()
    .AddDefaultTokenProviders();

builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;

    // see https://docs.duendesoftware.com/identityserver/v5/fundamentals/resources/
    options.EmitStaticAudienceClaim = true;
})
    .AddInMemoryApiScopes(Configuration.ApiScopes)
    .AddInMemoryClients(Configuration.Clients)
    .AddInMemoryIdentityResources(Configuration.IdentityResources)
    .AddInMemoryApiResources(Configuration.ApiResources)
    .AddAspNetIdentity<IdentityUser>()
    //.AddConfigurationStore(options =>
    //{
    //    options.ConfigureDbContext = b =>
    //    b.UseSqlServer(identityConnectionString, opt =>
    //    opt.MigrationsAssembly(typeof(Program).Assembly.FullName));
    //})
    .AddOperationalStore(options =>
    {
        options.ConfigureDbContext = b =>
        b.UseSqlServer(identityConnectionString, opt =>
        opt.MigrationsAssembly(typeof(Program).Assembly.FullName));
    })
    .AddDeveloperSigningCredential();

builder.Services.AddAuthorization(options =>
{
    // By default, all incoming requests will be authorized according to the default policy.
    options.FallbackPolicy = options.DefaultPolicy;
});//.AddGoogle(options =>
//{
//    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

//    // register your IdentityServer with Google at https://console.developers.google.com
//    // enable the Google+ API
//    // set the redirect URI to https://localhost:5001/signin-google
//    options.ClientId = "copy client ID from Google here";
//    options.ClientSecret = "copy client secret from Google here";
//});
//builder.Services.AddAuthentication("Bearer").AddIdentityServerAuthentication
builder.Services.AddControllersWithViews();
var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();
