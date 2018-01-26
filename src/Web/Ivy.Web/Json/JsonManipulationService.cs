using Ivy.Web.Core.Json;
using Newtonsoft.Json.Linq;

namespace Ivy.Web.Json
{
    public class JsonManipulationService : IJsonManipulationService
    {
        #region Variables & Constants

        private readonly IJsonSerializationService _jsonSerializer;

        #endregion

        #region Constructor

        public JsonManipulationService(
            IJsonSerializationService jsonSerializer)
        {
            _jsonSerializer = jsonSerializer;
        }

        #endregion

        #region Public Methods

        public T ExtractJsonAttribute<T>(string json, string attributeName)
        {
            var jsonObj = _jsonSerializer.Deserialize<JObject>(json);
            return jsonObj.Property(attributeName).Value.ToObject<T>();
        }

        public string RemoveJsonAttribute(string json, string attributeName)
        {
            var jsonObj = _jsonSerializer.Deserialize<JObject>(json);
            jsonObj.Property(attributeName).Remove();
            return jsonObj.ToString();
        }

        #endregion
    }
}
