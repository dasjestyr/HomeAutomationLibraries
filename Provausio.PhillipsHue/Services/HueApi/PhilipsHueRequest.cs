using System.Net.Http;
using Provausio.Core.WebClient;
using Provausio.Core.WebClient.Infrastructure;
using Provausio.PhillipsHue.Bridge;

namespace Provausio.PhillipsHue.Services.HueApi
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