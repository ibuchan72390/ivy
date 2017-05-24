using Ivy.Web.Core.Client;
using Ivy.Web.Json;
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
            var response = _clientHelper.Send(request);

            CheckResponseStatus(response);

            var content = response.Content.ReadAsStringAsync().Result;

            return _serializationService.Deserialize<T>(content);
        }

        public async Task<T> GetApiDataAsync<T>(HttpRequestMessage request)
        {
            var response = await _clientHelper.SendAsync(request);

            CheckResponseStatus(response);

            var content = await response.Content.ReadAsStringAsync();

            return _serializationService.Deserialize<T>(content);
        }

        #endregion

        #region Helper Methods

        private void CheckResponseStatus(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Unsuccessful response received when requesting API! " + 
                                    $"Request Uri: {response.RequestMessage.RequestUri.ToString()} / " + 
                                    $"Status Code: {response.StatusCode} / Reason: {response.ReasonPhrase}");
            }
        }

        private T DeserializeResult<T>(string result)
        {
            return _serializationService.Deserialize<T>(result);
        }

        #endregion
    }
}
