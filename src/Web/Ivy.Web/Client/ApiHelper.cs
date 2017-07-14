using Ivy.Web.Core.Client;
using Ivy.Web.Core.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ivy.Web.Client
{
    public class ApiHelper : IApiHelper
    {
        #region Variables & Constants

        private readonly IHttpClientHelper _clientHelper;
        private readonly IJsonSerializationService _serializationService;

        #endregion

        #region Constructor

        public ApiHelper(
            IHttpClientHelper clientHelper,
            IJsonSerializationService serializationService)
        {
            _clientHelper = clientHelper;
            _serializationService = serializationService;
        }

        #endregion

        #region Public Methods

        public T GetApiData<T>(HttpRequestMessage request)
        {
            var response = SendApiData(request);

            var content = response.Content.ReadAsStringAsync().Result;

            return _serializationService.Deserialize<T>(content);
        }

        public async Task<T> GetApiDataAsync<T>(HttpRequestMessage request)
        {
            var response = await SendApiDataAsync(request);

            var content = await response.Content.ReadAsStringAsync();

            return _serializationService.Deserialize<T>(content);
        }

        public HttpResponseMessage SendApiData(HttpRequestMessage request)
        {
            var response = _clientHelper.Send(request);

            CheckResponseStatus(response);

            return response;
        }

        public async Task<HttpResponseMessage> SendApiDataAsync(HttpRequestMessage request)
        {
            var response = await _clientHelper.SendAsync(request);

            CheckResponseStatus(response);

            return response;
        }

        #endregion

        #region Helper Methods

        private void CheckResponseStatus(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Unsuccessful response received when requesting API! " + 
                                    $"Request Uri: {response.RequestMessage.RequestUri.ToString()} / " + 
                                    $"Status Code: {response.StatusCode.ToString()} / Reason: {response.ReasonPhrase} / " + 
                                    $"Message: {response.RequestMessage}");
            }
        }

        private T DeserializeResult<T>(string result)
        {
            return _serializationService.Deserialize<T>(result);
        }

        #endregion
    }
}
