using Duende.IdentityServer.Models;

namespace RadarLite.Login;
public class Configuration {
    public static IEnumerable<IdentityResource> IdentityResources =>
  new[]
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
            new ApiScope("openid", "Open Id"),
            new ApiScope("profile", "Profile"),
            new ApiScope("email", "Email")
        };

    public static IEnumerable<Client> Clients =>
        new List<Client> {
            new Client {
                ClientId = "client",

                // no interactive user, use the clientid/secret for authentication
                AllowedGrantTypes = GrantTypes.ClientCredentials,

                // secret for authentication
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                RedirectUris = { "http://localhost:7502/signin-oidc" },
                FrontChannelLogoutUri =  "http://localhost:7502/signout-oidc",
                PostLogoutRedirectUris = { "http://localhost:7502/signout-callback-oidc" },
                AllowOfflineAccess = true,
                // scopes that client has access to
                AllowedScopes = { "openid","profile","email" }
            }
        };
}

