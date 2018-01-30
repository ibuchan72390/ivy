using System;
using System.Collections.Generic;
using System.Net.Http;
using Ivy.Mailing.ActiveCampaign.Core.Interfaces.Providers;
using Ivy.Mailing.Core.Interfaces.Services;
using Ivy.Mailing.Core.Models;

namespace Ivy.Mailing.ActiveCampaign.Services
{
    public class ActiveCampaignRequestFactory : IMailingRequestFactory
    {
        #region Variables & Constants

        private readonly IActiveCampaignConfigurationProvider _configProvider;

        const string output = "json";

        #endregion

        #region Constructor

        public ActiveCampaignRequestFactory(
            IActiveCampaignConfigurationProvider configProvider)
        {
            _configProvider = configProvider;
        }

        #endregion

        #region Public Methods

        public HttpRequestMessage GenerateGetMemberRequest(string email)
        {
            var req = new HttpRequestMessage();
            req.Method = HttpMethod.Get;

            string url = GenerateBaseUrl("contact_list", true);
            url += $"&filters[email]={email}";
            req.RequestUri = new Uri(url);

            return req;
        }

        public HttpRequestMessage GenerateAddMemberRequest(MailingMember member)
        {
            return InternetalGenerateEditMember(member, false);
        }

        public HttpRequestMessage GenerateEditMemberRequest(MailingMember member)
        {
            return InternetalGenerateEditMember(member, true);
        }

        #endregion

        #region Helper Methods

        private string GenerateBaseUrl(string action, bool isGet)
        {
            return $"{_configProvider.ApiUrl}/admin/api.php?api_action={action}&api_key={_configProvider.ApiKey}&api_output={output}";
        }

        private HttpRequestMessage InternetalGenerateEditMember(MailingMember member, bool isEdit)
        {
            var req = new HttpRequestMessage();
            req.Method = HttpMethod.Post;

            string url = GenerateBaseUrl(isEdit ? "contact_edit" : "contact_add", false);
            req.RequestUri = new Uri(url);

            var postDict = new Dictionary<string, string>();

            // Contact Parameters
            if (isEdit)
            {
                postDict.Add("id", member.Id);
            }

            postDict.Add("email", member.Email);
            postDict.Add("first_name", member.FirstName);
            postDict.Add("last_name", member.LastName);
            postDict.Add("phone", member.Phone);
            postDict.Add($"p[{_configProvider.ListId}]", _configProvider.ListId);

            foreach (var item in member.ExtraData)
            {
                postDict.Add($"field[{item.Key}]", item.Value);
            }

            // https://www.activecampaign.com/api/example.php?call=contact_add
            // We must use content of type application/x-www-form-urlencoded
            // Do NOT use JSON and StringContent, it does NOT convert correctly
            // FormUrlEncodedContent is the proper way to perform this assignment.
            req.Content = new FormUrlEncodedContent(postDict);

            return req;
        }

        #endregion
    }
}
