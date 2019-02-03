using Newtonsoft.Json;

namespace Provausio.PhillipsHue.Bridge
{
    public class Backup
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("errorcode")]
        public int ErrorCode { get; set; }
    }
}