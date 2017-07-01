using Newtonsoft.Json;

namespace HomeAutomation.PhilipsHue.Services.HueApi.Bridge
{
    public class PairingSuccessMessage
    {
        [JsonProperty("username")]
        public string Username { get; set; }
    }
}