using Ivy.PayPal.Core.Interfaces.Transformer;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text;

namespace Ivy.PayPal.Transformers
{
    public class PayPalIpnResponseTransformer : IPayPalIpnResponseTransformer
    {
        public string Transform(HttpRequest request)
        {
            StringBuilder to_send = new StringBuilder();

            to_send.AppendFormat("{0}={1}", "cmd", "_notify-validate");

            foreach (string key in request.Form.Keys)
            {
                // Only need to url encode on the actual value to regenerate the original string
                // .NET inherently uses the URL Decode on the incoming Request message
                to_send.AppendFormat("&{0}={1}", key, WebUtility.UrlEncode(request.Form[key]));
            }

            return to_send.ToString();
        }
    }
}
