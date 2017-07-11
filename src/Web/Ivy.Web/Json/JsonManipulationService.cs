using Ivy.Web.Core.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Ivy.Web.Json
{
    public class JsonManipulationService : IJsonManipulationService
    {
        public string RemoveJsonAttribute(string json, string attributeName)
        {
            var jsonObj = (JObject)JsonConvert.DeserializeObject(json);
            jsonObj.Property(attributeName).Remove();
            return jsonObj.ToString();
        }
    }
}
