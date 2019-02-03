using Newtonsoft.Json;

namespace Provausio.PhillipsHue.Services.HueApi
{
    public class HueErrorMessage
    {
        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}