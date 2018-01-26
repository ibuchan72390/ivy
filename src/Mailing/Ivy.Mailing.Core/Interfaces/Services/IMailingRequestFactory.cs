using Ivy.Mailing.Core.Models;
using System.Net.Http;

namespace Ivy.Mailing.Core.Interfaces.Services
{
    public interface IMailingRequestFactory
    {
        HttpRequestMessage GenerateAddMemberRequest(MailingMember member);

        HttpRequestMessage GenerateGetMemberRequest(string email);

        HttpRequestMessage GenerateEditMemberRequest(MailingMember member);
    }
}
