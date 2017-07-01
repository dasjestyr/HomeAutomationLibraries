using Newtonsoft.Json;

namespace HomeAutomation.PhilipsHue.Services.HueApi.Bridge
{
    public class PairingResponseMessage 
    {
        [JsonProperty("success")]
        public PairingSuccessMessage Succcess { get; set; }

        [JsonProperty("error")]
        public HueErrorMessage Error { get; set; }
    }
}