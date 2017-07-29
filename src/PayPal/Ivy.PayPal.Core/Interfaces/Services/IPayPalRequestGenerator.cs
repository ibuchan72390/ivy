using System.Net.Http;

namespace Ivy.PayPal.Core.Interfaces.Services
{
    public interface IPayPalRequestGenerator
    {
        HttpRequestMessage GenerateValidationRequest(string dataStr, bool useSandbox);
    }
}
