using Ivy.IoC;
using Ivy.PayPal.Api.Core.Interfaces.Providers;
using Ivy.PayPal.Api.Core.Interfaces.Services;
using Ivy.PayPal.Api.Tests.Base;
using Moq;
using Xunit;

namespace Ivy.PayPal.Api.Tests.Services
{
    public class PayPalUrlGeneratorTests : PayPalApiTestBase
    {
        #region Variables & Constants

        private readonly IPayPalUrlGenerator _sut;

        private readonly Mock<IPayPalApiConfigProvider> _mockConfigProvider;

        #endregion

        #region SetUp & TearDown

        public PayPalUrlGeneratorTests()
        {
            var containerGen = new ContainerGenerator();

            base.ConfigureContainer(containerGen);

            _mockConfigProvider = new Mock<IPayPalApiConfigProvider>();
            containerGen.RegisterInstance<IPayPalApiConfigProvider>(_mockConfigProvider.Object);

            _sut = containerGen.GenerateContainer().GetService<IPayPalUrlGenerator>();
        }

        #endregion

        #region Tests

        [Fact]
        public void GetPayPalUrl_Executes_As_Expected_For_Sandbox()
        {
            _mockConfigProvider.Setup(x => x.SandboxMode).Returns(true);

            Assert.Equal("https://api.sandbox.paypal.com/", _sut.GetPayPalUrl());
        }

        [Fact]
        public void GetPayPalUrl_Executes_As_Expected_For_Not_Sandbox()
        {
            _mockConfigProvider.Setup(x => x.SandboxMode).Returns(false);

            Assert.Equal("https://api.paypal.com/", _sut.GetPayPalUrl());
        }

        #endregion
    }
}
