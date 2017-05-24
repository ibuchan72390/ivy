using Ivy.Auth0.Core.Services;
using Ivy.Auth0.Test.Base;
using Ivy.IoC;
using Ivy.IoC.Core;
using Ivy.Web.Core.Client;
using Moq;
using System;
using System.Net.Http;
using Xunit;

namespace Ivy.Auth0.Test
{
    public class Auth0ManagementServiceTests : Auth0TestBase
    {
        #region Variables & Constants

        private readonly IAuth0ManagementService _sut;

        private readonly Mock<IHttpClientHelper> _mockClientHelper;
        private readonly Mock<IUserProvider> _mockUserProvider;
        private readonly Mock<IApiAuthTokenGenerator> _mockTokenGenerator;
        private readonly Mock<IAuth0ManagementRequestGenerator> _mockRequestGen;

        private readonly string apiToken = "TEST";
        private readonly HttpRequestMessage req;

        #endregion

        #region SetUp & TearDown

        public Auth0ManagementServiceTests()
        {
            var containerGen = ServiceLocator.Instance.Resolve<IContainerGenerator>();

            base.ConfigureContainer(containerGen);

            _mockClientHelper = new Mock<IHttpClientHelper>();
            containerGen.RegisterInstance<IHttpClientHelper>(_mockClientHelper.Object);

            _mockUserProvider = new Mock<IUserProvider>();
            containerGen.RegisterInstance<IUserProvider>(_mockUserProvider.Object);

            _mockTokenGenerator = new Mock<IApiAuthTokenGenerator>();
            containerGen.RegisterInstance<IApiAuthTokenGenerator>(_mockTokenGenerator.Object);

            _mockRequestGen = new Mock<IAuth0ManagementRequestGenerator>();
            containerGen.RegisterInstance<IAuth0ManagementRequestGenerator>(_mockRequestGen.Object);

            var container = containerGen.GenerateContainer();

            _sut = container.Resolve<IAuth0ManagementService>();


            _mockTokenGenerator.
                Setup(x => x.GetApiAuthTokenAsync()).
                ReturnsAsync(apiToken);
        }

        #endregion

        #region Tests

        [Fact]
        public async void ResendVerificationEmailAsync_Executes_Without_Issue_If_Success()
        {
            var response = new HttpResponseMessage();
            response.StatusCode = System.Net.HttpStatusCode.OK;

            _mockClientHelper.
                Setup(x => x.SendAsync(req)).
                ReturnsAsync(response);

            await _sut.ResendVerificationEmailAsync();

            // If we made it this far, we're fine

            _mockTokenGenerator.Verify(x => x.GetApiAuthTokenAsync(), Times.Once);
            _mockRequestGen.Verify(x => x.GenerateVerifyEmailRequest(apiToken), Times.Once);
            _mockClientHelper.Verify(x => x.SendAsync(req), Times.Once);
        }

        [Fact]
        public async void ResendVerificationEmailAsync_Throws_Exception_If_Response_Is_Not_Success_Status_Code()
        {
            const string testAuthId = "TESTAuthenticationId";

            _mockUserProvider.Setup(x => x.AuthenticationId).Returns(testAuthId);

            var response = new HttpResponseMessage();
            response.StatusCode = System.Net.HttpStatusCode.Unauthorized;

            _mockClientHelper.
                Setup(x => x.SendAsync(req)).
                ReturnsAsync(response);

            var e = await Assert.ThrowsAsync<Exception>(() => _sut.ResendVerificationEmailAsync());

            Assert.Equal($"Verification email was not successful for auth id: {testAuthId}", e.Message);

            // If we made it this far, we're fine

            _mockTokenGenerator.Verify(x => x.GetApiAuthTokenAsync(), Times.Once);
            _mockRequestGen.Verify(x => x.GenerateVerifyEmailRequest(apiToken), Times.Once);
            _mockClientHelper.Verify(x => x.SendAsync(req), Times.Once);

            _mockUserProvider.Verify(x => x.AuthenticationId, Times.Once);
        }
        
        #endregion

    }
}
