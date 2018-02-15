using Ivy.PayPal.Api.Core.Models;
using System.Threading.Tasks;

namespace Ivy.PayPal.Api.Core.Interfaces.Services
{
    public interface IPayPalApiTokenGenerator
    {
        Task<PayPalTokenResponse> GetApiTokenAsync();
    }
}
