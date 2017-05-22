using System.Net.Http;
using System.Threading.Tasks;

namespace IBFramework.Web.Core.Client
{
    public interface IApiHelper
    {
        Task<T> GetApiDataAsync<T>(HttpRequestMessage request);

        T GetApiData<T>(HttpRequestMessage request);
    }
}
