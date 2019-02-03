using System.Net.Http;
using Provausio.Core.WebClient;
using Provausio.PhillipsHue.Bridge;
using Provausio.PhillipsHue.Ext;

namespace Provausio.PhillipsHue.Services.HueApi.Lights
{
    public class SetLightNameRequest : PhilipsHueRequest
    {
        private readonly int _lightId;
        private readonly string _name;

        public SetLightNameRequest(int lightId, string name, HueBridge bridge) 
            : base(HttpMethod.Put, bridge)
        {
            _lightId = lightId;
            _name = name;
        }

        protected override void SetRequest(IResourceBuilder builder)
        {
            base.SetRequest(builder);
            builder.WithPath($"{Bridge.Username}/lights/{_lightId}");
        }

        public override HttpContent GetContent()
        {
            var body = new {name = _name};
            return body.ToJsonContent();
        }
    }
}