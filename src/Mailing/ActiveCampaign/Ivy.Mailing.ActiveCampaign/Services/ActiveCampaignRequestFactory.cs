using System;
using System.Net.Http;
using Ivy.Mailing.ActiveCampaign.Core.Interfaces.Providers;
using Ivy.Mailing.Core.Interfaces.Services;
using Ivy.Mailing.Core.Models;
using Newtonsoft.Json.Linq;

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
            url += $"&filter[email]={email}";
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
            var baseUrl = $"{_configProvider.ApiUrl}/admin/api.php?api_action={action}";

            if (isGet)
            {
                baseUrl += $"&api_key={_configProvider.ApiKey}&api_output={output}";
            }

            return baseUrl;
        }

        private HttpRequestMessage InternetalGenerateEditMember(MailingMember member, bool isEdit)
        {
            var req = new HttpRequestMessage();
            req.Method = HttpMethod.Post;

            string url = GenerateBaseUrl(isEdit ? "contact_edit" : "contact_add", false);
            req.RequestUri = new Uri(url);

            var postObj = new JObject();

            // Missing from URL
            postObj.Add("api_key", _configProvider.ApiKey);
            postObj.Add("api_output", output);

            // Contact Parameters
            if (isEdit)
            {
                postObj.Add("id", member.Id);
            }

            postObj.Add("email", member.Email);
            postObj.Add("first_name", member.FirstName);
            postObj.Add("last_name", member.LastName);
            postObj.Add("phone", member.Phone);
            postObj.Add($"p[{_configProvider.ListId}]", _configProvider.ListId);

            foreach (var item in member.ExtraData)
            {
                postObj.Add($"field[{item.Key}]", item.Value);
            }

            req.Content = new StringContent(postObj.ToString(), System.Text.Encoding.Default,
                "application/x-www-form-urlencoded");

            return req;
        }

        #endregion
    }
}
