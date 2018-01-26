using System;
using System.Linq;
using System.Threading.Tasks;
using Ivy.Mailing.ActiveCampaign.Core.Interfaces.Services;
using Ivy.Mailing.ActiveCampaign.Core.Interfaces.Transformers;
using Ivy.Mailing.Core.Interfaces.Services;
using Ivy.Mailing.Core.Models;
using Ivy.Web.Core.Client;

namespace Ivy.Mailing.ActiveCampaign.Services
{
    public class ActiveCampaignApiHelper : IMailingApiHelper
    {
        #region Variables & Constnats

        private readonly IApiHelper _apiHelper;

        private readonly IMailingRequestFactory _requestFactory;

        private readonly IActiveCampaignContactListDeserializer _contactListDeserializer;

        private readonly IActiveCampaignContactTransformer _contactTransformer;

        #endregion

        #region Constructor

        public ActiveCampaignApiHelper(
            IApiHelper apiHelper,
            IMailingRequestFactory requestFactory,
            IActiveCampaignContactListDeserializer contactListDeserializer,
            IActiveCampaignContactTransformer contactTransformer)
        {
            _apiHelper = apiHelper;
            _requestFactory = requestFactory;
            _contactListDeserializer = contactListDeserializer;
            _contactTransformer = contactTransformer;
        }

        #endregion

        #region Public Methods

        public async Task<MailingMember> GetMemberAsync(string email)
        {
            var req = _requestFactory.GenerateGetMemberRequest(email);

            var result = await _apiHelper.SendApiDataAsync(req);

            var resultJson = await result.Content.ReadAsStringAsync();

            var resultObj = _contactListDeserializer.Deserialize(resultJson);

            if (resultObj.Count > 1)
            {
                throw new Exception($"Found multiple ActiveCampaign users with the same email! Email: {email}");
            }
            else if (resultObj.Count == 0)
            {
                return null;
            }

            var resultAcUser = resultObj.First().Value;

            return _contactTransformer.Transform(resultAcUser);
        }

        public async Task<MailingMember> AddMemberAsync(MailingMember member)
        {
            var req = _requestFactory.GenerateAddMemberRequest(member);

            await _apiHelper.SendApiDataAsync(req);

            return member;
        }

        public async Task<MailingMember> EditMemberAsync(MailingMember member)
        {
            var req = _requestFactory.GenerateEditMemberRequest(member);

            await _apiHelper.SendApiDataAsync(req);

            return member;
        }

        #endregion
    }
}
