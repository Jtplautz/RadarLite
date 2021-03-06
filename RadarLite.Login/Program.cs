using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web.UI;
using RadarLite.Identity.Models;
using RadarLite.Login;
using Serilog;

try
{

    var seed = args.Contains("/seed");
    if (seed)
    {
        args = args.Except(new[] { "/seed" }).ToArray();
    }

    var builder = WebApplication.CreateBuilder(args);

    var identityConnectionString = builder.Configuration.GetConnectionString("RadarLiteIdentityContextConnection");
    builder.Host.UseSerilog((ctx, lc) => lc
        .ReadFrom.Configuration(ctx.Configuration)
        .WriteTo.Seq(builder.Configuration.GetConnectionString("Seq")));

    if (seed)
    {
        SeedData.EnsureSeedData(identityConnectionString);
    }


    builder.WebHost.UseWebRoot(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"));

    builder.Services.AddDbContext<RadarLiteIdentityContext>(options =>
        options.UseSqlServer(identityConnectionString,
        b => b.MigrationsAssembly(typeof(Program).Assembly.GetName().Name)));

    builder.Services.AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<RadarLiteIdentityContext>();

    builder.Services.AddIdentityServer()
        .AddInMemoryApiScopes(Configuration.ApiScopes)
        .AddInMemoryClients(Configuration.Clients)
        .AddInMemoryIdentityResources(Configuration.IdentityResources)
        .AddAspNetIdentity<IdentityUser>()
        .AddConfigurationStore(options =>
        {
            options.ConfigureDbContext = b =>
            b.UseSqlServer(identityConnectionString, opt =>
            opt.MigrationsAssembly(typeof(Program).Assembly.FullName));
        })
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
    });
    builder.Services.AddRazorPages()
        .AddMicrosoftIdentityUI();
    builder.Services.AddAuthentication();


    var app = builder.Build();
    app.UseIdentityServer();
    app.UseSerilogRequestLogging();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        app.UseDeveloperExceptionPage();
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    //app.MapRazorPages();
    //.MapControllers().RequireAuthorization("ApiScope");
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapDefaultControllerRoute();//.RequireAuthorization("ApiScope");
    });
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "unhandled exception during start up.");

}
finally 
{
    Log.Information("Terminating...");
    Log.CloseAndFlush();
}