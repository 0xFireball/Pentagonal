using LiteDB;
using Nancy;
using Pentagonal.Auth.Configuration;
using Pentagonal.Auth.Services;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Pentagonal.Auth
{
    public class PentagonalAuthConfiguration
    {
        public LiteDatabase Database { get; set; }
        public string UserTableName { get; set; } = "Pentagonal_Auth_Users";
        public PasswordCryptoConfiguration PasswordCryptoConfiguration { get; set; } = PasswordCryptoConfiguration.CreateDefault();

        public Func<NancyContext, Task<ClaimsPrincipal>> ResolveUserIdentity { get; set; } = ApiClientAuthenticationService.GetDefaultResolver();

        public RegistrationRestrictions RegistrationRestrictions { get; set; } = new RegistrationRestrictions();

        public AccountSecurityRequirements AccountSecurityRequirements { get; set; } = new AccountSecurityRequirements();

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