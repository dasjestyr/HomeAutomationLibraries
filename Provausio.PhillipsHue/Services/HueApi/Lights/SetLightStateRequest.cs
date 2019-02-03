using System.Net.Http;
using Provausio.Core.WebClient;
using Provausio.PhillipsHue.Bridge;
using Provausio.PhillipsHue.Ext;
using Provausio.PhillipsHue.Lights;

namespace Provausio.PhillipsHue.Services.HueApi.Lights
{
    public class SetLightStateRequest : PhilipsHueRequest
    {
        private readonly LightStateAdjustment _lightState;
        private readonly int _lightId;

        public SetLightStateRequest(int lightId, HueBridge bridge, LightStateAdjustment state) 
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
            var body = _lightState.ToJsonContent(true);
            return body;
        }
    }
}