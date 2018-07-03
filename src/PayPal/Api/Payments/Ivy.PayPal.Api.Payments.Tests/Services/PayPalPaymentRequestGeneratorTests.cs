using Ivy.IoC;
using Ivy.IoC.Core;
using Ivy.PayPal.Api.Core.Interfaces.Services;
using Ivy.PayPal.Api.Core.Models;
using Ivy.PayPal.Api.Payments.Core.Interfaces.Services;
using Ivy.PayPal.Api.Payments.Tests.Base;
using Moq;
using System.Net.Http;
using Xunit;

namespace Ivy.PayPal.Api.Payments.Tests.Services
{
    public class PayPalPaymentRequestGeneratorTests : 
        PayPalPaymentsTestBase<IPayPalPaymentsRequestGenerator>
    {
        #region Variables & Constants

        private Mock<IPayPalApiTokenGenerator> _mockTokenGen;
        private Mock<IPayPalUrlGenerator> _mockUrlGen;

        private const string payPalUrl = "https://api.sandbox.paypal.com/";

        #endregion

        #region SetUp & TearDown

        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            _mockTokenGen = InitializeMoq<IPayPalApiTokenGenerator>(containerGen);

            _mockUrlGen = InitializeMoq<IPayPalUrlGenerator>(containerGen);
            _mockUrlGen.Setup(x => x.GetPayPalUrl()).Returns(payPalUrl);
        }

        #endregion

        #region Tests

        [Fact]
        public async void GenerateShowPaymentRequestAsync_Generates_As_Expected()
        {
            // Arrange
            const string paymentId = "payment";

            var token = new PayPalTokenResponse { access_token = "token" };
            _mockTokenGen.Setup(x => x.GetApiTokenAsync()).ReturnsAsync(token);


            // Act
            var result = await Sut.GenerateShowPaymentRequestAsync(paymentId);


            // Assert
            _mockTokenGen.Verify(x => x.GetApiTokenAsync(), Times.Once);

            var expectedUrl = "https://api.sandbox.paypal.com/v1/payments/payment/" + paymentId;
            Assert.Equal(expectedUrl, result.RequestUri.ToString());
            Assert.Equal(HttpMethod.Get, result.Method);

            Assert.Equal("Bearer", result.Headers.Authorization.Scheme);
            Assert.Equal(token.access_token, result.Headers.Authorization.Parameter);
        }

        #endregion
    }
}
