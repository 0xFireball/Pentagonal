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
    }
}