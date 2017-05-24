namespace Ivy.Web.Json
{
    public interface IJsonSerializationService
    {
        string Serialize(object obj);

        T Deserialize<T>(string json);
    }
}
