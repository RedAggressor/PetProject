using IdentityServer4.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {
                new ApiResource("localhost:3000")
                {
                    Scopes = new List<Scope>
                    {
                        new Scope("react")
                    }
                },
                new ApiResource("www.fruitshop.com:3000")
                {
                    Scopes = new List<Scope>
                    {
                        new Scope("mvc")
                    },
                },
                new ApiResource("catalog")
                {
                    Scopes = new List<Scope>
                    {
                        new Scope("catalog.catalogbff"),
                        new Scope("catalog.catalogitem")
                    },
                },
            };
        }

        public static IEnumerable<Client> GetClients(IConfiguration configuration)
        {
            return new[]
            {
                new Client
                {
                    ClientId = "react_spa",
                    ClientName = "React SPA",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RedirectUris = { $"{configuration["ReactClientUrl"]}/callback" },
                    PostLogoutRedirectUris = { $"{configuration["ReactClientUrl"]}/" },
                    AllowedCorsOrigins = { configuration["ReactClientUrl"] },
                    AllowedScopes = { "openid", "profile", "react", "mvc", "catalog.catalogbff", "catalog.catalogitem"},                    
                    AllowAccessTokensViaBrowser = true
                    
                },
                //new Client
                //{
                //    ClientId = "mvc_pkce",
                //    ClientName = "MVC PKCE Client",
                //    AllowedGrantTypes = GrantTypes.Code,
                //    ClientSecrets = {new Secret("secret".Sha256())},
                //    RedirectUris = { $"{configuration["MvcUrl"]}/signin-oidc"},
                //    AllowedScopes = {"openid", "profile", "mvc"},
                //    RequirePkce = true,
                //    RequireConsent = false
                //},
                new Client
                {
                    ClientId = "catalog",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                },
                new Client
                {
                    ClientId = "catalogswaggerui",
                    ClientName = "Catalog Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"{configuration["CatalogApi"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{configuration["CatalogApi"]}/swagger/" },

                    AllowedScopes =
                    {
                        "catalog.catalogbff", "catalog.catalogitem", "react", "mvc"
                    }
                },
                new Client
                {
                    ClientId = "basket",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                },
                new Client
                {
                    ClientId = "basketswaggerui",
                    ClientName = "Basket Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"{configuration["BasketApi"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{configuration["BasketApi"]}/swagger/" },

                    AllowedScopes =
                    {
                        "react", "mvc"
                    }
                },
                new Client
                {
                    ClientId = "payment",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                },
                new Client
                {
                    ClientId = "paymentswaggerui",
                    ClientName = "Payment Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"{configuration["PaymentApi"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{configuration["PaymentApi"]}/swagger/" },

                    AllowedScopes =
                    {
                        "react", "mvc"
                    }
                },
            };
        }
    }
}