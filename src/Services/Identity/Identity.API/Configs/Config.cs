using Duende.IdentityServer.Models;
using IdentityModel;

namespace Identity.API.Configs;

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
            new ApiScope("library.read", "Read privilege for book catalog"),
            new ApiScope("library.modify", "Modifying privilege for book catalog"),
            new ApiScope("users.manage", "Managing privilege for users of the application"),
            new ApiScope("user.manage", "Managing privilege for personal account")
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new ApiResource[]
        {
            //new ApiResource("book", "Book Catalog API")
            //{
            //    Scopes = {"library.read", "library.modify"},
            //    UserClaims = {JwtClaimTypes.Role}
            //},
            new ApiResource("user", "User API")
            {
                Scopes = {"users.manage", "user.manage"},
                UserClaims = {JwtClaimTypes.Role}
            }
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            // interactive client using code flow + pkce
            new Client
            {
                ClientId = "SPA",
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = { "https://localhost:44300/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },

                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "library.read", "library.modify", "users.manage", "user.manage" }
            },
             new Client
            {
                ClientId = "postman",

                // No interactive user
                AllowedGrantTypes = GrantTypes.ClientCredentials,

                // Client secret for authentication
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                AllowedScopes = { "library.read" }
            }
        };
}
