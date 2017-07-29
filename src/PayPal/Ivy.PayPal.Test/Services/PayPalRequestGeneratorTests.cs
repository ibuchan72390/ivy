using Ivy.IoC;
using Ivy.PayPal.Core.Interfaces.Services;
using Ivy.PayPal.Test.Base;
using Ivy.Utility.Core;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ivy.PayPal.Test.Services
{
    public class PayPalRequestGeneratorTests : PayPalTestBase
    {
        #region Variables & Constants

        private readonly IPayPalRequestGenerator _sut;

        private readonly string dataStr;
        private readonly byte[] dataStrArr;

        #endregion

        #region SetUp & TearDown

        public PayPalRequestGeneratorTests()
        {
            var randHelper = ServiceLocator.Instance.Resolve<IRandomizationHelper>();

            dataStr = randHelper.RandomString(1000);
            dataStrArr = Encoding.UTF8.GetBytes(dataStr);

            _sut = ServiceLocator.Instance.Resolve<IPayPalRequestGenerator>();
        }

        #endregion

        #region Tests

        [Fact]
        public async Task GenerateValidationRequest_Works_As_Expected_For_Sandbox()
        {
            var req = _sut.GenerateValidationRequest(dataStr, true);

            await AssertRequestAsync(req, true);
        }

        [Fact]
        public async Task GenerateValidationRequest_Works_As_Expected_For_Non_Sandbox()
        {
            var req = _sut.GenerateValidationRequest(dataStr, false);

            await AssertRequestAsync(req, false);
        }

        #region

        #endregion

        #endregion

        #region Helper Methods

        private async Task AssertRequestAsync(HttpRequestMessage req, bool isSandbox)
        {
            string paypalUrl = isSandbox ?
                "https://ipnpb.sandbox.paypal.com/cgi-bin/webscr" :
                "https://ipnpb.paypal.com/cgi-bin/webscr";

            Assert.Equal(paypalUrl, req.RequestUri.ToString());
            Assert.Equal(HttpMethod.Post, req.Method);

            var reqContent = await req.Content.ReadAsByteArrayAsync();

            Assert.Equal(reqContent.Length, dataStrArr.Length);

            for (var i = 0; i < reqContent.Length; i++)
            {
                Assert.Equal(reqContent[i], dataStrArr[i]);
            }
        }

        #endregion
    }
}
