using System.Net.Http;
using System.Threading.Tasks;

namespace Ivy.Web.Core.Client
{
    public interface IApiHelper
    {
        Task<T> GetApiDataAsync<T>(HttpRequestMessage request);

        T GetApiData<T>(HttpRequestMessage request);

        //Task<HttpResponseMessage> SendApiDataAsync(HttpRequestMessage request);

        //HttpResponseMessage SendApiData(HttpRequestMessage request);
    }
}
