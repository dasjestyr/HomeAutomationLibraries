using Newtonsoft.Json;

namespace Provausio.PhillipsHue.Bridge
{
    public class PortalState
    {
        [JsonProperty("signedon")]
        public bool IsSignedOn { get; set; }

        [JsonProperty("incoming")]
        public bool IsIncoming { get; set; }

        [JsonProperty("outgoing")]
        public bool IsOutgoing { get; set; }

        [JsonProperty("communication")]
        public string Communication { get; set; }
    }
}