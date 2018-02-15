using System.Net.Http;
using System.Threading.Tasks;

namespace Ivy.PayPal.Api.Payments.Core.Interfaces.Services
{
    public interface IPayPalPaymentsRequestGenerator
    {
        Task<HttpRequestMessage> GenerateShowPaymentRequestAsync(string paymentId);
    }
}
