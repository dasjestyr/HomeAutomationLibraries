using System;
using Newtonsoft.Json;

namespace Provausio.PhillipsHue.Lights
{
    [Serializable]
    public class HueLight
    {
        /// <summary>
        /// The plain ID of the light
        /// </summary>
        public int Id { get; set; }

        [JsonProperty("state")]
        public LightState State { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("modelid")]
        public string ModelId { get; set; }

        [JsonProperty("manufacturername")]
        public string ManufacturerName { get; set; }

        [JsonProperty("uniqueid")]
        public string UniqueId { get; set; }

        [JsonProperty("swversion")]
        public string SoftwareVersion { get; set; }
    }
}
