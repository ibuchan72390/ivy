using Ivy.PayPal.Ipn.Core.Interfaces.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Ivy.PayPal.Ipn.Core.Interfaces.Services
{
    public interface IPayPalIpnService
    {
        Task<bool> VerifyIpnAsync(HttpRequest request, IPayPalIpnResponse model);
    }
}
