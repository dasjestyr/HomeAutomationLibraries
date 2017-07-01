using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace HomeAutomation.PhilipsHue.Ext
{
    public static class ObjectEx
    {
        public static string ToJson(this object target)
        {
            var asJson = JsonConvert.SerializeObject(target);
            return asJson;
        }

        public static HttpContent ToJsonContent(this object target)
        {
            var asJson = target.ToJson();
            var content = new StringContent(asJson, Encoding.UTF8, "application/json");
            return content;
        }
    }
}
