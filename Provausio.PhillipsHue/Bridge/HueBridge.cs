using Newtonsoft.Json;

namespace Provausio.PhillipsHue.Bridge
{
    public class HueBridge
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("internalipaddress")]
        public string IpAddress { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }
    }
}