using Ivy.Web.Json;
using Newtonsoft.Json;

/*
 * Simply a wrapper to allow us to replace the Json Serialization library whenever possible
 * Probably won't ever be necessary since Newtonsoft.Json is pretty much king, but whatever.
 * 
 */

namespace Ivy.Web.Core.Json
{
    public class JsonSerializationService : IJsonSerializationService
    {
        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        /*
         * Would this really be that useful???
         */
        //public async Task<T> DeserializeContentAsync<T>(HttpResponseMessage response)
        //{
        //    var content = await response.Content.ReadAsStringAsync();

        //    return Deserialize<T>(content);
        //}
    }
}
