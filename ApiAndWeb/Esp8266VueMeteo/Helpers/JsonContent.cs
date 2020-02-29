using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Esp8266VueMeteo.Helpers
{
    public class JsonContent : StringContent
    {
        public JsonContent(object obj) :
            base(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
        { }

        public JsonContent(string obj) : base(obj, Encoding.UTF8, "application/json") { }
    }
}
