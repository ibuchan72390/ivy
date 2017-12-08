using Ivy.Web.Core.Client;
using Ivy.Web.Core.Json;
using Microsoft.Extensions.Logging;
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

        private readonly ILogger<IApiHelper> _logger;

        #endregion

        #region Constructor

        public ApiHelper(
            IHttpClientHelper clientHelper,
            IJsonSerializationService serializationService,
            ILogger<IApiHelper> logger)
        {
            _clientHelper = clientHelper;
            _serializationService = serializationService;

            _logger = logger;
        }

        #endregion

        #region Public Methods

        public T GetApiData<T>(HttpRequestMessage request)
        {
            var response = SendApiData(request);

            _logger.LogInformation("GetApiData - Request Completed");

            var content = response.Content.ReadAsStringAsync().Result;

            _logger.LogInformation("GetApiData - Content Read");

            var result = _serializationService.Deserialize<T>(content);

            _logger.LogInformation("GetApiData - Content Deserialized");

            return result;
        }

        public async Task<T> GetApiDataAsync<T>(HttpRequestMessage request)
        {
            var response = await SendApiDataAsync(request);

            _logger.LogInformation("GetApiDataAsync - Request Completed");

            var content = await response.Content.ReadAsStringAsync();

            _logger.LogInformation("GetApiDataAsync - Content Read");

            var result = _serializationService.Deserialize<T>(content);

            _logger.LogInformation("GetApiDataAsync - Content Deserialized");

            return result;
        }

        public HttpResponseMessage SendApiData(HttpRequestMessage request)
        {
            var response = _clientHelper.Send(request);

            _logger.LogInformation("SendApiData - Request Completed");

            CheckResponseStatus(response);

            _logger.LogInformation("SendApiData - Content Read");

            return response;
        }

        public async Task<HttpResponseMessage> SendApiDataAsync(HttpRequestMessage request)
        {
            var response = await _clientHelper.SendAsync(request);

            _logger.LogInformation("SendApiDataAsync - Request Completed");

            CheckResponseStatus(response);

            _logger.LogInformation("SendApiDataAsync - Content Read");

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

        #endregion
    }
}
