using Newtonsoft.Json;

namespace Pentagonal.Auth.Models
{
    public class RemoteAuthResponse
    {
        [JsonProperty("user")]
        public RegisteredUser User { get; set; }

        [JsonProperty("apikey")]
        public string ApiKey { get; set; }
    }
}