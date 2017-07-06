using System.Threading.Tasks;
using HomeAutomation.PhilipsHue.Configuration.ApplicationConfiguration;
using HomeAutomation.PhilipsHue.Lights;
using HomeAutomation.PhilipsHue.Services;
using Xunit;

namespace HomeAutomation.PhilipsHue.Tests
{
    public class AuthorizationServiceTests
    {
        private string _appName = "provausio_automation_svc";
        private string _deviceType = "destkop jeremy";
        
        //[Fact]
        //public async Task GetLightingSetup()
        //{
        //    var configManager = new AppConfigurationManager(_appName, _deviceType);
        //    var bridge = configManager.AppConfiguration.HueBridges[0];
            
        //    var svc = new LightService();
        //    var lights = await svc.GetLightSetup(bridge);
        //    var light = lights[12];

        //    var newSettings = new LightStateAdjustment
        //    {
        //        IsOn = true,
        //        Alert = "lselect"
        //    };

        //    await svc.SetLightState(12, bridge, newSettings).ConfigureAwait(false);
        //}

        [Fact]
        public async Task Do()
        {
            var file = new LightConfiguration();
            
        }
    }
}
