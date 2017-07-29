using Ivy.IoC;
using Ivy.IoC.Core;
using Ivy.PayPal.Core.Interfaces.Services;
using Ivy.PayPal.Core.Interfaces.Transformer;
using Ivy.PayPal.Core.Models;
using Ivy.PayPal.Test.Base;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using Xunit;

namespace Ivy.PayPal.Test.Services
{
    public class PayPalIpnServiceTests : PayPalTestBase
    {
        #region Variables & Constants

        private readonly IPayPalIpnService _sut;

        private readonly Mock<IPayPalIpnResponseTransformer> _mockTransformer;
        private readonly Mock<IPayPalIpnValidator> _mockValidator;

        #endregion

        #region SetUp & TearDown

        public PayPalIpnServiceTests()
        {
            var containerGen = ServiceLocator.Instance.Resolve<IContainerGenerator>();

            base.ConfigureContainer(containerGen);

            _mockTransformer = new Mock<IPayPalIpnResponseTransformer>();
            containerGen.RegisterInstance<IPayPalIpnResponseTransformer>(_mockTransformer.Object);

            _mockValidator = new Mock<IPayPalIpnValidator>();
            containerGen.RegisterInstance<IPayPalIpnValidator>(_mockValidator.Object);

            _sut = containerGen.GenerateContainer().Resolve<IPayPalIpnService>();
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

            Assert.True(await _sut.VerifyIpnAsync(req.Request, model));

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

            Assert.False(await _sut.VerifyIpnAsync(req.Request, model));

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

            var e = await Assert.ThrowsAsync<Exception>(() => _sut.VerifyIpnAsync(req.Request, model));

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
