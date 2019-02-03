using Newtonsoft.Json;

namespace Provausio.PhillipsHue.Services.HueApi.Bridge
{
    public class PairingSuccessMessage
    {
        [JsonProperty("username")]
        public string Username { get; set; }
    }
}