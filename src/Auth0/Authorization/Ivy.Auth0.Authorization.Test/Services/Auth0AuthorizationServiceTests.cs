using Ivy.Auth0.Authorization.Core.Models.Responses;
using Ivy.Auth0.Authorization.Core.Services;
using Ivy.Auth0.Authorization.Test.Base;
using Ivy.Auth0.Core.Models;
using Ivy.IoC;
using Ivy.IoC.Core;
using Ivy.Web.Core.Client;
using Moq;
using System.Collections.Generic;
using System.Net.Http;
using Xunit;

namespace Ivy.Auth0.Authorization.Test.Services
{
    public class Auth0AuthorizationServiceTests : 
        Auth0AuthorizationTestBase<IAuth0AuthorizationService>
    {
        #region Variables & Constants

        private Mock<IApiHelper> _mockClientHelper;
        private Mock<IAuthorizationApiTokenGenerator> _mockTokenGenerator;
        private Mock<IAuth0AuthorizationRequestGenerator> _mockRequestGen;

        const string TestUserId = "TESTUserId";

        const string TestRoleId = "TESTRoleId";
        string[] TestRoles = new string[] { TestRoleId };

        private readonly string apiToken = "TESTToken";
        private readonly HttpRequestMessage req = new HttpRequestMessage();

        private readonly HttpResponseMessage resp = new HttpResponseMessage();

        #endregion

        #region SetUp & TearDown

        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            _mockClientHelper = InitializeMoq<IApiHelper>(containerGen);
            _mockRequestGen = InitializeMoq<IAuth0AuthorizationRequestGenerator>(containerGen);

            _mockTokenGenerator = InitializeMoq<IAuthorizationApiTokenGenerator>(containerGen);
            _mockTokenGenerator.
                Setup(x => x.GetApiTokenAsync()).
                ReturnsAsync(apiToken);
        }

        #endregion

        #region Tests

        #region AddUserRolesAsync

        [Fact]
        public async void AddUserRolesAsync_Executes_As_Expected()
        {
            _mockRequestGen.
                Setup(x => x.GenerateAddUserRolesRequest(apiToken, TestUserId, TestRoles)).
                Returns(req);

            _mockClientHelper.
                Setup(x => x.SendApiDataAsync(req)).
                ReturnsAsync(resp);

            await Sut.AddUserRolesAsync(TestUserId, TestRoles);

            _mockTokenGenerator.Verify(x => x.GetApiTokenAsync(), Times.Once);
            _mockRequestGen.Verify(x => x.GenerateAddUserRolesRequest(apiToken, TestUserId, TestRoles), Times.Once);
            _mockClientHelper.Verify(x => x.SendApiDataAsync(req), Times.Once);
        }

        #endregion

        #region DeleteUserRolesAsync

        [Fact]
        public async void DeleteUserRolesAsync_Executes_As_Expected()
        {
            _mockRequestGen.
                Setup(x => x.GenerateDeleteUserRolesRequest(apiToken, TestUserId, TestRoles)).
                Returns(req);

            _mockClientHelper.
                Setup(x => x.SendApiDataAsync(req)).
                ReturnsAsync(resp);

            await Sut.DeleteUserRolesAsync(TestUserId, TestRoles);

            _mockTokenGenerator.Verify(x => x.GetApiTokenAsync(), Times.Once);
            _mockRequestGen.Verify(x => x.GenerateDeleteUserRolesRequest(apiToken, TestUserId, TestRoles), Times.Once);
            _mockClientHelper.Verify(x => x.SendApiDataAsync(req), Times.Once);
        }

        #endregion

        #region GetAllRolesAsync

        [Fact]
        public async void GetAllRolesAsync_Executes_As_Expected()
        {
            var responseModel = new Auth0RoleResponse();

            _mockRequestGen.
                Setup(x => x.GenerateGetRolesRequest(apiToken)).
                Returns(req);

            _mockClientHelper.
                Setup(x => x.GetApiDataAsync<Auth0RoleResponse>(req)).
                ReturnsAsync(responseModel);

            var results = await Sut.GetAllRolesAsync();

            Assert.Same(responseModel, results);

            _mockTokenGenerator.Verify(x => x.GetApiTokenAsync(), Times.Once);
            _mockRequestGen.Verify(x => x.GenerateGetRolesRequest(apiToken), Times.Once);
            _mockClientHelper.Verify(x => x.GetApiDataAsync<Auth0RoleResponse>(req), Times.Once);
        }

        #endregion

        #region GetUserRolesAsync

        [Fact]
        public async void GetUserRolesAsync_Executes_As_Expected()
        {
            var responseModel = new List<Auth0Role>();

            _mockRequestGen.
                Setup(x => x.GenerateGetUserRolesRequest(apiToken, TestUserId)).
                Returns(req);

            _mockClientHelper.
                Setup(x => x.GetApiDataAsync<IEnumerable<Auth0Role>>(req)).
                ReturnsAsync(responseModel);

            var results = await Sut.GetUserRolesAsync(TestUserId);

            Assert.Same(responseModel, results);

            _mockTokenGenerator.Verify(x => x.GetApiTokenAsync(), Times.Once);
            _mockRequestGen.Verify(x => x.GenerateGetUserRolesRequest(apiToken, TestUserId), Times.Once);
            _mockClientHelper.Verify(x => x.GetApiDataAsync<IEnumerable<Auth0Role>>(req), Times.Once);
        }

        #endregion

        #endregion
    }
}
