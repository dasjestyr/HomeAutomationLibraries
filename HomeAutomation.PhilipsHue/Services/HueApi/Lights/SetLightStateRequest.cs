using System.Net.Http;
using DAS.Infrastructure.WebClient;
using HomeAutomation.PhilipsHue.Bridge;
using HomeAutomation.PhilipsHue.Ext;
using HomeAutomation.PhilipsHue.Lights;

namespace HomeAutomation.PhilipsHue.Services.HueApi.Lights
{
    public class SetLightStateRequest : PhilipsHueRequest
    {
        private readonly LightState _lightState;
        private readonly int _lightId;

        public SetLightStateRequest(int lightId, HueBridge bridge, LightState state) 
            : base(HttpMethod.Put, bridge)
        {
            _lightState = state;
            _lightId = lightId;
        }

        protected override void SetRequest(IResourceBuilder builder)
        {
            base.SetRequest(builder);
            builder.WithPath($"{Bridge.Username}/lights/{_lightId}/state");
        }

        public override HttpContent GetContent()
        {
            var body = _lightState.ToJsonContent();
            return body;
        }
    }
}