using System.Net.Http;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;
using IBFramework.Web.Core.Client;

namespace IBFramework.Web.Client
{
    public class HttpClientHelper : IHttpClientHelper
    {
        #region Variables & Constants

        private readonly ILogger<IHttpClientHelper> _logger;

        #endregion

        #region Constructor

        public HttpClientHelper(
            ILogger<IHttpClientHelper> logger)
        {
            _logger = logger;
        }

        #endregion

        #region Public Methods

        /*
         * This method can't be mocked, we need to do this in order to test it appropriately
         */
        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            /*
             * using client.SendAsync seems to be broken in AWS Lambda
             * I'm experiencing sucess with individualized methods
             * I'll just try to use those for now I guess???
             */

            using (var client = new HttpClient())
            {
                try
                {
                    _logger.LogInformation($"Sending request to {request.RequestUri}");

                    var response = await client.SendAsync(request);
                    _logger.LogInformation("Completing request");
                    return response;
                }
                catch (Exception e)
                {
                    _logger.LogError($"Request err: {e.Message}");
                    throw;
                }
            }
        }

        public HttpResponseMessage Send(HttpRequestMessage request)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    _logger.LogInformation($"Sending request to {request.RequestUri}");

                    var response = client.SendAsync(request).Result;
                    _logger.LogInformation("Completing request");
                    return response;
                }
                catch (Exception e)
                {
                    _logger.LogError($"Request err: {e.Message}");
                    throw;
                }
            }
        }

        #endregion
    }
}
