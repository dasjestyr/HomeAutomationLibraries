using System.Net.Http;
using Provausio.Core.WebClient;
using Provausio.PhillipsHue.Bridge;
using Provausio.PhillipsHue.Services.HueApi;

namespace Provausio.PhillipsHue.Services
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