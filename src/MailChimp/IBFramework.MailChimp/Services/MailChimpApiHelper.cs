using IBFramework.MailChimp.Core.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using IBFramework.Web.Core.Client;
using IBFramework.MailChimp.Core.Models;
using IBFramework.Web.Json;

namespace IBFramework.MailChimp.Services
{
    public class MailChimpApiHelper : IMailChimpApiHelper
    {
        #region Variables & Constants

        private readonly IHttpClientHelper _httpClientHelper;
        private readonly IJsonSerializationService _serializationService;

        private readonly IMailChimpRequestFactory _mailChimpRequestFactory;

        private readonly ILogger<IMailChimpApiHelper> _logger;

        #endregion

        #region Constructor

        public MailChimpApiHelper(
            IHttpClientHelper httpClientHelper,
            IJsonSerializationService serializationService,
            IMailChimpRequestFactory mailChimpRequestFactory,
            ILogger<IMailChimpApiHelper> logger)
        {
            _httpClientHelper = httpClientHelper;
            _serializationService = serializationService;

            _mailChimpRequestFactory = mailChimpRequestFactory;

            _logger = logger;
        }

        #endregion

        #region Public Methods

        public async Task<MailChimpMember> EditMemberAsync(MailChimpMember member)
        {
            var req = _mailChimpRequestFactory.GenerateEditMemberRequest(member);

            return await RequestToModelAsync<MailChimpMember>(req, DefaultHandleMailChimpError);
        }

        public async Task<MailChimpMember> GetMemberAsync(string email)
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
            return member.status == "404" ? null : member;
        }

        public async Task<MailChimpMember> SaveContactInfoAsync(MailChimpContactInfo contactInfo)
        {
            var req = _mailChimpRequestFactory.GenerateSubmitMemberRequest(contactInfo);

            return await RequestToModelAsync<MailChimpMember>(req, DefaultHandleMailChimpError);
        }

        public async Task<MailChimpList> GetListAsync(string listId)
        {
            var req = _mailChimpRequestFactory.GenerateGetListRequest(listId);

            return await RequestToModelAsync<MailChimpList>(req, DefaultHandleMailChimpError);
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
