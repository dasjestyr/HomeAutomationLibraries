using Newtonsoft.Json;

namespace HomeAutomation.PhilipsHue.Bridge
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