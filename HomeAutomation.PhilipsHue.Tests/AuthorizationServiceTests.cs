using System.Threading.Tasks;
using HomeAutomation.PhilipsHue.ApplicationConfiguration;
using HomeAutomation.PhilipsHue.Lights;
using HomeAutomation.PhilipsHue.Services;
using Xunit;

namespace HomeAutomation.PhilipsHue.Tests
{
    public class AuthorizationServiceTests
    {
        private string _appName = "provausio_automation_svc";
        private string _deviceType = "destkop jeremy";
        
        [Fact]
        public async Task GetLightingSetup()
        {
            var configManager = new AppConfigurationManager(_appName, _deviceType);
            var bridge = configManager.AppConfiguration.HueBridges[0];

            var light = new HueLightAdjustment();
            light.IncrementBrightness = -255;

            var svc = new LightService();
            await svc.SetLightState(12, bridge, light).ConfigureAwait(false);
        }
    }
}
