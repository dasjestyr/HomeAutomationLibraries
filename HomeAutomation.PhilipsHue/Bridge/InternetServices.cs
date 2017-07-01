using Newtonsoft.Json;

namespace HomeAutomation.PhilipsHue.Bridge
{
    public class InternetServices
    {
        [JsonProperty("remoteaccess")]
        public string RemoteAccess { get; set; }
    }
}