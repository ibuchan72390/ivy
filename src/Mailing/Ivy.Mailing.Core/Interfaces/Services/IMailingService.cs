using Ivy.Mailing.Core.Models;
using Ivy.Validation.Core.Interfaces;
using System.Threading.Tasks;

namespace Ivy.Mailing.Core.Interfaces.Services
{
    public interface IMailingService
    {
        Task<IValidationResult> ProcessContactInfoAsync(MailingMember contactInfo);
    }
}
