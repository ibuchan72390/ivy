using Microsoft.AspNetCore.Http;

namespace Ivy.PayPal.Core.Interfaces.Transformer
{
    /*
     * Although it doesn't follow the usual pattern, this
     * is technically just a transformer that converts the
     * request into the original request content string.
     */

    public interface IPayPalIpnResponseTransformer
    {
        string Transform(HttpRequest request);
    }
}
