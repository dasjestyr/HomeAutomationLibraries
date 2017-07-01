using Newtonsoft.Json;

namespace HomeAutomation.PhilipsHue.Lights
{
    public class HueLightAdjustment : LightState
    {
        /// <summary>
        /// Value from 0 to 65535. Works in increments of 100ms. Default is 4 (400ms). 10 = 1 second.
        /// </summary>
        [JsonProperty("transitiontime")]
        public ushort TransitionTime { get; set; }

        /// <summary>
        /// Value from -254 to 254
        /// </summary>
        [JsonProperty("bri_inc")]
        public int IncrementBrightness { get; set; }

        /// <summary>
        /// Value from -254 to 254
        /// </summary>
        [JsonProperty("sat_inc")]
        public int IncrementSaturation { get; set; }

        /// <summary>
        /// Value from -65534 to 65534
        /// </summary>
        [JsonProperty("hue_inc")]
        public int IncrementHue { get; set; }

        /// <summary>
        /// Value from -65534 to 65534
        /// </summary>
        [JsonProperty("ct_inc")]
        public int IncrementColorTemperature { get; set; }

        /// <summary>
        /// Can only contain 2 values, each ranging from 0 to 1
        /// </summary>
        [JsonProperty("xy_inc")]
        public float[] IncrementCieColorSpace { get; set; } = {0f, 0f};
    }
}