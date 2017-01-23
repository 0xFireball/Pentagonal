using Newtonsoft.Json;
using Pentagonal.Auth.Services;

namespace Pentagonal.Auth.Models
{
    public class RegisteredUser
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonIgnore]
        public string ApiKey { get; set; }

        [JsonIgnore]
        public byte[] PasswordKey { get; set; }

        [JsonIgnore]
        public byte[] CryptoSalt { get; set; }

        [JsonIgnore]
        public PasswordCryptoConfiguration PasswordCryptoConf { get; set; }

        [JsonIgnore]
        public string Identifier { get; set; }

        [JsonIgnore]
        public bool Enabled { get; set; } = true;
    }
}