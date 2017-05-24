using System.Net.Http;

namespace Ivy.PayPal.Core.Interfaces.Services
{
    public interface IPayPalRequestGenerator
    {
        HttpRequestMessage GenerateValidationRequest(bool useSandbox);
    }
}
