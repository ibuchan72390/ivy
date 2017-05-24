using System.Net.Http;
using System.Threading.Tasks;

namespace Ivy.Web.Core.Client
{
    /*
     * This breaks our dependency on the HttpClient
     * Allows us to mock it out for testing purposes.
     * 
     * Really, all of the Http object generation should 
     * probably be interfaced out...
     */
    public interface IHttpClientHelper
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);

        HttpResponseMessage Send(HttpRequestMessage request);
    }
}
