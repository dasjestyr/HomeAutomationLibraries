using System;
using Newtonsoft.Json;

namespace HomeAutomation.PhilipsHue.Lights
{
    [Serializable]
    public class LightState
    {
        [JsonProperty("on")]
        public bool IsOn { get; set; }

        /// <summary>
        /// Value from 1 -254
        /// </summary>
        [JsonProperty("bri")]
        public byte Brightness { get; set; }

        /// <summary>
        /// Value from 0 to 65535. Min and max values are red, 25500 is green, and 46920 is blue.
        /// </summary>
        [JsonProperty("hue")]
        public ushort Hue { get; set; }

        /// <summary>
        /// Should only be 2 values, each ranging from 0 to 1.
        /// </summary>
        [JsonProperty("xy")]
        public float[] CieColorSpace { get; set; }

        /// <summary>
        /// Value from 0 to 65535. Greater numbers are warmer (more red).
        /// </summary>
        [JsonProperty("ct")]
        public ushort ColorTemperature { get; set; }

        /// <summary>
        /// Value from 1 -254
        /// </summary>
        [JsonProperty("sat")]
        public byte Saturation { get; set; }

        /// <summary>
        /// "none" and "colorloop" are supported.
        /// </summary>
        [JsonProperty("effect")]
        public string Effect { get; set; }

        /// <summary>
        /// "none" =  no effect, "select" = single breathe cycle, "lselect" = 15s breathe cycle. To interrupt lselect, send another command with "none".
        /// </summary>
        [JsonProperty("alert")]
        public string Alert { get; set; }

        [JsonProperty("colormode")]
        public string ColorMode { get; set; }

        [JsonProperty("reachable")]
        public bool IsReachable { get; set; }
    }
}