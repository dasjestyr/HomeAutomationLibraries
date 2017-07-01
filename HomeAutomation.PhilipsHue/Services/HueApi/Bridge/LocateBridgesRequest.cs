using System.Net.Http;
using DAS.Infrastructure.WebClient;
using DAS.Infrastructure.WebClient.Infrastructure;

namespace HomeAutomation.PhilipsHue.Services.HueApi.Bridge
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