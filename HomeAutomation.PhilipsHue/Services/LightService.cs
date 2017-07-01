using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using DAS.Infrastructure.WebClient;
using HomeAutomation.PhilipsHue.Bridge;
using HomeAutomation.PhilipsHue.Lights;
using HomeAutomation.PhilipsHue.Services.HueApi.Lights;

namespace HomeAutomation.PhilipsHue.Services
{
    public class LightService
    {
        private readonly WebClient _webClient;

        public LightService()
        {
            _webClient = new WebClient();
        }

        public async Task SetLightName(int lightId, string name, HueBridge bridge)
        {
            var request = new SetLightNameRequest(lightId, name, bridge);

            // TODO: error handling?
            await _webClient
                .SendAsync(request)
                .ConfigureAwait(false);
        }

        public async Task SetLightState(int lightId, HueBridge bridge, LightState state)
        {
            var request = new SetLightStateRequest(lightId, bridge, state);
            // TODO: error handling?
            var r = await _webClient
                .SendAsync(request)
                .ConfigureAwait(false);

            Trace.WriteLine(r.Content.ReadAsStringAsync().Result);
        }

        public async Task<Dictionary<int, HueLight>> GetLightSetup(HueBridge bridge)
        {
            var request = new GetLightsRequest(bridge.Username, bridge);
            var response = await _webClient
                .SendAsync<Dictionary<int, HueLight>>(request)
                .ConfigureAwait(false);

            return response;
        }
    }
}
