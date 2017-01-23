using LiteDB;
using Nancy;
using Pentagonal.Auth.Services;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Pentagonal.Auth
{
    public class PentagonalAuthConfiguration
    {
        public LiteDatabase Database { get; set; }
        public string UserTableName { get; set; } = "Pentagonal.Auth.Users";
        public PasswordCryptoConfiguration PasswordCryptoConfiguration { get; set; } = PasswordCryptoConfiguration.CreateDefault();

        public Func<NancyContext, Task<ClaimsPrincipal>> ResolveUserIdentity { get; set; } = async (ctx) =>
        {
            // Check for the API key
            string accessToken = null;
            if (ctx.Request.Query.apikey.HasValue)
            {
                accessToken = ctx.Request.Query.apikey;
            }
            else if (ctx.Request.Form["apikey"].HasValue)
            {
                accessToken = ctx.Request.Form["apikey"];
            }

            // Authenticate the request
            return accessToken == null ? null : await ApiClientAuthenticationService.ResolveClientIdentity(accessToken);
        };

        public bool IsValid
        {
            get
            {
                return (Database != null)
                    && (ResolveUserIdentity != null);
            }
        }
    }
}