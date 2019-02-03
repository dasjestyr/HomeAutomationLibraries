using Newtonsoft.Json;

namespace Provausio.PhillipsHue.Services.HueApi.Bridge
{
    public class PairingResponseMessage 
    {
        [JsonProperty("success")]
        public PairingSuccessMessage Succcess { get; set; }

        [JsonProperty("error")]
        public HueErrorMessage Error { get; set; }
    }
}