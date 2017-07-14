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
    public class Auth0AccountManagementServiceTests : Auth0ManagementTestBase
    {
        #region Variables & Constants

        private readonly IAuth0AccountManagementService _sut;

        private readonly Mock<IApiHelper> _mockClientHelper;
        private readonly Mock<IApiManagementTokenGenerator> _mockTokenGenerator;
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

            _mockTokenGenerator = new Mock<IApiManagementTokenGenerator>();
            containerGen.RegisterInstance<IApiManagementTokenGenerator>(_mockTokenGenerator.Object);

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
                Setup(x => x.GenerateVerifyEmailRequest(apiToken, TestUserId)).
                Returns(req);

            _mockClientHelper.
                Setup(x => x.SendApiDataAsync(req)).
                ReturnsAsync(response);

            await _sut.ResendVerificationEmailAsync(TestUserId);

            _mockTokenGenerator.Verify(x => x.GetApiAuthTokenAsync(), Times.Once);
            _mockRequestGen.Verify(x => x.GenerateVerifyEmailRequest(apiToken, TestUserId), Times.Once);
            _mockClientHelper.Verify(x => x.SendApiDataAsync(req), Times.Once);
        }

        #endregion

        #endregion
    }
}
