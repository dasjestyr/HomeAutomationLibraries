using System.Net.Http;
using DAS.Infrastructure.WebClient;
using DAS.Infrastructure.WebClient.Infrastructure;
using HomeAutomation.PhilipsHue.Bridge;

namespace HomeAutomation.PhilipsHue.Services.HueApi
{
    public abstract class PhilipsHueRequest : WebRequest
    {
        protected readonly HueBridge Bridge;
        protected const string Resource = "api";

        protected PhilipsHueRequest(HttpMethod method, HueBridge bridge)
            : base(method)
        {
            Bridge = bridge;
        }

        protected override void SetRequest(IResourceBuilder builder)
        {
            builder
                .WithScheme(Scheme.Http)
                .WithHost($"{Bridge.IpAddress}/api");
        }
    }
}