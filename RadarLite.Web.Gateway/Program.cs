using IdentityModel.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
        .ReadFrom.Configuration(ctx.Configuration)
        .WriteTo.Seq(builder.Configuration.GetConnectionString("Seq")));
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => 
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "RadarLite Gateway API.", Version = "v1" })
);

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
    options.DefaultSignOutScheme = "oidc";
})
   .AddCookie("Cookies")
   .AddOpenIdConnect("oidc", options =>
   {
       options.Authority = "https://localhost:5001";

       options.ClientId = "RadarLite";
       options.ClientSecret = "secret";
       options.ResponseType = "code";
       options.Scope.Add("api1");

       options.SaveTokens = true;
       options.GetClaimsFromUserInfoEndpoint = true;
   });

builder.Services.AddAuthorization();
builder.Services.AddBff();

builder.Services.AddAccessTokenManagement();
var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();

//https://github.com/bnolan001/BackendForFrontEnd
app.Use(async (context, next) =>
{
    // We want the entire application to enforce authentication from the start
    if (!context.User.Identity.IsAuthenticated)
    {
        // No need to have a custom page for authentication. This call will redirect
        // the user to the Identity Server login page
        await context.ChallengeAsync();
        return;
    }

    await next();
});
app.UseRouting();

//});
app.Run();
