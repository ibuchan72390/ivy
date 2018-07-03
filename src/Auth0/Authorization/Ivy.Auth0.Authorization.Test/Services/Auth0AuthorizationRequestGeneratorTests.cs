using Ivy.Auth0.Authorization.Core.Providers;
using Ivy.Auth0.Authorization.Core.Services;
using Ivy.Auth0.Authorization.Test.Base;
using Ivy.Auth0.Core.Models.Requests;
using Ivy.Auth0.Core.Providers;
using Ivy.IoC;
using Ivy.IoC.Core;
using Ivy.Web.Core.Json;
using Moq;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using Xunit;

namespace Ivy.Auth0.Authorization.Test.Services
{
    public class Auth0AuthorizationRequestGeneratorTests : 
        Auth0AuthorizationTestBase<IAuth0AuthorizationRequestGenerator>
    {
        #region Variables & Constants

        private readonly IJsonSerializationService _serializationService;

        private Mock<IAuth0GenericConfigurationProvider> _mockConfig;
        private Mock<IAuth0ClientConfigurationProvider> _mockClientConfig;
        private Mock<IAuth0ApiConfigurationProvider> _mockApiConfig;
        private Mock<IAuth0AuthorizationConfigurationProvider> _mockAuthConfig;

        const string testUserId = "TESTUserId";

        const string testRoleId = "TESTRoleId";
        string[] testRoles => new string[] { testRoleId };

        const string testToken = "TESTToken";
        const string testConnection = "TESTConnection";

        const string testDomain = "TESTDomain.auth0.com";
        const string testApiClientId = "TESTApiClientId";
        const string testApiClientSecret = "TESTApiClientSecret";
        const string testSpaClientId = "TESTSpaClientId";

        const string testAuthenticationId = "TESTAuthenticationId";

        const string testAuthUrl = "https://iamglobaleducation.us.webtask.io/api";

        #endregion

        #region SetUp & TearDown

        public Auth0AuthorizationRequestGeneratorTests()
        {
            _serializationService = ServiceLocator.Instance.GetService<IJsonSerializationService>();
        }

        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            _mockConfig = InitializeMoq<IAuth0GenericConfigurationProvider>(containerGen);
            _mockConfig.Setup(x => x.Connection).Returns(testConnection);
            _mockConfig.Setup(x => x.Domain).Returns(testDomain);

            _mockApiConfig = InitializeMoq<IAuth0ApiConfigurationProvider>(containerGen);
            _mockApiConfig.Setup(x => x.ApiClientId).Returns(testApiClientId);
            _mockApiConfig.Setup(x => x.ApiClientSecret).Returns(testApiClientSecret);

            _mockClientConfig = InitializeMoq<IAuth0ClientConfigurationProvider>(containerGen);
            _mockClientConfig.Setup(x => x.SpaClientId).Returns(testSpaClientId);

            _mockAuthConfig = InitializeMoq<IAuth0AuthorizationConfigurationProvider>(containerGen);
            _mockAuthConfig.Setup(x => x.AuthorizationUrl).Returns(testAuthUrl);
        }

        #endregion

        #region Tests

        #region GenerateManagementApiTokenRequest

        [Fact]
        public async void GenerateManagementApiTokenRequest_Works_As_Expected()
        {
            var req = Sut.GenerateApiTokenRequest();

            Assert.Equal(HttpMethod.Post, req.Method);

            string expectedUri = $"https://{testDomain}/oauth/token";
            Assert.Equal(expectedUri.ToLower(), req.RequestUri.ToString());

            var acceptHeader = req.Headers.Accept.ToString();
            Assert.Equal("application/json", acceptHeader);

            var expectedBodyModel = new Auth0ApiTokenRequest
            {
                client_id = testApiClientId,
                client_secret = testApiClientSecret,

                grant_type = "client_credentials",

                // We use a custom audience for the Authorization API
                audience = "urn:auth0-authz-api"
            };

            var tokenBodyString = JsonConvert.SerializeObject(expectedBodyModel);

            var requestBody = await req.Content.ReadAsStringAsync();

            Assert.Equal(requestBody, tokenBodyString);
        }

        #endregion

        #region GenerateGetRolesRequest

        [Fact]
        public void GenerateGetRolesRequest_Creates_Request_As_Expected()
        {
            var req = Sut.GenerateGetRolesRequest(testToken);

            var expectedUrl = $"{testAuthUrl}/roles";

            ValidateRequest(req, new Uri(expectedUrl), HttpMethod.Get);
        }

        #endregion

        #region GenerateAddUserRoleRequest

        [Fact]
        public async void GenerateAddUserRoleRequest_Creates_Request_As_Expected()
        {
            var req = Sut.GenerateAddUserRolesRequest(testToken, testUserId, testRoles);

            string expectedUri = GenerateUserRoleUrl();

            ValidateRequest(req, new Uri(expectedUri), new HttpMethod("PATCH"));

            var body = await req.Content.ReadAsStringAsync();

            var result = _serializationService.Deserialize<string[]>(body);

            Assert.Single(result);
            Assert.Equal(testRoleId, result[0]);
        }

        #endregion

        #region GenerateDeleteUserRoleRequest

        [Fact]
        public async void GenerateDeleteUserRoleRequest_Creates_Request_As_Expected()
        {
            var req = Sut.GenerateDeleteUserRolesRequest(testToken, testUserId, testRoles);

            string expectedUri = GenerateUserRoleUrl();

            ValidateRequest(req, new Uri(expectedUri), HttpMethod.Delete);

            var body = await req.Content.ReadAsStringAsync();

            var result = _serializationService.Deserialize<string[]>(body);

            Assert.Single(result);
            Assert.Equal(testRoleId, result[0]);
        }

        #endregion

        #region GenerateGetUserRolesRequest

        [Fact]
        public void GenerateGetUserRolesRequest_Creates_Request_As_Expected()
        {
            var req = Sut.GenerateGetUserRolesRequest(testToken, testUserId);

            string expectedUri = GenerateUserRoleUrl();

            ValidateRequest(req, new Uri(expectedUri), HttpMethod.Get);
        }

        #endregion

        #endregion

        #region Helper Methods

        private string GenerateUserRoleUrl()
        {
            return $"{testAuthUrl}/users/{testUserId}/roles";
        }

        private void ValidateRequest(HttpRequestMessage req, Uri expectedUri, HttpMethod method)
        {
            Assert.Equal(expectedUri.ToString(), req.RequestUri.ToString());

            var acceptHeader = req.Headers.Accept.ToString();
            Assert.Equal("application/json", acceptHeader);

            var authHeader = req.Headers.Authorization;
            Assert.Equal($"Bearer {testToken}", authHeader.ToString());

            Assert.Equal(method, req.Method);
        }

        #endregion
    }
}
