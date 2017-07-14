using Ivy.Auth0.Core.Models;
using Ivy.Auth0.Management.Core.Models.Requests;
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
    public class Auth0UserManagementServiceTests : Auth0ManagementTestBase
    {
        #region Variables & Constants

        private readonly IAuth0UserManagementService _sut;

        private readonly Mock<IApiHelper> _mockClientHelper;
        private readonly Mock<IManagementApiTokenGenerator> _mockTokenGenerator;
        private readonly Mock<IAuth0ManagementRequestGenerator> _mockRequestGen;

        const string TestUserId = "TESTUserId";

        private readonly string apiToken = "TEST";
        private readonly HttpRequestMessage req = new HttpRequestMessage();

        #endregion

        #region SetUp & TearDown

        public Auth0UserManagementServiceTests()
        {
            var containerGen = ServiceLocator.Instance.Resolve<IContainerGenerator>();

            base.ConfigureContainer(containerGen);

            _mockClientHelper = new Mock<IApiHelper>();
            containerGen.RegisterInstance<IApiHelper>(_mockClientHelper.Object);
            
            _mockTokenGenerator = new Mock<IManagementApiTokenGenerator>();
            containerGen.RegisterInstance<IManagementApiTokenGenerator>(_mockTokenGenerator.Object);

            _mockRequestGen = new Mock<IAuth0ManagementRequestGenerator>();
            containerGen.RegisterInstance<IAuth0ManagementRequestGenerator>(_mockRequestGen.Object);

            var container = containerGen.GenerateContainer();

            _sut = container.Resolve<IAuth0UserManagementService>();

            _mockTokenGenerator.
                Setup(x => x.GetApiTokenAsync()).
                ReturnsAsync(apiToken);
        }

        #endregion

        #region Tests

        #region GetUsersAsync

        [Fact]
        public async void GetUsersAsync_Executes_As_Expected()
        {
            var model = new Auth0ListUsersRequest();
            var responseModel = new Auth0ListUsersResponse();

            _mockRequestGen.
                Setup(x => x.GenerateListUsersRequest(apiToken, model)).
                Returns(req);

            _mockClientHelper.
                Setup(x => x.GetApiDataAsync<Auth0ListUsersResponse>(req)).
                ReturnsAsync(responseModel);

            var result = await _sut.GetUsersAsync(model);

            Assert.Same(responseModel, result);

            _mockTokenGenerator.Verify(x => x.GetApiTokenAsync(), Times.Once);
            _mockRequestGen.Verify(x => x.GenerateListUsersRequest(apiToken, model), Times.Once);
            _mockClientHelper.Verify(x => x.GetApiDataAsync<Auth0ListUsersResponse>(req), Times.Once);
        }

        #endregion

        #region CreateUserAsync

        [Fact]
        public async void CreateUserAsync_Executes_As_Expected()
        {
            var model = new Auth0CreateUserRequest();
            var responseModel = new Auth0User();

            _mockRequestGen.
                Setup(x => x.GenerateCreateUserRequest(apiToken, model)).
                Returns(req);

            _mockClientHelper.
                Setup(x => x.GetApiDataAsync<Auth0User>(req)).
                ReturnsAsync(responseModel);

            var result = await _sut.CreateUserAsync(model);

            Assert.Same(responseModel, result);

            _mockTokenGenerator.Verify(x => x.GetApiTokenAsync(), Times.Once);
            _mockRequestGen.Verify(x => x.GenerateCreateUserRequest(apiToken, model), Times.Once);
            _mockClientHelper.Verify(x => x.GetApiDataAsync<Auth0User>(req), Times.Once);
        }

        #endregion

        #region GetUserAsync

        [Fact]
        public async void GetUserAsync_Executes_As_Expected()
        {
            var responseModel = new Auth0User();

            _mockRequestGen.
                Setup(x => x.GenerateGetUserRequest(apiToken, TestUserId)).
                Returns(req);

            _mockClientHelper.
                Setup(x => x.GetApiDataAsync<Auth0User>(req)).
                ReturnsAsync(responseModel);

            var result = await _sut.GetUserAsync(TestUserId);

            Assert.Same(responseModel, result);

            _mockTokenGenerator.Verify(x => x.GetApiTokenAsync(), Times.Once);
            _mockRequestGen.Verify(x => x.GenerateGetUserRequest(apiToken, TestUserId), Times.Once);
            _mockClientHelper.Verify(x => x.GetApiDataAsync<Auth0User>(req), Times.Once);
        }

        #endregion

        #region UpdateUserAsync

        [Fact]
        public async void UpdateUserAsync_Executes_As_Expected()
        {
            var requestModel = new Auth0UpdateUserRequest();
            var responseModel = new Auth0User();

            _mockRequestGen.
                Setup(x => x.GenerateUpdateUserRequest(apiToken, requestModel)).
                Returns(req);

            _mockClientHelper.
                Setup(x => x.GetApiDataAsync<Auth0User>(req)).
                ReturnsAsync(responseModel);

            var result = await _sut.UpdateUserAsync(requestModel);

            Assert.Same(responseModel, result);

            _mockTokenGenerator.Verify(x => x.GetApiTokenAsync(), Times.Once);
            _mockRequestGen.Verify(x => x.GenerateUpdateUserRequest(apiToken, requestModel), Times.Once);
            _mockClientHelper.Verify(x => x.GetApiDataAsync<Auth0User>(req), Times.Once);
        }

        #endregion

        #region DeleteUserAsync

        [Fact]
        public async void DeleteUserAsync_Executes_As_Expected()
        {
            var response = new HttpResponseMessage();

            _mockRequestGen.
                Setup(x => x.GenerateDeleteUserRequest(apiToken, TestUserId)).
                Returns(req);

            _mockClientHelper.
                Setup(x => x.SendApiDataAsync(req)).
                ReturnsAsync(response);

            await _sut.DeleteUserAsync(TestUserId);

            _mockTokenGenerator.Verify(x => x.GetApiTokenAsync(), Times.Once);
            _mockRequestGen.Verify(x => x.GenerateDeleteUserRequest(apiToken, TestUserId), Times.Once);
            _mockClientHelper.Verify(x => x.SendApiDataAsync(req), Times.Once);
        }

        #endregion

        #endregion

    }
}
