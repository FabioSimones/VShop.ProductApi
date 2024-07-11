using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace VShop.IdentityServer.Configuration;

public class IdentityConfiguration
{
    public const string Admin = "Admin";
    public const string Client = "Client";

    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile()
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            //VShop é aplicação web que vai acessar
            //o IdentityServer para obter o Token
            new ApiScope("vshop", "VShop Server"),
            new ApiScope(name: "read", "Read data."),
            new ApiScope(name: "write", "Write data."),
            new ApiScope(name: "delete", "Delete data.")
        };

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            //Cliente genérico
            new Client
            {
                ClientId = "client",
                ClientSecrets = {new Secret ("abracadabra#simsalabim".Sha256())},
                AllowedGrantTypes = GrantTypes.ClientCredentials, //Precisa das credenciais
                AllowedScopes = {"read", "write", "profile"}
            },
            new Client
            {
                ClientId = "vshop",
                ClientSecrets = {new Secret ("abracadabra#simsalabim".Sha256())},
                AllowedGrantTypes = GrantTypes.Code,//Via Código
                RedirectUris = { "https://localhost:7041/signin-oidc" }, //Login
                PostLogoutRedirectUris = { "https://localhost:7041/signout-callback-oidc" },//Logout
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "vshop"
                }
            }
        };            
}
