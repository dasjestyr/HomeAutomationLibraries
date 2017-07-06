using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace HomeAutomation.PhilipsHue.Ext
{
    public static class ObjectEx
    {
        public static string ToJson(this object target, bool ignoreNull = false)
        {
            var settings = ignoreNull
                ? new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore}
                : new JsonSerializerSettings {NullValueHandling = NullValueHandling.Include};

            return JsonConvert.SerializeObject(target, Formatting.None, settings);
        }

        public static HttpContent ToJsonContent(this object target, bool ignoreNull = false)
        {
            var asJson = target.ToJson(ignoreNull);
            var content = new StringContent(asJson, Encoding.UTF8, "application/json");
            return content;
        }
    }
}
