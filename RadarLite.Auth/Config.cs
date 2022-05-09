using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace IdentityServerAspNetIdentity;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("NWS.Wind", "Wind"),
            new ApiScope("NWS.Temperature", "Temp"),
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new ApiResource[]
        {
            new ApiResource("RadarLite.NationalWeatherService") 
            {
                Scopes = new List<string> { "NWS.Wind", "NWS.Temperature" },
                ApiSecrets = new List<Secret>{ new Secret("initsecret".Sha256()) },
                UserClaims = new List<string> { "base" }
            }
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            // machine to machine client
            new Client
            {
                ClientId = "client",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                // scopes that client has access to
                AllowedScopes = { "NWS.Wind" }
            },

            // interactive ASP.NET Core Web App
            new Client
            {
                ClientId = "RadarLiteClient",
                ClientSecrets = { new Secret("vuesecret".Sha256()) },

                AllowedGrantTypes = GrantTypes.ClientCredentials,

                // where to redirect to after login
                RedirectUris = { "https://localhost:7056/signin-oidc" },

                // where to redirect to after logout
                PostLogoutRedirectUris = { "https://localhost:7056/signout-callback-oidc" },
                
                AllowOfflineAccess = true,
                
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "NWS.Wind",
                    "NWS.Temperature"
                }
            }
        };
}
