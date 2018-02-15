using Ivy.IoC;
using Ivy.IoC.Core;
using Ivy.PayPal.Api.Core.Interfaces.Providers;
using Ivy.PayPal.Api.Core.Interfaces.Services;
using Ivy.PayPal.Api.Tests.Base;
using Moq;
using System;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Ivy.PayPal.Api.Tests.Services
{
    public class PayPalApiTokenRequestGeneratorTests :
        PayPalApiTestBase
    {
        #region Variables & Constants

        private readonly IPayPalApiTokenRequestGenerator _sut;

        private readonly Mock<IPayPalApiConfigProvider> _mockConfig;

        const string id = "Id";
        const string secret = "Secret";

        #endregion

        #region SetUp & TearDown

        public PayPalApiTokenRequestGeneratorTests()
        {
            var containerGen = ServiceLocator.Instance.GetService<IContainerGenerator>();

            base.ConfigureContainer(containerGen);

            _mockConfig = new Mock<IPayPalApiConfigProvider>();
            containerGen.RegisterInstance<IPayPalApiConfigProvider>(_mockConfig.Object);

            _mockConfig.Setup(x => x.ClientId).Returns(id);
            _mockConfig.Setup(x => x.ClientSecret).Returns(secret);

            _sut = containerGen.GenerateContainer().GetService<IPayPalApiTokenRequestGenerator>();
        }

        #endregion

        #region Tests

        #region GenerateApiTokenRequest

        [Fact]
        public void GenerateApiTokenRequest_Executes_As_Expected()
        {
            // Act
            var result = _sut.GenerateApiTokenRequest();

            // Assert
            Assert.Equal("https://api.sandbox.paypal.com/v1/oauth2/token", result.RequestUri.ToString());
            Assert.Equal(HttpMethod.Post, result.Method);
            Assert.Equal("application/json", result.Headers.Accept.ToString());
            Assert.Equal("en_US", result.Headers.AcceptLanguage.ToString());

            var expectedAuthString = $"{id}:{secret}";
            expectedAuthString = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(expectedAuthString));

            Assert.Equal(expectedAuthString, result.Headers.Authorization.Parameter);
            Assert.Equal("Basic", result.Headers.Authorization.Scheme);

            var body = result.Content.ReadAsStringAsync().Result;
            var bodyPieces = body.Split('=');
            Assert.Equal("grant_type", bodyPieces[0]);
            Assert.Equal("client_credentials", bodyPieces[1]);
        }

        #endregion

        #endregion
    }
}
