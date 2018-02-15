using Ivy.PayPal.Api.Payments.Core.Models.Response;
using System.Threading.Tasks;

namespace Ivy.PayPal.Api.Payments.Core.Interfaces.Services
{
    public interface IPayPalPaymentsService
    {
        Task<PayPalPaymentShowResponse> GetPaymentDetailsAsync(string paymentId);
    }
}
