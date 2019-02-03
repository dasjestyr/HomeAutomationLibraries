using Newtonsoft.Json;

namespace Provausio.PhillipsHue.Bridge
{
    public class DeviceType
    {
        [JsonProperty("bridge")]
        public bool IsBridge { get; set; }

        [JsonProperty("lights")]
        public int[] Lights { get; set; }

        [JsonProperty("sensors")]
        public int[] Sensors { get; set; }
    }
}