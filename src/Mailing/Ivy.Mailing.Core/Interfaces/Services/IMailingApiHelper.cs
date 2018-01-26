using Ivy.Mailing.Core.Models;
using System.Threading.Tasks;

namespace Ivy.Mailing.Core.Interfaces.Services
{
    public interface IMailingApiHelper
    {
        Task<MailingMember> EditMemberAsync(MailingMember member);

        Task<MailingMember> AddMemberAsync(MailingMember contactInfo);

        Task<MailingMember> GetMemberAsync(string email);
    }
}
