using Newtonsoft.Json;

namespace Provausio.PhillipsHue.Bridge
{
    public class SoftwareUpdateInfo
    {
        [JsonProperty("updatestate")]
        public int UpdateState { get; set; }

        [JsonProperty("checkforupdate")]
        public bool CheckForUpdate { get; set; }

        [JsonProperty("deviceTypes")]
        public DeviceType DeviceTypes { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("notify")]
        public bool Notify { get; set; }
    }
}