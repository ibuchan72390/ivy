using Ivy.IoC.Core;
using Ivy.PayPal.Api.Core.Interfaces.Providers;
using Ivy.PayPal.Api.Core.Interfaces.Services;
using Ivy.PayPal.Api.Tests.Base;
using Moq;
using Xunit;

namespace Ivy.PayPal.Api.Tests.Services
{
    public class PayPalUrlGeneratorTests : 
        PayPalApiTestBase<IPayPalUrlGenerator>
    {
        #region Variables & Constants

        private Mock<IPayPalApiConfigProvider> _mockConfigProvider;

        #endregion

        #region SetUp & TearDown

        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            _mockConfigProvider = InitializeMoq<IPayPalApiConfigProvider>(containerGen);
        }

        #endregion

        #region Tests

        [Fact]
        public void GetPayPalUrl_Executes_As_Expected_For_Sandbox()
        {
            _mockConfigProvider.Setup(x => x.SandboxMode).Returns(true);

            Assert.Equal("https://api.sandbox.paypal.com/", Sut.GetPayPalUrl());
        }

        [Fact]
        public void GetPayPalUrl_Executes_As_Expected_For_Not_Sandbox()
        {
            _mockConfigProvider.Setup(x => x.SandboxMode).Returns(false);

            Assert.Equal("https://api.paypal.com/", Sut.GetPayPalUrl());
        }

        #endregion
    }
}
