using System.Collections.Generic;
using Xunit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Ivy.PayPal.Ipn.Test.Base;
using Ivy.PayPal.Ipn.Core.Interfaces.Transformer;
using Ivy.IoC;

namespace Ivy.PayPal.Ipn.Test.Transformers
{
    public class PayPalIpnResponseTransformerTests : 
        PayPalTestBase<IPayPalIpnResponseTransformer>
    {
        #region Tests

        /*
         * This sample has been taken expressly from the PayPal developer documentation 
         * https://developer.paypal.com/docs/classic/ipn/integration-guide/IPNIntro/#id08CKFJ00JYK
         * 
         * When this request passes successfully through the process, you will end up receiving an 
         * "INVALID" response from PayPal during the validation.  This is expected since this is a
         * default test message.
         * 
         * If you wish to sample this in a live local environment, download Postman,
         * start your Web API, create a post request to http://localhost:5000/api/PayPal/VerifyPurchase,
         * copy and paste the below text into the request body and set the type to text/plain
         */

        [Fact]
        public void Transform_Works_As_Expected_On_Request()
        {
            const string expectedBodyText = "cmd=_notify-validate&mc_gross=19.95&protection_eligibility=Eligible&address_status=confirmed&" +
                "payer_id=LPLWNMTBWMFAY&tax=0.00&address_street=1+Main+St&payment_date=20%3A12%3A59+Jan+13%2C+2009+PST&" +
                "payment_status=Completed&charset=windows-1252&address_zip=95131&first_name=Test&mc_fee=0.88&" +
                "address_country_code=US&address_name=Test+User&notify_version=2.6&custom=&payer_status=verified&" +
                "address_country=United+States&address_city=San+Jose&quantity=1&verify_sign=AtkOfCXbDm2hu0ZELryHFjY-Vb7PAUvS6nMXgysbElEn9v-1XcmSoGtf&" +
                "payer_email=gpmac_1231902590_per%40paypal.com&txn_id=61E67681CH3238416&payment_type=instant&last_name=User&" +
                "address_state=CA&receiver_email=gpmac_1231902686_biz%40paypal.com&payment_fee=0.88&receiver_id=S8XGHLYDW9T3S&" +
                "txn_type=express_checkout&item_name=&mc_currency=USD&item_number=&residence_country=US&test_ipn=1&handling_amount=0.00&" +
                "transaction_subject=&payment_gross=19.95&shipping=0.00";

            var context = new DefaultHttpContext();

            //context.Request.Body = GenerateStreamFromString(incomingBodyText);
            var formDictionary = new Dictionary<string, StringValues>
            {
                //mc_gross=19.95
                { "mc_gross", new StringValues("19.95") },

                //protection_eligibility=Eligible
                { "protection_eligibility", new StringValues("Eligible") },

                //address_status=confirmed
                { "address_status", new StringValues("confirmed") },

                //payer_id=LPLWNMTBWMFAY
                { "payer_id", new StringValues("LPLWNMTBWMFAY") },

                //tax=0.00
                { "tax", new StringValues("0.00") },

                //address_street=1+Main+St
                { "address_street", new StringValues("1 Main St") },

                //payment_date=20%3A12%3A59+Jan+13%2C+2009+PST
                { "payment_date", new StringValues("20:12:59 Jan 13, 2009 PST") },

                //payment_status=Completed
                { "payment_status", new StringValues("Completed") },

                //charset=windows-1252
                { "charset", new StringValues("windows-1252") },

                //address_zip=95131
                { "address_zip", new StringValues("95131") },

                //first_name=Test
                { "first_name", new StringValues("Test") },

                //mc_fee=0.88
                { "mc_fee", new StringValues("0.88") },

                //address_country_code=US
                { "address_country_code", new StringValues("US") },

                //address_name=Test+User
                { "address_name", new StringValues("Test User") },

                //notify_version=2.6
                { "notify_version", new StringValues("2.6") },

                //custom=
                { "custom", new StringValues("") },

                //payer_status=verified
                { "payer_status", new StringValues("verified") },

                //address_country=United+States
                { "address_country", new StringValues("United States") },

                //address_city=San+Jose
                { "address_city", new StringValues("San Jose") },

                //quantity=1
                { "quantity", new StringValues("1") },

                //verify_sign=AtkOfCXbDm2hu0ZELryHFjY-Vb7PAUvS6nMXgysbElEn9v-1XcmSoGtf
                { "verify_sign", new StringValues("AtkOfCXbDm2hu0ZELryHFjY-Vb7PAUvS6nMXgysbElEn9v-1XcmSoGtf") },

                //payer_email=gpmac_1231902590_per%40paypal.com
                { "payer_email", new StringValues("gpmac_1231902590_per@paypal.com") },

                //txn_id=61E67681CH3238416
                { "txn_id", new StringValues("61E67681CH3238416") },

                //payment_type=instant
                { "payment_type", new StringValues("instant") },

                //last_name=User
                { "last_name", new StringValues("User") },

                //address_state=CA
                { "address_state", new StringValues("CA") },

                //receiver_email=gpmac_1231902686_biz%40paypal.com
                { "receiver_email", new StringValues("gpmac_1231902686_biz@paypal.com") },

                //payment_fee=0.88
                { "payment_fee", new StringValues("0.88") },

                //receiver_id=S8XGHLYDW9T3S
                { "receiver_id", new StringValues("S8XGHLYDW9T3S") },

                //txn_type=express_checkout
                { "txn_type", new StringValues("express_checkout") },

                //item_name=
                { "item_name", new StringValues("") },

                //mc_currency=USD
                { "mc_currency", new StringValues("USD") },

                //item_number=
                { "item_number", new StringValues("") },

                //residence_country=US
                { "residence_country", new StringValues("US") },

                //test_ipn=1
                { "test_ipn", new StringValues("1") },

                //handling_amount=0.00
                { "handling_amount", new StringValues("0.00") },

                //transaction_subject=
                { "transaction_subject", new StringValues("") },

                //payment_gross=19.95
                { "payment_gross", new StringValues("19.95") },

                //shipping=0.00
                { "shipping", new StringValues("0.00") },
            };
            context.Request.Form = new FormCollection(formDictionary);

            var result = Sut.Transform(context.Request);

            Assert.Equal(expectedBodyText, result);
        }

        #endregion
    }
}
