using Ivy.Auth0.Core.Models;
using Ivy.Auth0.Core.Models.Requests;
using Ivy.Auth0.Core.Providers;
using Ivy.Auth0.Management.Core.Models.Requests;
using Ivy.Auth0.Management.Core.Providers;
using Ivy.Auth0.Management.Core.Services;
using Ivy.Auth0.Management.Test.Base;
using Ivy.IoC;
using Ivy.IoC.Core;
using Moq;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using Xunit;

namespace Ivy.Auth0.Management.Test.Services
{
    public class Auth0ManagementRequestGeneratorTests : Auth0ManagementTestBase
    {
        #region Variables & Constants

        private readonly IAuth0ManagementRequestGenerator _sut;

        private readonly IAuth0QueryStringUriGenerator _queryStringGenerator;

        private readonly Mock<IAuth0GenericConfigurationProvider> _mockConfig;
        private readonly Mock<IAuth0ClientConfigurationProvider> _mockClientConfig;
        private readonly Mock<IAuth0ApiConfigurationProvider> _mockApiConfig;
        private readonly Mock<IAuth0ManagementConfigurationProvider> _mockManagementConfig;

        const string testUserId = "TESTUserId";

        const string testToken = "TESTToken";
        const string testConnection = "TESTConnection";

        const string testDomain = "TESTDomain.auth0.com";
        const string testApiClientId = "TESTApiClientId";
        const string testApiClientSecret = "TESTApiClientSecret";
        const string testSpaClientId = "TESTSpaClientId";

        const string testAuthenticationId = "TESTAuthenticationId";

        #endregion

        #region SetUp & TearDown

        public Auth0ManagementRequestGeneratorTests()
        {
            _queryStringGenerator = ServiceLocator.Instance.Resolve<IAuth0QueryStringUriGenerator>();

            var containerGen = ServiceLocator.Instance.Resolve<IContainerGenerator>();

            base.ConfigureContainer(containerGen);

            _mockConfig = new Mock<IAuth0GenericConfigurationProvider>();
            containerGen.RegisterInstance<IAuth0GenericConfigurationProvider>(_mockConfig.Object);

            _mockConfig.Setup(x => x.Connection).Returns(testConnection);
            _mockConfig.Setup(x => x.Domain).Returns(testDomain);

            _mockApiConfig = new Mock<IAuth0ApiConfigurationProvider>();
            _mockApiConfig.Setup(x => x.ApiClientId).Returns(testApiClientId);
            _mockApiConfig.Setup(x => x.ApiClientSecret).Returns(testApiClientSecret);
            containerGen.RegisterInstance<IAuth0ApiConfigurationProvider>(_mockApiConfig.Object);

            _mockClientConfig = new Mock<IAuth0ClientConfigurationProvider>();
            _mockClientConfig.Setup(x => x.SpaClientId).Returns(testSpaClientId);
            containerGen.RegisterInstance<IAuth0ClientConfigurationProvider>(_mockClientConfig.Object);

            _mockManagementConfig = new Mock<IAuth0ManagementConfigurationProvider>();
            containerGen.RegisterInstance<IAuth0ManagementConfigurationProvider>(_mockManagementConfig.Object);

            var container = containerGen.GenerateContainer();

            _sut = container.Resolve<IAuth0ManagementRequestGenerator>();
        }

        #endregion

        #region Tests

        #region GenerateManagementApiTokenRequest

        [Fact]
        public async void GenerateManagementApiTokenRequest_Works_As_Expected()
        {
            var req = _sut.GenerateApiTokenRequest();

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
            var req = _sut.GenerateVerifyEmailRequest(testToken, testAuthenticationId);

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

        #region GenerateListUsersRequest

        [Fact]
        public void GenerateListUsersRequest_Generates_Request_As_Expected()
        {
            var testModel = new Auth0ListUsersRequest();

            var expectedBaseString = $"https://{testDomain}/api/v2/users";

            var req = _sut.GenerateListUsersRequest(testToken, testModel);

            testModel.Connection = testConnection;
            var expectedUri = _queryStringGenerator.GenerateGetUsersQueryString(expectedBaseString, testModel);

            ValidateRequest(req, expectedUri, HttpMethod.Get);
        }

        #endregion

        #region GenerateCreateUserRequest

        [Fact]
        public void GenerateCreateUserRequest_Generates_Request_As_Expected()
        {
            var testModel = new Auth0CreateUserRequest();

            var expectedUri = new Uri($"https://{testDomain}/api/v2/users");

            var req = _sut.GenerateCreateUserRequest(testToken, testModel);

            ValidateRequest(req, expectedUri, HttpMethod.Post);
        }

        [Fact]
        public void GenerateCreateUserRequest_Removes_Phone_If_Phone_Null()
        {
            var testModel = new Auth0CreateUserRequest { phone_number = null };

            var expectedUri = new Uri($"https://{testDomain}/api/v2/users");

            var req = _sut.GenerateCreateUserRequest(testToken, testModel);

            ValidateRequest(req, expectedUri, HttpMethod.Post);

            var stringContent = req.Content as StringContent;

            var result = stringContent.ReadAsStringAsync().Result;

            // Phone and verified has been removed
            Assert.True(result.IndexOf("phone_number") == -1);
            Assert.True(result.IndexOf("phone_verified") == -1);

            var resultModel = JsonConvert.DeserializeObject<Auth0CreateUserRequest>(result);

            Assert.Null(resultModel.phone_number);
        }

        [Fact]
        public void GenerateCreateUserRequest_Removes_Phone_If_Phone_Empty()
        {
            var testModel = new Auth0CreateUserRequest { phone_number = "" };

            var expectedUri = new Uri($"https://{testDomain}/api/v2/users");

            var req = _sut.GenerateCreateUserRequest(testToken, testModel);

            ValidateRequest(req, expectedUri, HttpMethod.Post);

            var stringContent = req.Content as StringContent;

            var result = stringContent.ReadAsStringAsync().Result;

            // Phone and verified has been removed
            Assert.True(result.IndexOf("phone_number") == -1);
            Assert.True(result.IndexOf("phone_verified") == -1);

            var resultModel = JsonConvert.DeserializeObject<Auth0CreateUserRequest>(result);

            Assert.Null(resultModel.phone_number);
        }

        [Fact]
        public void GenerateCreateUserRequest_Validates_Phone_Against_Auth0_Regex()
        {
            var request = new Auth0CreateUserRequest { phone_number = "TEST" };

            var expectedUri = new Uri($"https://{testDomain}/api/v2/users");

            var e = Assert.Throws<Exception>(() => _sut.GenerateCreateUserRequest(testToken, request));

            var expectedMsg = "Invalid phone number received! Must match regex - ^\\+[0-9]{1,15}$" +
                        $" / Phone: {request.phone_number}";

            Assert.Equal(expectedMsg, e.Message);
        }

        [Fact]
        public void GenerateCreateUserRequest_Removes_Username_If_UseUsername_Config_False()
        {
            _mockManagementConfig.Setup(x => x.UseUsername).Returns(false);

            var testModel = new Auth0CreateUserRequest { username = "TEST" };

            var expectedUri = new Uri($"https://{testDomain}/api/v2/users");

            var req = _sut.GenerateCreateUserRequest(testToken, testModel);

            ValidateRequest(req, expectedUri, HttpMethod.Post);

            var stringContent = req.Content as StringContent;

            var result = stringContent.ReadAsStringAsync().Result;

            // Username has been removed
            Assert.True(result.IndexOf("username") == -1);

            var resultModel = JsonConvert.DeserializeObject<Auth0CreateUserRequest>(result);

            Assert.Null(resultModel.username);
        }

        #endregion

        #region GenerateGetUserRequest

        [Fact]
        public void GenerateGetUserRequest_Generates_Request_As_Expected()
        {
            var expectedUri = new Uri($"https://{testDomain}/api/v2/users/{testUserId}");

            var req = _sut.GenerateGetUserRequest(testToken, testUserId);

            ValidateRequest(req, expectedUri, HttpMethod.Get);
        }

        #endregion

        #region GenerateUpdateUserRequest

        [Fact]
        public void GenerateUpdateUserRequest_Generates_Request_As_Expected()
        {
            var model = new Auth0UpdateUserRequest { user_id = testUserId };

            var expectedUri = new Uri($"https://{testDomain}/api/v2/users/{testUserId}");

            var req = _sut.GenerateUpdateUserRequest(testToken, model);

            ValidateRequest(req, expectedUri, new HttpMethod("PATCH"));
        }

        #endregion

        #region GenerateDeleteUserRequest

        [Fact]
        public void GenerateDeleteUserRequest_Generates_Request_As_Expected()
        {
            var expectedUri = new Uri($"https://{testDomain}/api/v2/users/{testUserId}");

            var req = _sut.GenerateDeleteUserRequest(testToken, testUserId);

            ValidateRequest(req, expectedUri, HttpMethod.Delete);
        }

        #endregion

        #endregion

        #region Helper Methods

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
