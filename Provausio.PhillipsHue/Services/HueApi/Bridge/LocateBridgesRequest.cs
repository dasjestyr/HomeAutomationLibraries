using System.Net.Http;
using Provausio.Core.WebClient;
using Provausio.Core.WebClient.Infrastructure;

namespace Provausio.PhillipsHue.Services.HueApi.Bridge
{
    public class LocateBridgesRequest : WebRequest
    {
        public LocateBridgesRequest() 
            : base(HttpMethod.Get)
        {
        }

        protected override void SetRequest(IResourceBuilder builder)
        {
            builder
                .WithScheme(Scheme.Https)
                .WithHost("www.meethue.com")
                .WithPath("api/nupnp");
        }
    }
}