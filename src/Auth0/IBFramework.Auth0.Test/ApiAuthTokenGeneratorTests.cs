using IBFramework.Auth0.Core.Models;
using IBFramework.Auth0.Core.Services;
using IBFramework.Auth0.Test.Base;
using IBFramework.IoC;
using IBFramework.IoC.Core;
using IBFramework.Web.Core.Client;
using Moq;
using Newtonsoft.Json;
using System.Net.Http;
using Xunit;

namespace IBFramework.Auth0.Test
{
    public class ApiAuthTokenGeneratorTests : Auth0TestBase
    {
        #region Variables & Constants

        private readonly IApiAuthTokenGenerator _sut;

        private readonly Mock<IHttpClientHelper> _mockClientHelper;
        private readonly Mock<IAuth0ManagementRequestGenerator> _mockRequestGen;

        private readonly HttpRequestMessage request;
        private readonly HttpResponseMessage response;

        private readonly Auth0AccessTokenResponse modelResponse = new Auth0AccessTokenResponse
        {
            access_token = "TEST-Token-Contents",
        };

        #endregion

        #region SetUp & TearDown

        public ApiAuthTokenGeneratorTests()
        {
            var containerGen = ServiceLocator.Instance.Resolve<IContainerGenerator>();

            //base.ConfigureContainer(containerGen);

            _mockClientHelper = new Mock<IHttpClientHelper>();
            containerGen.RegisterInstance<IHttpClientHelper>(_mockClientHelper.Object);

            _mockRequestGen = new Mock<IAuth0ManagementRequestGenerator>();
            containerGen.RegisterInstance<IAuth0ManagementRequestGenerator>(_mockRequestGen.Object);


            var container = containerGen.GenerateContainer();
            _sut = container.Resolve<IApiAuthTokenGenerator>();


            request = new HttpRequestMessage();

            response = new HttpResponseMessage();
            var responseText = JsonConvert.SerializeObject(modelResponse);
            response.Content = new StringContent(responseText);

            _mockRequestGen.
                Setup(x => x.GenerateManagementApiTokenRequest()).
                Returns(request);

            _mockClientHelper.
                Setup(x => x.SendAsync(request)).
                ReturnsAsync(response);
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

            _mockClientHelper.
                Verify(x => x.SendAsync(request), Times.Once);
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

            _mockClientHelper.
                Verify(x => x.SendAsync(request), Times.Exactly(repeatCount));
        }

        #endregion
    }
}
