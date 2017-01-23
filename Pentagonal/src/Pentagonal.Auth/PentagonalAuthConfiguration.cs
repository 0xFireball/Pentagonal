using LiteDB;
using Pentagonal.Auth.Services;

namespace Pentagonal.Auth
{
    public class PentagonalAuthConfiguration
    {
        public LiteDatabase Database { get; set; }
        public string UserTableName { get; set; } = "Pentagonal.Auth.Users";
        public PasswordCryptoConfiguration PasswordCryptoConfiguration { get; set; } = PasswordCryptoConfiguration.CreateDefault();
    }
}