using Newtonsoft.Json;

namespace HomeAutomation.PhilipsHue.Bridge
{
    public class Backup
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("errorcode")]
        public int ErrorCode { get; set; }
    }
}