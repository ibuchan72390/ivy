using Ivy.IoC;
using Ivy.IoC.Core;
using Ivy.PayPal.Ipn.Core.Interfaces.Services;
using Ivy.PayPal.Ipn.Core.Interfaces.Transformer;
using Ivy.PayPal.Ipn.Core.Models;
using Ivy.PayPal.Ipn.Test.Base;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using Xunit;

namespace Ivy.PayPal.Ipn.Test.Services
{
    public class PayPalIpnServiceTests : 
        PayPalTestBase<IPayPalIpnService>
    {
        #region Variables & Constants

        private Mock<IPayPalIpnResponseTransformer> _mockTransformer;
        private Mock<IPayPalIpnValidator> _mockValidator;

        #endregion

        #region SetUp & TearDown

        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            _mockTransformer = InitializeMoq<IPayPalIpnResponseTransformer>(containerGen);
            _mockValidator = InitializeMoq<IPayPalIpnValidator>(containerGen);
        }

        #endregion

        #region Tests

        #region VerifyIpnAsync

        [Fact]
        public async void VerifyIpnAsync_Executes_As_Expected_For_Verified_Response()
        {
            const string bodyStr = "TEST";
            var req = new DefaultHttpContext();
            var model = new PayPalIpnResponse();

            _mockTransformer.Setup(x => x.Transform(req.Request)).Returns(bodyStr);
            _mockValidator.Setup(x => x.GetValidationResultAsync(bodyStr, model)).ReturnsAsync("VERIFIED");

            Assert.True(await Sut.VerifyIpnAsync(req.Request, model));

            _mockTransformer.Verify(x => x.Transform(req.Request), Times.Once);
            _mockValidator.Verify(x => x.GetValidationResultAsync(bodyStr, model), Times.Once);
        }

        [Fact]
        public async void VerifyIpnAsync_Executes_As_Expected_For_Invalid_Response()
        {
            const string bodyStr = "TEST";
            var req = new DefaultHttpContext();
            var model = new PayPalIpnResponse();

            _mockTransformer.Setup(x => x.Transform(req.Request)).Returns(bodyStr);
            _mockValidator.Setup(x => x.GetValidationResultAsync(bodyStr, model)).ReturnsAsync("INVALID");

            Assert.False(await Sut.VerifyIpnAsync(req.Request, model));

            _mockTransformer.Verify(x => x.Transform(req.Request), Times.Once);
            _mockValidator.Verify(x => x.GetValidationResultAsync(bodyStr, model), Times.Once);
        }

        [Fact]
        public async void VerifyIpnAsync_Throws_Exception_While_Processing_Results()
        {
            const string verificationResponse = "TEST";
            const string bodyStr = "TEST";
            var req = new DefaultHttpContext();
            var model = new PayPalIpnResponse();

            _mockTransformer.Setup(x => x.Transform(req.Request)).Returns(bodyStr);
            _mockValidator.Setup(x => x.GetValidationResultAsync(bodyStr, model)).ReturnsAsync(verificationResponse);

            var e = await Assert.ThrowsAsync<Exception>(() => Sut.VerifyIpnAsync(req.Request, model));

            _mockTransformer.Verify(x => x.Transform(req.Request), Times.Once);
            _mockValidator.Verify(x => x.GetValidationResultAsync(bodyStr, model), Times.Once);

            var err = "PayPalControler.ProcessVerificationResponse " +
                    $"received an unexpected response type from PayPal. Received: {verificationResponse}";

            Assert.Equal(err, e.Message);
        }

        #endregion

        #endregion
    }
}
