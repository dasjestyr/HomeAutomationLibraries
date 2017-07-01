using System;
using Newtonsoft.Json;

namespace HomeAutomation.PhilipsHue.Bridge
{
    public class WhiteListItem
    {
        [JsonProperty("last use date")]
        public DateTime LastUseDate { get; set; }

        [JsonProperty("create date")]
        public DateTime CreateDate { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}