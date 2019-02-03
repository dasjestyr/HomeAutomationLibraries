using System.Collections.Generic;
using Newtonsoft.Json;
using Provausio.PhillipsHue.Bridge;

namespace Provausio.PhillipsHue.Configuration.ApplicationConfiguration
{
    public class AppConfigurationData
    {
        [JsonProperty("applicationName")]
        public string ApplicationName { get; set; }

        [JsonProperty("deviceType")]
        public string DeviceType { get; set; }

        [JsonProperty("bridges")]
        public List<HueBridge> HueBridges { get; set; } = new List<HueBridge>();
    }
}
