using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace RadarLite.Signin;
public class Configuration {
    public static IEnumerable<IdentityResource> IdentityResources =>
  new List<IdentityResource>
  {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
        new IdentityResource
        {
          Name = "role",
          UserClaims = new List<string> {"role"}
        }
  };
    public static IEnumerable<ApiResource> ApiResources =>
        new List<ApiResource> {
            new ApiResource("NationalWeatherServiceAPI"){
                Scopes = new List<string> {"openid","profile","email"},
                ApiSecrets = new List<Secret> {new Secret("surpise".Sha256())},
                UserClaims = new List<string> {"role"}
            }
        };
    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope> {
            new ApiScope("radarlite.api", "RadarLite API")
        };

    public static IEnumerable<Client> Clients =>
        new List<Client> {
            //machine to machine client
            new Client {
                ClientId = "m2m",

                // no interactive user, use the clientid/secret for authentication
                AllowedGrantTypes = GrantTypes.ClientCredentials,

                // secret for authentication
                ClientSecrets = { new Secret("secret".Sha256())},
                RedirectUris = { "http://localhost:7502/signin-oidc" },
                FrontChannelLogoutUri =  "http://localhost:7502/signout-oidc",
                PostLogoutRedirectUris = { "http://localhost:7502/signout-callback-oidc" },
                AllowOfflineAccess = true,
                // scopes that client has access to
                 AllowedScopes = { "radarlite.api" }
            },

            // interactive ASP.NET Core MVC client
            new Client
            {
                ClientId = "minmalAPI",
                AllowedGrantTypes = GrantTypes.Code,
                ClientSecrets = { new Secret("secret".Sha256()) },

                // where to redirect after login
                RedirectUris = { "http://localhost:7502/signin-oidc" },

                // where to redirect after logout
                PostLogoutRedirectUris = { "http://localhost:7502/signout-callback-oidc" },

                AllowOfflineAccess = true,

                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "radarlite.api"
                }
            }
        };
}

