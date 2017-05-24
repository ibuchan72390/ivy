using Ivy.PayPal.Core.Interfaces.Models;
using System.Threading.Tasks;

namespace Ivy.PayPal.Core.Interfaces.Services
{
    public interface IPayPalIpnValidator
    {
        Task<string> GetValidationResultAsync(string bodyStr, IPayPalIpnResponse model);
    }
}
