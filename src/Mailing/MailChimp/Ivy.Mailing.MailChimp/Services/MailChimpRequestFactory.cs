﻿using System;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using Ivy.Mailing.MailChimp.Core.Providers;
using Ivy.Web.Core.Json;
using Ivy.Utility.Core.Extensions;
using Ivy.Mailing.Core.Interfaces.Services;
using Ivy.Mailing.Core.Models;
using Ivy.Mailing.MailChimp.Core.Interfaces.Transformers;

namespace Ivy.Mailing.MailChimp.Services
{
    public class MailChimpRequestFactory : IMailingRequestFactory
    {
        #region Variables & Constants

        private readonly IMailChimpConfigurationProvider _configProvider;
        private readonly IJsonSerializationService _serializationService;
        private readonly IMailChimpContactTransformer _contactTransformer;

        private readonly string baseApiUrl;

        #endregion

        #region Constructor

        public MailChimpRequestFactory(
            IMailChimpConfigurationProvider configProvider,
            IJsonSerializationService serializationService,
            IMailChimpContactTransformer contactTransformer)
        {
            _configProvider = configProvider;
            _serializationService = serializationService;
            _contactTransformer = contactTransformer;

            baseApiUrl = $"https://{_configProvider.DataCenter}.api.mailchimp.com/3.0/";
        }

        #endregion

        #region Public Methods

        public HttpRequestMessage GenerateGetMemberRequest(string email)
        {
            return GetBaseRequest(HttpMethod.Get, $"lists/{_configProvider.ListId}/members/{email.ToMD5Hash()}?fields=id,email_address,status");
        }

        public HttpRequestMessage GenerateAddMemberRequest(MailingMember member)
        {
            var req = GetBaseRequest(HttpMethod.Post, $"lists/{_configProvider.ListId}/members");

            var mailChimpMember = _contactTransformer.Transform(member);
            req.Content = GetStringContent(mailChimpMember);

            return req;
        }

        public HttpRequestMessage GenerateEditMemberRequest(MailingMember member)
        {
            var req = GetBaseRequest(HttpMethod.Put, $"lists/{_configProvider.ListId}/members/{member.Email.ToMD5Hash()}");

            var mailChimpMember = _contactTransformer.Transform(member);
            req.Content = GetStringContent(mailChimpMember);

            return req;
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
