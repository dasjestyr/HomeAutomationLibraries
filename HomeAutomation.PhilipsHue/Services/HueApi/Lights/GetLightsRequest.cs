using System.Net.Http;
using DAS.Infrastructure.WebClient;
using HomeAutomation.PhilipsHue.Bridge;

namespace HomeAutomation.PhilipsHue.Services.HueApi.Lights
{
    public class GetLightsRequest : PhilipsHueRequest
    {
        private readonly string _userId;

        public GetLightsRequest(string userId, HueBridge bridge) 
            : base(HttpMethod.Get, bridge)
        {
            _userId = userId;
        }

        protected override void SetRequest(IResourceBuilder builder)
        {
            base.SetRequest(builder);
            builder.WithPath($"{_userId}/lights");
        }
    }
}