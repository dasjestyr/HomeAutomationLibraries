using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using HomeAutomation.PhilipsHue.Bridge;
using HomeAutomation.PhilipsHue.Services;

namespace HomeAutomation.PhilipsHue.Lights
{
    public class LightStateManager
    {
        private readonly ILightService _lightService;

        public LightStateManager(ILightService lightService)
        {
            _lightService = lightService;
        }

        public async Task<IDictionary<int, HueLight>> GetCurrentSetup(HueBridge bridge)
        {
            return await _lightService.GetLightSetup(bridge);
        }

        public async Task SaveConfiguration(HueBridge bridge, IDictionary<int, HueLight> lightConfiguration)
        {
            
        }
    }
}
