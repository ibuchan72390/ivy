using Ivy.IoC;
using Ivy.IoC.Core;
using Ivy.PayPal.Api.Payments.Core.Interfaces.Services;
using Ivy.PayPal.Api.Payments.Core.Models.Response;
using Ivy.PayPal.Api.Payments.Tests.Base;
using Ivy.Web.Core.Client;
using Moq;
using System.Net.Http;
using Xunit;

namespace Ivy.PayPal.Api.Payments.Tests.Services
{
    public class PayPalPaymentsServiceTests : 
        PayPalPaymentsTestBase<IPayPalPaymentsService>
    {
        #region Variables & Constants

        private Mock<IPayPalPaymentsRequestGenerator> _mockRequestGen;
        private Mock<IApiHelper> _mockApiHelper;

        #endregion

        #region SetUp & TearDown
        
        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            _mockRequestGen = InitializeMoq<IPayPalPaymentsRequestGenerator>(containerGen);
            _mockApiHelper = InitializeMoq<IApiHelper>(containerGen);
        }

        #endregion

        #region Tests

        [Fact]
        public async void GetPaymentDetailsAsync_Returns_As_Expected()
        {
            // Arrange
            const string paymentId = "Id";
            var req = new HttpRequestMessage();
            var expected = new PayPalPaymentShowResponse();

            _mockRequestGen.Setup(x => x.GenerateShowPaymentRequestAsync(paymentId)).
                ReturnsAsync(req);

            _mockApiHelper.Setup(x => x.GetApiDataAsync<PayPalPaymentShowResponse>(req)).
                ReturnsAsync(expected);

            // Act
            var result = await Sut.GetPaymentDetailsAsync(paymentId);

            // Assert
            _mockRequestGen.Verify(x => x.GenerateShowPaymentRequestAsync(paymentId),
                Times.Once);

            _mockApiHelper.Verify(x => x.GetApiDataAsync<PayPalPaymentShowResponse>(req),
                Times.Once);

            Assert.Same(expected, result);
        }

        #endregion
    }
}
