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
        PayPalPaymentsTestBase
    {
        #region Variables & Constants

        private readonly IPayPalPaymentsRequestGenerator _sut;

        private readonly Mock<IPayPalApiTokenGenerator> _mockTokenGen;

        #endregion

        #region SetUp & TearDown

        public PayPalPaymentRequestGeneratorTests()
        {
            var containerGen = ServiceLocator.Instance.GetService<IContainerGenerator>();

            base.ConfigureContainer(containerGen);

            _mockTokenGen = new Mock<IPayPalApiTokenGenerator>();
            containerGen.RegisterInstance<IPayPalApiTokenGenerator>(_mockTokenGen.Object);

            _sut = containerGen.GenerateContainer().GetService<IPayPalPaymentsRequestGenerator>();
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
            var result = await _sut.GenerateShowPaymentRequestAsync(paymentId);


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
