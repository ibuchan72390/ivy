using Ivy.Auth0.Core.Models;
using Ivy.Auth0.Core.Providers;
using Ivy.Auth0.Core.Services;
using Ivy.Auth0.Test.Base;
using Ivy.IoC;
using Ivy.IoC.Core;
using Moq;
using Newtonsoft.Json;
using System.Net.Http;
using Xunit;

namespace Ivy.Auth0.Test
{
    public class Auth0ManagementRequestGeneratorTests : Auth0TestBase
    {
        #region Variables & Constants

        private readonly IAuth0ManagementRequestGenerator _sut;

        private readonly Mock<IAuth0ConfigurationProvider> _mockConfigProvider;
        private readonly Mock<IUserProvider> _mockUserProvider;

        const string testDomain = "TESTDomain";
        const string testApiClientId = "TESTApiClientId";
        const string testApiClientSecret = "TESTApiClientSecret";
        const string testSpaClientId = "TESTSpaClientId";

        const string testAuthenticationId = "TESTAuthenticationId";

        #endregion

        #region SetUp & TearDown

        public Auth0ManagementRequestGeneratorTests()
        {
            var containerGen = ServiceLocator.Instance.Resolve<IContainerGenerator>();

            base.ConfigureContainer(containerGen);

            _mockConfigProvider = new Mock<IAuth0ConfigurationProvider>();
            containerGen.RegisterInstance<IAuth0ConfigurationProvider>(_mockConfigProvider.Object);

            _mockConfigProvider.Setup(x => x.Domain).Returns(testDomain);
            _mockConfigProvider.Setup(x => x.ApiClientId).Returns(testApiClientId);
            _mockConfigProvider.Setup(x => x.ApiClientSecret).Returns(testApiClientSecret);
            _mockConfigProvider.Setup(x => x.SpaClientId).Returns(testSpaClientId);

            _mockUserProvider = new Mock<IUserProvider>();
            containerGen.RegisterInstance<IUserProvider>(_mockUserProvider.Object);

            _mockUserProvider.Setup(x => x.AuthenticationId).Returns(testAuthenticationId);

            var container = containerGen.GenerateContainer();

            _sut = container.Resolve<IAuth0ManagementRequestGenerator>();
        }

        #endregion

        #region Tests

        #region GenerateManagementApiTokenRequest

        [Fact]
        public async void GenerateManagementApiTokenRequest_Works_As_Expected()
        {
            var req = _sut.GenerateManagementApiTokenRequest();

            Assert.Equal(HttpMethod.Post, req.Method);

            string expectedUri = $"https://{testDomain}/oauth/token";
            Assert.Equal(expectedUri.ToLower(), req.RequestUri.ToString());

            var acceptHeader = req.Headers.Accept.ToString();
            Assert.Equal("application/json", acceptHeader);

            var expectedBodyModel = new Auth0ApiTokenRequest
            {
                client_id = testApiClientId,
                client_secret = testApiClientSecret,

                audience = $"https://{testDomain}/api/v2/",
                grant_type = "client_credentials"
            };

            var tokenBodyString = JsonConvert.SerializeObject(expectedBodyModel);

            var requestBody = await req.Content.ReadAsStringAsync();

            Assert.Equal(requestBody, tokenBodyString);
        }

        #endregion

        #region GenerateVerifyEmailRequest

        [Fact]
        public async void GenerateVerifyEmailRequest_Works_As_Expected()
        {
            const string testToken = "TESTToken";

            var req = _sut.GenerateVerifyEmailRequest(testToken);

            Assert.Equal(HttpMethod.Post, req.Method);

            string expectedUri = $"https://{testDomain}/api/v2/jobs/verification-email";
            Assert.Equal(expectedUri.ToLower(), req.RequestUri.ToString());

            var acceptHeader = req.Headers.Accept.ToString();
            Assert.Equal("application/json", acceptHeader);

            var authHeader = req.Headers.Authorization;
            Assert.Equal($"Bearer {testToken}", authHeader.ToString());

            var verifyEmailModel = new Auth0VerifyEmailModel
            {
                user_id = testAuthenticationId,
                client_id = testSpaClientId
            };

            var tokenBodyString = JsonConvert.SerializeObject(verifyEmailModel);

            var requestBody = await req.Content.ReadAsStringAsync();

            Assert.Equal(requestBody, tokenBodyString);
        }

        #endregion

        #endregion

    }
}
