using Ivy.Auth0.Management.Core.Services;
using Ivy.Auth0.Management.Test.Base;
using Ivy.IoC.Core;
using Ivy.Web.Core.Client;
using Moq;
using System.Net.Http;
using Xunit;

namespace Ivy.Auth0.Management.Test.Services
{
    public class Auth0AccountManagementServiceTests : 
        Auth0ManagementTestBase<IAuth0AccountManagementService>
    {
        #region Variables & Constants

        private Mock<IApiHelper> _mockClientHelper;
        private Mock<IManagementApiTokenGenerator> _mockTokenGenerator;
        private Mock<IAuth0ManagementRequestGenerator> _mockRequestGen;

        const string TestUserId = "TESTUserId";

        private readonly string apiToken = "TEST";
        private readonly HttpRequestMessage req = new HttpRequestMessage();

        #endregion

        #region SetUp & TearDown

        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            _mockClientHelper = InitializeMoq<IApiHelper>(containerGen);

            _mockTokenGenerator = InitializeMoq<IManagementApiTokenGenerator>(containerGen);
            _mockTokenGenerator.
                Setup(x => x.GetApiTokenAsync()).
                ReturnsAsync(apiToken);

            _mockRequestGen = InitializeMoq<IAuth0ManagementRequestGenerator>(containerGen);
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

            await Sut.ResendVerificationEmailAsync(TestUserId);

            _mockTokenGenerator.Verify(x => x.GetApiTokenAsync(), Times.Once);
            _mockRequestGen.Verify(x => x.GenerateVerifyEmailRequest(apiToken, TestUserId), Times.Once);
            _mockClientHelper.Verify(x => x.SendApiDataAsync(req), Times.Once);
        }

        #endregion

        #endregion
    }
}
