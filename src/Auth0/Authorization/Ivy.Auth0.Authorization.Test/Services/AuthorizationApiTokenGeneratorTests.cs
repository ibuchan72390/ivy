using Ivy.Auth0.Authorization.Core.Services;
using Ivy.Auth0.Authorization.Test.Base;
using Ivy.Auth0.Core.Models.Responses;
using Ivy.IoC;
using Ivy.IoC.Core;
using Ivy.Web.Core.Client;
using Moq;
using System.Net.Http;
using Xunit;

namespace Ivy.Auth0.Authorization.Test.Services
{
    public class AuthorizationApiTokenGeneratorTests : 
        Auth0AuthorizationTestBase<IAuthorizationApiTokenGenerator>
    {
        #region Variables & Constants

        private Mock<IApiHelper> _mockApiHelper;
        private Mock<IAuth0AuthorizationRequestGenerator> _mockRequestGen;

        private readonly HttpRequestMessage request;

        private readonly Auth0AccessTokenResponse modelResponse = new Auth0AccessTokenResponse
        {
            access_token = "TEST-Token-Contents",
        };

        #endregion

        #region SetUp & TearDown

        public AuthorizationApiTokenGeneratorTests()
        {
            request = new HttpRequestMessage();

            _mockApiHelper.
                Setup(x => x.GetApiDataAsync<Auth0AccessTokenResponse>(request)).
                ReturnsAsync(modelResponse);

            _mockRequestGen.
                Setup(x => x.GenerateApiTokenRequest()).
                Returns(request);

        }

        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            _mockApiHelper = InitializeMoq<IApiHelper>(containerGen);
            _mockRequestGen = InitializeMoq<IAuth0AuthorizationRequestGenerator>(containerGen);
        }

        #endregion

        #region Tests

        [Fact]
        public async void GetApiAuthTokenAsync_Returns_As_Expected_For_Fresh_Token()
        {
            var authToken = await Sut.GetApiTokenAsync();

            Assert.Equal(authToken, modelResponse.access_token);

            _mockRequestGen.
                Verify(x => x.GenerateApiTokenRequest(), Times.Once);

            _mockApiHelper.
                Verify(x => x.GetApiDataAsync<Auth0AccessTokenResponse>(request), Times.Once);
        }

        [Fact]
        public async void GetApiAuthTokenAsync_Returns_As_Expected_For_Cached_Token()
        {
            const int repeatCount = 4;

            for (var i = 0; i < repeatCount; i++)
            {
                var authToken = await Sut.GetApiTokenAsync();

                Assert.Equal(authToken, modelResponse.access_token);
            }

            _mockRequestGen.
                Verify(x => x.GenerateApiTokenRequest(), Times.Exactly(repeatCount));

            _mockApiHelper.
                Verify(x => x.GetApiDataAsync<Auth0AccessTokenResponse>(request), Times.Exactly(repeatCount));
        }

        #endregion
    }
}
