using System.Net.Http;

namespace Ivy.PayPal.Ipn.Core.Interfaces.Services
{
    public interface IPayPalRequestGenerator
    {
        HttpRequestMessage GenerateValidationRequest(string dataStr, bool useSandbox);
    }
}
