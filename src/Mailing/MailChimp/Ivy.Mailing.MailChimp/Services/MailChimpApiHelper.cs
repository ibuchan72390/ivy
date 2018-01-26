using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Ivy.Web.Core.Client;
using Ivy.Mailing.MailChimp.Core.Models;
using Ivy.Web.Core.Json;
using Ivy.Mailing.Core.Interfaces.Services;
using Ivy.Mailing.Core.Models;
using Ivy.Mailing.MailChimp.Core.Interfaces.Transformers;

namespace Ivy.Mailing.MailChimp.Services
{
    public class MailChimpApiHelper : IMailingApiHelper
    {
        #region Variables & Constants

        private readonly IHttpClientHelper _httpClientHelper;
        private readonly IJsonSerializationService _serializationService;

        private readonly IMailingRequestFactory _mailChimpRequestFactory;

        private readonly IMailChimpContactTransformer _contactTransformer;

        private readonly ILogger<IMailingApiHelper> _logger;

        #endregion

        #region Constructor

        public MailChimpApiHelper(
            IHttpClientHelper httpClientHelper,
            IJsonSerializationService serializationService,
            IMailingRequestFactory mailChimpRequestFactory,
            IMailChimpContactTransformer contactTransformer,
            ILogger<IMailingApiHelper> logger)
        {
            _httpClientHelper = httpClientHelper;
            _serializationService = serializationService;

            _mailChimpRequestFactory = mailChimpRequestFactory;

            _contactTransformer = contactTransformer;

            _logger = logger;
        }

        #endregion

        #region Public Methods

        public async Task<MailingMember> EditMemberAsync(MailingMember member)
        {
            var req = _mailChimpRequestFactory.GenerateEditMemberRequest(member);

            var result = await RequestToModelAsync<MailChimpMember>(req, DefaultHandleMailChimpError);

            return _contactTransformer.Transform(result);
        }

        public async Task<MailingMember> GetMemberAsync(string email)
        {
            var req = _mailChimpRequestFactory.GenerateGetMemberRequest(email);

            Action<HttpResponseMessage, string> customResponseHandler = (response, body) =>
            {
                // Ridiculous thing where this service returns 404 if a user isn't found
                // why they can't return logical JSON, I have no idea...
                if (response.StatusCode != System.Net.HttpStatusCode.NotFound)
                {
                    DefaultHandleMailChimpError(response, body);
                }
            };

            var member = await RequestToModelAsync<MailChimpMember>(req, customResponseHandler);

            // Ridiculousness, it still casts due to the 404 status, not a valid status for
            if (member.status == "404")
            {
                return null;
            }

            return _contactTransformer.Transform(member);
        }

        public async Task<MailingMember> AddMemberAsync(MailingMember contactInfo)
        {
            var req = _mailChimpRequestFactory.GenerateAddMemberRequest(contactInfo);

            var result = await RequestToModelAsync<MailChimpMember>(req, DefaultHandleMailChimpError);

            return _contactTransformer.Transform(result);
        }

        #endregion

        #region Helper Methods

        private async Task<T> RequestToModelAsync<T>(HttpRequestMessage req, Action<HttpResponseMessage, string> handleResponse)
        {
            var response = await _httpClientHelper.SendAsync(req);

            var responseBody = await response.Content.ReadAsStringAsync();

            handleResponse(response, responseBody);

            return _serializationService.Deserialize<T>(responseBody);
        }

        private void DefaultHandleMailChimpError(HttpResponseMessage response, string responseBody)
        {
            if (!response.IsSuccessStatusCode)
            {
                var errObj = _serializationService.Deserialize<MailChimpError>(responseBody);

                var errString = $"Failed to complete API request! Request Url: {response.RequestMessage.RequestUri} / " +
                    $"Error Object Detail: {errObj.detail} / Error Object Type: {errObj.type} / " +
                    $"Response Status: {response.StatusCode} / Response Body: {responseBody}";

                _logger.LogError(errString);
                throw new Exception(errString);
            }
        }

        #endregion
    }
}
