using System.Collections.Generic;
using Newtonsoft.Json;

namespace HomeAutomation.PhilipsHue.Lights
{
    public class Scene
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("lights")]
        public Dictionary<int, HueLight> Lights { get; set; }
    }
}
