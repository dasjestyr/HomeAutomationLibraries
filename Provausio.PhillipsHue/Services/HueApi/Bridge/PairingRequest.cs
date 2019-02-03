using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Provausio.PhillipsHue.Bridge;

namespace Provausio.PhillipsHue.Services.HueApi.Bridge
{
    public class PairingRequest : PhilipsHueRequest
    {
        private readonly string _appName;
        private readonly string _deviceName;

        public PairingRequest(string appName, string deviceName, HueBridge bridge) 
            : base(HttpMethod.Post, bridge)
        {
            _appName = appName;
            _deviceName = deviceName;
        }

        public override HttpContent GetContent()
        {
            var body = new {devicetype = $"{_appName}#{_deviceName}"};
            var asJson = JsonConvert.SerializeObject(body);
            return new StringContent(asJson, Encoding.UTF8, "application/json");
        }
    }
}