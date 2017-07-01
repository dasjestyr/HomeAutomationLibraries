using System.Collections.Generic;
using HomeAutomation.PhilipsHue.Bridge;
using Newtonsoft.Json;

namespace HomeAutomation.PhilipsHue.ApplicationConfiguration
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
