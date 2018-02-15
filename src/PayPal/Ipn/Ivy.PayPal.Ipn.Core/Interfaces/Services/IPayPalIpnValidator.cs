using Ivy.PayPal.Ipn.Core.Interfaces.Models;
using System.Threading.Tasks;

namespace Ivy.PayPal.Ipn.Core.Interfaces.Services
{
    public interface IPayPalIpnValidator
    {
        Task<string> GetValidationResultAsync(string bodyStr, IPayPalIpnResponse model);
    }
}
