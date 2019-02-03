using Newtonsoft.Json;

namespace Provausio.PhillipsHue.Bridge
{
    public class InternetServices
    {
        [JsonProperty("remoteaccess")]
        public string RemoteAccess { get; set; }
    }
}