using Ivy.Auth0.Core.Models;
using Ivy.Auth0.Core.Models.Requests;
using Ivy.Auth0.Core.Models.Responses;
using Ivy.Auth0.Core.Services;
using Ivy.Auth0.Test.Base;
using Ivy.IoC;
using Ivy.IoC.Core;
using Ivy.Web.Core.Client;
using Moq;
using System.Net.Http;
using Xunit;

namespace Ivy.Auth0.Test.Services
{
    public class Auth0AccountManagementServiceTests : Auth0TestBase
    {
        #region Variables & Constants

        private readonly IAuth0AccountManagementService _sut;

        private readonly Mock<IApiHelper> _mockClientHelper;
        private readonly Mock<IUserProvider> _mockUserProvider;
        private readonly Mock<IApiAuthTokenGenerator> _mockTokenGenerator;
        private readonly Mock<IAuth0ManagementRequestGenerator> _mockRequestGen;

        const string TestUserId = "TESTUserId";

        private readonly string apiToken = "TEST";
        private readonly HttpRequestMessage req = new HttpRequestMessage();

        #endregion

        #region SetUp & TearDown

        public Auth0AccountManagementServiceTests()
        {
            var containerGen = ServiceLocator.Instance.Resolve<IContainerGenerator>();

            base.ConfigureContainer(containerGen);

            _mockClientHelper = new Mock<IApiHelper>();
            containerGen.RegisterInstance<IApiHelper>(_mockClientHelper.Object);

            _mockUserProvider = new Mock<IUserProvider>();
            containerGen.RegisterInstance<IUserProvider>(_mockUserProvider.Object);

            _mockTokenGenerator = new Mock<IApiAuthTokenGenerator>();
            containerGen.RegisterInstance<IApiAuthTokenGenerator>(_mockTokenGenerator.Object);

            _mockRequestGen = new Mock<IAuth0ManagementRequestGenerator>();
            containerGen.RegisterInstance<IAuth0ManagementRequestGenerator>(_mockRequestGen.Object);

            var container = containerGen.GenerateContainer();

            _sut = container.Resolve<IAuth0AccountManagementService>();

            _mockTokenGenerator.
                Setup(x => x.GetApiAuthTokenAsync()).
                ReturnsAsync(apiToken);
        }

        #endregion

        #region Tests

        #region ResendVerificationEmailAsync

        [Fact]
        public async void ResendVerificationEmailAsync_Executes_As_Expected()
        {
            var response = new HttpResponseMessage();

            _mockRequestGen.
                Setup(x => x.GenerateVerifyEmailRequest(apiToken)).
                Returns(req);

            _mockClientHelper.
                Setup(x => x.SendApiDataAsync(req)).
                ReturnsAsync(response);

            await _sut.ResendVerificationEmailAsync();

            _mockTokenGenerator.Verify(x => x.GetApiAuthTokenAsync(), Times.Once);
            _mockRequestGen.Verify(x => x.GenerateVerifyEmailRequest(apiToken), Times.Once);
            _mockClientHelper.Verify(x => x.SendApiDataAsync(req), Times.Once);
        }

        #endregion

        #endregion
    }
}
