using Ivy.MailChimp.Core.Models;
using Ivy.Validation.Core;
using System.Threading.Tasks;

namespace Ivy.MailChimp.Core.Services
{
    public interface IMailChimpService
    {
        Task<IValidationResult> ProcessContactInfoAsync(MailChimpContactInfo contactInfo);
    }
}
