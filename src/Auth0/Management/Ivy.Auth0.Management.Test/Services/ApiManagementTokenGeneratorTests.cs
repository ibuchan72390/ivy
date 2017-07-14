using Ivy.Auth0.Management.Core.Models.Responses;
using Ivy.Auth0.Management.Core.Services;
using Ivy.Auth0.Management.Test.Base;
using Ivy.IoC;
using Ivy.IoC.Core;
using Ivy.Web.Core.Client;
using Moq;
using System.Net.Http;
using Xunit;

namespace Ivy.Auth0.Management.Test.Services
{
    public class ApiManagementTokenGeneratorTests : Auth0ManagementTestBase
    {
        #region Variables & Constants

        private readonly IApiManagementTokenGenerator _sut;

        private readonly Mock<IApiHelper> _mockApiHelper;
        private readonly Mock<IAuth0ManagementRequestGenerator> _mockRequestGen;

        private readonly HttpRequestMessage request;

        private readonly Auth0AccessTokenResponse modelResponse = new Auth0AccessTokenResponse
        {
            access_token = "TEST-Token-Contents",
        };

        #endregion

        #region SetUp & TearDown

        public ApiManagementTokenGeneratorTests()
        {
            var containerGen = ServiceLocator.Instance.Resolve<IContainerGenerator>();

            base.ConfigureContainer(containerGen);

            _mockApiHelper = new Mock<IApiHelper>();
            containerGen.RegisterInstance<IApiHelper>(_mockApiHelper.Object);

            _mockRequestGen = new Mock<IAuth0ManagementRequestGenerator>();
            containerGen.RegisterInstance<IAuth0ManagementRequestGenerator>(_mockRequestGen.Object);


            var container = containerGen.GenerateContainer();
            _sut = container.Resolve<IApiManagementTokenGenerator>();


            request = new HttpRequestMessage();

            _mockRequestGen.
                Setup(x => x.GenerateManagementApiTokenRequest()).
                Returns(request);

            _mockApiHelper.
                Setup(x => x.GetApiDataAsync<Auth0AccessTokenResponse>(request)).
                ReturnsAsync(modelResponse);
        }

        #endregion

        #region Tests

        [Fact]
        public async void GetApiAuthTokenAsync_Returns_As_Expected_For_Fresh_Token()
        {
            var authToken = await _sut.GetApiAuthTokenAsync();

            Assert.Equal(authToken, modelResponse.access_token);

            _mockRequestGen.
                Verify(x => x.GenerateManagementApiTokenRequest(), Times.Once);

            _mockApiHelper.
                Verify(x => x.GetApiDataAsync<Auth0AccessTokenResponse>(request), Times.Once);
        }

        [Fact]
        public async void GetApiAuthTokenAsync_Returns_As_Expected_For_Cached_Token()
        {
            const int repeatCount = 4;

            for (var i = 0; i < repeatCount; i++)
            {
                var authToken = await _sut.GetApiAuthTokenAsync();

                Assert.Equal(authToken, modelResponse.access_token);
            }

            _mockRequestGen.
                Verify(x => x.GenerateManagementApiTokenRequest(), Times.Exactly(repeatCount));

            _mockApiHelper.
                Verify(x => x.GetApiDataAsync<Auth0AccessTokenResponse>(request), Times.Exactly(repeatCount));
        }

        #endregion
    }
}
