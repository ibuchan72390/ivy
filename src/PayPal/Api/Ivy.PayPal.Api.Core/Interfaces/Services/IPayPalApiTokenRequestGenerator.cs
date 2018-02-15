using System.Net.Http;

namespace Ivy.PayPal.Api.Core.Interfaces.Services
{
    public interface IPayPalApiTokenRequestGenerator
    {
        HttpRequestMessage GenerateApiTokenRequest();
    }
}
