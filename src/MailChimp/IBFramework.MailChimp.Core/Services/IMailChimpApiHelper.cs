using IBFramework.MailChimp.Core.Models;
using System.Threading.Tasks;

namespace IBFramework.MailChimp.Core.Services
{
    public interface IMailChimpApiHelper
    {
        Task<MailChimpMember> EditMemberAsync(MailChimpMember member);

        Task<MailChimpMember> SaveContactInfoAsync(MailChimpContactInfo contactInfo);

        Task<MailChimpMember> GetMemberAsync(string email);

        Task<MailChimpList> GetListAsync(string listId);
    }
}
