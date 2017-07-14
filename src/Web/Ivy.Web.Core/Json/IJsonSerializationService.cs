namespace Ivy.Web.Core.Json
{
    public interface IJsonSerializationService
    {
        string Serialize(object obj);

        T Deserialize<T>(string json);
    }
}
