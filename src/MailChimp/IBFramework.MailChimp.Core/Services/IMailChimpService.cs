using IBFramework.MailChimp.Core.Models;
using IBFramework.Validation.Core;
using System.Threading.Tasks;

namespace IBFramework.MailChimp.Core.Services
{
    public interface IMailChimpService
    {
        Task<IValidationResult> ProcessContactInfoAsync(MailChimpContactInfo contactInfo);
    }
}
