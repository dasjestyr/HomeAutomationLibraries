using Newtonsoft.Json;

namespace HomeAutomation.PhilipsHue.Lights
{
    public class LightStateAdjustment
    {
        [JsonProperty("on")]
        public bool? IsOn { get; set; }

        /// <summary>
        /// Value from 1 -254
        /// </summary>
        [JsonProperty("bri")]
        public byte? Brightness { get; set; }

        /// <summary>
        /// Value from 0 to 65535. Min and max values are red, 25500 is green, and 46920 is blue.
        /// </summary>
        [JsonProperty("hue")]
        public ushort? Hue { get; set; }

        /// <summary>
        /// Should only be 2 values, each ranging from 0 to 1.
        /// </summary>
        [JsonProperty("xy")]
        public float[] CieColorSpace { get; set; }

        /// <summary>
        /// Value from 0 to 65535. Greater numbers are warmer (more red).
        /// </summary>
        [JsonProperty("ct")]
        public ushort? ColorTemperature { get; set; }

        /// <summary>
        /// Value from 1 to 254
        /// </summary>
        [JsonProperty("sat")]
        public byte? Saturation { get; set; }

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

        /// <summary>
        /// Value from 0 to 65535. Works in increments of 100ms. Default is 4 (400ms). 10 = 1 second.
        /// </summary>
        [JsonProperty("transitiontime")]
        public ushort? TransitionTime { get; set; }

        /// <summary>
        /// Value from -254 to 254
        /// </summary>
        [JsonProperty("bri_inc")]
        public int? IncrementBrightness { get; set; }

        /// <summary>
        /// Value from -254 to 254
        /// </summary>
        [JsonProperty("sat_inc")]
        public int? IncrementSaturation { get; set; }

        /// <summary>
        /// Value from -65534 to 65534
        /// </summary>
        [JsonProperty("hue_inc")]
        public int? IncrementHue { get; set; }

        /// <summary>
        /// Value from -65534 to 65534
        /// </summary>
        [JsonProperty("ct_inc")]
        public int? IncrementColorTemperature { get; set; }

        /// <summary>
        /// Can only contain 2 values, each ranging from 0 to 1
        /// </summary>
        [JsonProperty("xy_inc")]
        public float[] IncrementCieColorSpace { get; set; }
    }
}