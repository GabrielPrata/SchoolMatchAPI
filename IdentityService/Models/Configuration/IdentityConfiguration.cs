using Duende.IdentityServer.Models;

namespace IdentityService.Models.Configuration
{
    public static class IdentityConfiguration
    {
        public const string User = "User";

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes() =>
            new List<ApiScope>
            {
                new ApiScope("schoolMatch", "AccountService"),
                new ApiScope(name: "read", "Read Data"),
                new ApiScope(name: "write", "Write Data"),
                new ApiScope(name: "delete", "Delete Data"),
            };

        public static IEnumerable<Client> GetClients(IConfiguration configuration)
        {
            var clientSecret = configuration["IdentitySettings:ClientSecret"]; // Obtém do appsettings.json

            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    ClientSecrets = { new Secret(clientSecret.Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = {"read", "write", "profile"}
                }
            };
        }
    };
}
