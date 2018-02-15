using Ivy.PayPal.Ipn.Core.Interfaces.Services;
using System;
using System.Net.Http;
using System.Text;

namespace Ivy.PayPal.Ipn.Services
{
    public class PayPalRequestGenerator : IPayPalRequestGenerator
    {
        public HttpRequestMessage GenerateValidationRequest(string dataStr, bool useSandbox)
        {
            // ORIGINAL WORKING
            //var request = WebRequest.Create(new Uri(paypalUrl)) as HttpWebRequest;
            //request.Method = "POST";

            //byte[] data = Encoding.UTF8.GetBytes(bodyStr);
            //using (var requestStream = await Task<Stream>.Factory.FromAsync(request.BeginGetRequestStream, request.EndGetRequestStream, request))
            //{
            //    await requestStream.WriteAsync(data, 0, data.Length);
            //}

            //WebResponse responseObject = await Task<WebResponse>.Factory.FromAsync(request.BeginGetResponse, request.EndGetResponse, request);
            //var responseStream = responseObject.GetResponseStream();
            //var sr = new StreamReader(responseStream);
            //return await sr.ReadToEndAsync();


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

            request.Content = new ByteArrayContent(Encoding.UTF8.GetBytes(dataStr));

            return request;
        }
    }
}
