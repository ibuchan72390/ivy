﻿using Ivy.IoC;
using Ivy.IoC.Core;
using Ivy.PayPal.Api.Core.Interfaces.Services;
using Ivy.PayPal.Api.Core.Models;
using Ivy.PayPal.Api.Tests.Base;
using Ivy.Web.Core.Client;
using Ivy.Web.Core.Json;
using Moq;
using System.Net.Http;
using Xunit;

namespace Ivy.PayPal.Api.Tests.Services
{
    public class PayPalApiTokenGeneratorTests :
        PayPalApiTestBase<IPayPalApiTokenGenerator>
    {
        #region Variables & Constants

        private readonly IJsonSerializationService _serializer;

        private Mock<IApiHelper> _mockApiHelper;
        private Mock<IPayPalApiTokenRequestGenerator> _mockRequestGen;

        #endregion

        #region SetUp & TearDown

        public PayPalApiTokenGeneratorTests()
        {
            _serializer = TestContainer.GetService<IJsonSerializationService>();
        }

        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            _mockApiHelper = InitializeMoq<IApiHelper>(containerGen);
            _mockRequestGen = InitializeMoq<IPayPalApiTokenRequestGenerator>(containerGen);
        }

        #endregion

        #region Tests

        #region GetApiTokenAsync

        [Fact]
        public async void GetApiTokenAsync_Returns_As_Expected()
        {
            // Arrange
            var responseContent = new PayPalTokenResponse();

            var json = _serializer.Serialize(responseContent);

            var req = new HttpRequestMessage();

            req.Content = new StringContent(json);

            _mockRequestGen.Setup(x => x.GenerateApiTokenRequest()).
                Returns(req);

            _mockApiHelper.Setup(x => x.GetApiDataAsync<PayPalTokenResponse>(req)).
                ReturnsAsync(responseContent);


            // Act
            var result = await Sut.GetApiTokenAsync();


            // Assert
            Assert.Same(responseContent, result);

            _mockRequestGen.Verify(x => x.GenerateApiTokenRequest(), Times.Once);

            _mockApiHelper.Verify(x => x.GetApiDataAsync<PayPalTokenResponse>(req), Times.Once);
        }

        #endregion

        #endregion
    }
}
