using System.Net.Http;
using DAS.Infrastructure.WebClient;
using HomeAutomation.PhilipsHue.Bridge;
using HomeAutomation.PhilipsHue.Services.HueApi;

namespace HomeAutomation.PhilipsHue.Services
{
    public class GetBridgeConfigurationRequest : PhilipsHueRequest
    {
        public GetBridgeConfigurationRequest(HueBridge bridge) 
            : base(HttpMethod.Get, bridge)
        {
        }

        protected override void SetRequest(IResourceBuilder builder)
        {
            base.SetRequest(builder);
            builder.WithPath($"{Bridge.Username}/config");
        }
    }
}