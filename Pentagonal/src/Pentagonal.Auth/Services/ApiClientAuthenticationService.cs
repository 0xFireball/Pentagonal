using Nancy;
using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Pentagonal.Auth.Services
{
    public class ApiClientAuthenticationService
    {
        public static Claim StatelessAuthClaim { get; } = new Claim("authType", "stateless");
        public static string UidKey => "uid";

        public static async Task<ClaimsPrincipal> ResolveClientIdentity(string apiKey)
        {
            // Check user records in database
            var userMgr = new WebUserManager();
            var u = await userMgr.FindUserByApiKey(apiKey);
            if (u == null || !u.Enabled) return null;
            // Give client identity
            var id = new ClaimsPrincipal(new ClaimsIdentity(new GenericIdentity(u.Username, "stateless"),
                new[]
                {
                    StatelessAuthClaim
                }
            ));
            return id;
        }

        internal static Func<NancyContext, Task<ClaimsPrincipal>> GetDefaultResolver()
        {
            return async (ctx) =>
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
        }
    }
}