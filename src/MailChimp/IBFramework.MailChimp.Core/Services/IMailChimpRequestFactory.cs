using IBFramework.MailChimp.Core.Models;
using System.Net.Http;

namespace IBFramework.MailChimp.Core.Services
{
    public interface IMailChimpRequestFactory
    {
        HttpRequestMessage GenerateSubmitMemberRequest(MailChimpContactInfo member);

        HttpRequestMessage GenerateGetMemberRequest(string email);

        HttpRequestMessage GenerateEditMemberRequest(MailChimpMember member);

        HttpRequestMessage GenerateGetListRequest(string listId);
    }
}
