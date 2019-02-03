using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Provausio.Core.WebClient;
using Provausio.PhillipsHue.Bridge;
using Provausio.PhillipsHue.Lights;
using Provausio.PhillipsHue.Services.HueApi.Lights;

namespace Provausio.PhillipsHue.Services
{
    public interface ILightService
    {
        Task SetLightName(int lightId, string name, HueBridge bridge);
        Task SetLightState(int lightId, HueBridge bridge, LightStateAdjustment state);
        Task<Dictionary<int, HueLight>> GetLightSetup(HueBridge bridge);
    }

    public class LightService : ILightService
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

        public async Task SetLightState(int lightId, HueBridge bridge, LightStateAdjustment state)
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

            foreach (var light in response)
                light.Value.Id = light.Key;

            return response;
        }
    }
}
