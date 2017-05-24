using Ivy.PayPal.Core.Interfaces.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Ivy.PayPal.Core.Interfaces.Services
{
    public interface IPayPalIpnService
    {
        Task<bool> VerifyIpnAsync(HttpRequest request, IPayPalIpnResponse model);
    }
}
