using System.Net.Http;
using DAS.Infrastructure.WebClient;
using HomeAutomation.PhilipsHue.Bridge;
using HomeAutomation.PhilipsHue.Ext;

namespace HomeAutomation.PhilipsHue.Services.HueApi.Lights
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