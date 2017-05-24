using Ivy.PayPal.Core.Interfaces.Services;
using System;
using System.Net.Http;

namespace Ivy.PayPal.Services
{
    public class PayPalRequestGenerator : IPayPalRequestGenerator
    {
        public HttpRequestMessage GenerateValidationRequest(bool useSandbox)
        {
            /*
             * PayPal is now suggesting the ipnpb url for IPN post-back responses instead of the www subdomain
             * https://www.paypal-knowledge.com/infocenter/index?page=content&widgetview=true&id=FAQ1916&viewlocale=en_US
             * 
             * Sandbox alternate should be removed from here when we're ready to go live
             */
            string paypalUrl = useSandbox ?
                "https://ipnpb.sandbox.paypal.com/cgi-bin/webscr" :
                "https://ipnpb.paypal.com/cgi-bin/webscr";

            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.RequestUri = new Uri(paypalUrl);

            return request;
        }
    }
}
