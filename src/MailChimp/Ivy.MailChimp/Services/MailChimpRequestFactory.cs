using System;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using Ivy.MailChimp.Core.Services;
using Ivy.MailChimp.Core.Providers;
using Ivy.MailChimp.Core.Models;
using Ivy.Web.Json;
using Ivy.Utility.Extensions;

namespace Ivy.MailChimp.Services
{
    public class MailChimpRequestFactory : IMailChimpRequestFactory
    {
        #region Variables & Constants

        private readonly IMailChimpConfigurationProvider _configProvider;
        private readonly IJsonSerializationService _serializationService;

        private readonly string baseApiUrl;

        #endregion

        #region Constructor

        public MailChimpRequestFactory(
            IMailChimpConfigurationProvider configProvider,
            IJsonSerializationService serializationService)
        {
            _configProvider = configProvider;
            _serializationService = serializationService;

            baseApiUrl = $"https://{_configProvider.DataCenter}.api.mailchimp.com/3.0/";
        }

        #endregion

        #region Public Methods

        public HttpRequestMessage GenerateGetMemberRequest(string email)
        {
            return GetBaseRequest(HttpMethod.Get, $"lists/{_configProvider.ListId}/members/{email.ToMD5Hash()}?fields=id,email_address,status");
        }

        public HttpRequestMessage GenerateSubmitMemberRequest(MailChimpContactInfo member)
        {
            var req = GetBaseRequest(HttpMethod.Post, $"lists/{_configProvider.ListId}/members");

            req.Content = GetStringContent(member);

            return req;
        }

        public HttpRequestMessage GenerateEditMemberRequest(MailChimpMember member)
        {
            var req = GetBaseRequest(HttpMethod.Put, $"lists/{_configProvider.ListId}/members/{member.email_address.ToMD5Hash()}");

            req.Content = GetStringContent(member);

            return req;
        }

        public HttpRequestMessage GenerateGetListRequest(string listId)
        {
            return GetBaseRequest(HttpMethod.Get, $"lists/{_configProvider.ListId}");
        }

        #endregion

        #region Helper Methods

        private HttpRequestMessage GetBaseRequest(HttpMethod method, string refPath)
        {
            // Setup the request
            var req = new HttpRequestMessage(method, new Uri(GetUrl(refPath)));

            // Setup the authentication header
            var byteArray = Encoding.ASCII.GetBytes($"anything:{_configProvider.ApiKey}");
            var header = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            req.Headers.Authorization = header;

            return req;
        }

        private string GetUrl(string refPath)
        {
            return baseApiUrl + refPath;
        }

        private HttpContent GetStringContent<T>(T obj)
        {
            var stringContent = _serializationService.Serialize(obj);

            return new StringContent(stringContent);
        }

        #endregion
    }
}
