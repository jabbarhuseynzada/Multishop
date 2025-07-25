using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace MultiShop.IdentityServer;

public static class Config
{
    /*    public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("scope1"),
                new ApiScope("scope2"),
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // m2m client credentials flow client
                new Client
                {
                    ClientId = "m2m.client",
                    ClientName = "Client Credentials Client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                    AllowedScopes = { "scope1" }
                },

                // interactive client using code flow + pkce
                new Client
                {
                    ClientId = "interactive",
                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,

                    RedirectUris = { "https://localhost:44300/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },

                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "scope2" }
                },
            };
    */

    public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
    {
        new ApiResource("ResourceCatalog", "Catalog API")
        {
            Scopes = { "catalogapi.read", "catalogapi.write", "catalogapi.fullpermission" }
        },
        new ApiResource("ResourceOrder", "Order API")
        {
            Scopes = { "orderapi.read", "orderapi.write", "orderapi.fullpermission" }
        },
        new ApiResource("ResourceDiscount", "Discount API")
        {
            Scopes = { "discountapi.read", "discountapi.write", "discountapi.fullpermission" }
        },
        new ApiResource("ResourceCargo", "Cargo API")
        {
            Scopes = { "cargoapi.read", "cargoapi.write", "cargoapi.fullpermission" }
        },
        new ApiResource(IdentityServerConstants.LocalApi.ScopeName, "Access to local APIs")
        {
            Scopes = { IdentityServerConstants.LocalApi.ScopeName }
        }
    };
    public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
    {
        new IdentityResources.OpenId(),
        new IdentityResources.Email(),
        new IdentityResources.Profile(),
       // new IdentityResource("roles", "Your role(s)", new[] { "role" })
    };
    public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
    {
        new ApiScope("catalogapi.read", "Read access to Catalog API"),
        new ApiScope("catalogapi.write", "Write access to Catalog API"),
        new ApiScope("catalogapi.fullpermission", "Full access to Catalog API"),
        new ApiScope("orderapi.read", "Read access to Order API"),
        new ApiScope("orderapi.write", "Write access to Order API"),
        new ApiScope("orderapi.fullpermission", "Full access to Order API"),
        new ApiScope("discountapi.read", "Read access to Discount API"),
        new ApiScope("discountapi.write", "Write access to Discount API"),
        new ApiScope("discountapi.fullpermission", "Full access to Discount API"),
        new ApiScope("cargoapi.read", "Read access to Cargo API"),
        new ApiScope("cargoapi.write", "Write access to Cargo API"),
        new ApiScope("cargoapi.fullpermission", "Full access to Cargo API"),
        new ApiScope(IdentityServerConstants.LocalApi.ScopeName, "Access to local APIs")
    };

    public static IEnumerable<Client> Clients => new Client[]
    {
        new Client
        {
            ClientId = "MultiShopVisitor",
            ClientName = "MultiShop Client User",
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            ClientSecrets = { new Secret("multishop-user-secret".Sha256()) },
            AllowedScopes = { "catalogapi.read", "orderapi.read", "discountapi.read", "cargoapi.read" }
            ///AllowedScopes = { "orderapi.read", "discountapi.read" }
        },
        new Client
        {
            ClientId = "MultiShopAdmin",
            ClientName = "MultiShop Admin User",
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            ClientSecrets = { new Secret("multishop-admin-secret".Sha256()) },
            AllowedScopes = { "catalogapi.fullpermission", "orderapi.fullpermission", "discountapi.fullpermission", "cargoapi.fullpermission", 
                IdentityServerConstants.LocalApi.ScopeName,
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                IdentityServerConstants.StandardScopes.Email
            }
        },
        new Client
        {
            ClientId = "MultiShopWebApp",
            ClientName = "MultiShop Web Application",
            AllowedGrantTypes = GrantTypes.Code,
            ClientSecrets = { new Secret("multishop-webapp-secret".Sha256()) },
            RedirectUris = { "https://localhost:5001/signin-oidc" },
            PostLogoutRedirectUris = { "https://localhost:5001/signout-callback-oidc" },
            AllowedScopes = { "openid", "profile", "email", "catalogapi.read", "orderapi.read", "discountapi.read", "cargoapi.read" },
            AllowOfflineAccess = true
        }
    };
}
