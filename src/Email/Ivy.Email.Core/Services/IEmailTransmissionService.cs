using Ivy.Email.Core.Domain;
using System.Threading.Tasks;

namespace Ivy.Email.Core.Services
{
    public interface IEmailTransmissionService
    {
        Task SendEmailAsync(SendEmailModel model);
    }
}
