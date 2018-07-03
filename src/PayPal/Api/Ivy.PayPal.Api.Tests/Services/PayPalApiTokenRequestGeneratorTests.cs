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
        PayPalApiTestBase<IPayPalApiTokenRequestGenerator>
    {
        #region Variables & Constants

        private Mock<IPayPalApiConfigProvider> _mockConfig;
        private Mock<IPayPalUrlGenerator> _mockUrlGen;

        const string id = "Id";
        const string secret = "Secret";
        const string payPalUrl = "https://api.sandbox.paypal.com/";

        #endregion

        #region SetUp & TearDown

        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            _mockConfig = InitializeMoq<IPayPalApiConfigProvider>(containerGen);
            _mockConfig.Setup(x => x.ClientId).Returns(id);
            _mockConfig.Setup(x => x.ClientSecret).Returns(secret);

            _mockUrlGen = InitializeMoq<IPayPalUrlGenerator>(containerGen);
            _mockUrlGen.Setup(x => x.GetPayPalUrl()).Returns(payPalUrl);

        }

        #endregion

        #region Tests

        #region GenerateApiTokenRequest

        [Fact]
        public void GenerateApiTokenRequest_Executes_As_Expected()
        {
            // Act
            var result = Sut.GenerateApiTokenRequest();

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
