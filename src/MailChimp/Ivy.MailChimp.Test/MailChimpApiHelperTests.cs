using Ivy.IoC;
using Ivy.IoC.Core;
using Ivy.MailChimp.Core.Models;
using Ivy.MailChimp.Core.Services;
using Ivy.MailChimp.Test.Base;
using Ivy.Web.Core.Client;
using Ivy.Web.Json;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Ivy.MailChimp.Test
{
    public class MailChimpApiHelperTests : MailChimpTestBase
    {
        #region Variables & Constants

        private IMailChimpApiHelper _sut;
        private IJsonSerializationService _serializationService;

        private Mock<IMailChimpRequestFactory> _mockRequestFactory;
        private Mock<IHttpClientHelper> _mockClientHelper;
        private Mock<ILogger> _mockLogger;

        #endregion

        #region SetUp & TearDown

        public MailChimpApiHelperTests()
        {
            _serializationService = ServiceLocator.Instance.Resolve<IJsonSerializationService>();

            // Configure Test Container
            var containerGen = ServiceLocator.Instance.Resolve<IContainerGenerator>();

            base.ConfigureContainer(containerGen);

            // Setup Logger Mock
            _mockLogger = new Mock<ILogger>();
            containerGen.RegisterInstance<ILogger>(_mockLogger.Object);

            // Setup HttpClientFactory Mock
            _mockClientHelper = new Mock<IHttpClientHelper>();
            containerGen.RegisterInstance<IHttpClientHelper>(_mockClientHelper.Object);

            // Setup MailChimpRequestFactory Mock
            _mockRequestFactory = new Mock<IMailChimpRequestFactory>();
            containerGen.RegisterInstance<IMailChimpRequestFactory>(_mockRequestFactory.Object);

            var container = containerGen.GenerateContainer();

            _sut = container.Resolve<IMailChimpApiHelper>();
        }

        #endregion

        #region Tests

        #region EditMemberAsync

        [Fact]
        public async void EditMemberAsync_Executes_As_Expected()
        {
            var resultMember = new MailChimpMember { email_address = "test@gmail.com" };
            var resultMemberContent = new StringContent(_serializationService.Serialize(resultMember));

            var member = new MailChimpMember();
            var req = new HttpRequestMessage();
            var response = new HttpResponseMessage();
            response.StatusCode = System.Net.HttpStatusCode.OK;
            response.Content = resultMemberContent;

            _mockRequestFactory.Setup(x => x.GenerateEditMemberRequest(member))
                .Returns(req);

            _mockClientHelper.Setup(x => x.SendAsync(req)).Returns(Task.FromResult(response));

            var result = await _sut.EditMemberAsync(member);

            Assert.Equal(resultMember.email_address, result.email_address);

            _mockRequestFactory.Verify(x => x.GenerateEditMemberRequest(member), Times.Once);

            _mockClientHelper.Verify(x => x.SendAsync(req), Times.Once);
        }

        //[Fact]
        //public void EditMemberAsync_Reports_Errors_As_Expected()
        //{
        //    var resultMember = new MailChimpMember { email_address = "test@gmail.com" };

        //    var resultErr = new MailChimpError { detail = "TEST Detail", type = "TEST Type" };
        //    string responseBody = JsonConvert.SerializeObject(resultErr);
        //    var resultMemberContent = new StringContent(responseBody);

        //    var member = new MailChimpMember();
        //    var req = new HttpRequestMessage();
        //    var response = new HttpResponseMessage();
        //    response.StatusCode = System.Net.HttpStatusCode.NotFound;
        //    response.Content = resultMemberContent;

        //    var errMsg = $"Failed to complete API request! Request Url: {req.RequestUri} / " +
        //            $"Error Object Detail: {resultErr.detail} / Error Object Type: {resultErr.type} / " +
        //            $"Response Status: {response.StatusCode} / Response Body: {responseBody}";

        //    _mockRequestFactory.Setup(x => x.GenerateEditMemberRequest(member))
        //        .Returns(req);

        //    _mockClientHelper.Setup(x => x.SendAsync(req)).Returns(Task.FromResult(response));

        //    _mockLogger.Setup(x => x.LogError(errMsg, new object[] { }));

        //    var e = Assert.ThrowsAsync<Exception>(() => _sut.EditMemberAsync(member));

        //    _mockRequestFactory.Verify(x => x.GenerateEditMemberRequest(member), Times.Once);

        //    _mockClientHelper.Verify(x => x.SendAsync(req), Times.Once);

        //    _mockLogger.Verify(x => x.LogError(errMsg, new object[] { }), Times.Once);
        //}

        #endregion

        #region GetMemberAsync

        [Fact]
        public async void GetMemberAsync_Executes_As_Expected()
        {
            const string testEmail = "test@gmail.com";
            var resultMember = new MailChimpMember { email_address = testEmail };
            var resultMemberContent = new StringContent(_serializationService.Serialize(resultMember));

            var member = new MailChimpMember();
            var req = new HttpRequestMessage();
            var response = new HttpResponseMessage();
            response.StatusCode = System.Net.HttpStatusCode.OK;
            response.Content = resultMemberContent;

            _mockRequestFactory.Setup(x => x.GenerateGetMemberRequest(testEmail))
                .Returns(req);

            _mockClientHelper.Setup(x => x.SendAsync(req)).Returns(Task.FromResult(response));

            var result = await _sut.GetMemberAsync(testEmail);

            Assert.Equal(resultMember.email_address, result.email_address);

            _mockRequestFactory.Verify(x => x.GenerateGetMemberRequest(testEmail), Times.Once);

            _mockClientHelper.Verify(x => x.SendAsync(req), Times.Once);
        }

        //[Fact]
        //public void GetMemberAsync_Reports_Errors_As_Expected()
        //{

        //}

        #endregion

        #region SaveContactInfoAsync

        [Fact]
        public async void SaveContactInfoAsync_Executes_As_Expected()
        {
            const string testEmail = "test@gmail.com";
            var contactInfo = new MailChimpContactInfo { email_address = testEmail };
            var resultMember = new MailChimpMember { email_address = testEmail };
            var resultMemberContent = new StringContent(_serializationService.Serialize(resultMember));

            var member = new MailChimpMember();
            var req = new HttpRequestMessage();
            var response = new HttpResponseMessage();
            response.StatusCode = System.Net.HttpStatusCode.OK;
            response.Content = resultMemberContent;

            _mockRequestFactory.Setup(x => x.GenerateSubmitMemberRequest(contactInfo))
                .Returns(req);

            _mockClientHelper.Setup(x => x.SendAsync(req)).Returns(Task.FromResult(response));

            var result = await _sut.SaveContactInfoAsync(contactInfo);

            Assert.Equal(resultMember.email_address, result.email_address);

            _mockRequestFactory.Verify(x => x.GenerateSubmitMemberRequest(contactInfo), Times.Once);

            _mockClientHelper.Verify(x => x.SendAsync(req), Times.Once);
        }

        //[Fact]
        //public void SaveContactInfoAsync_Reports_Errors_As_Expected()
        //{

        //}

        #endregion

        #region GetContactListAsync

        [Fact]
        public async void GetContactListAsync_Executes_As_Expected()
        {
            const string testListId = "testListId";

            var resultObj = new MailChimpList { name = "TEST" };
            var resultMemberContent = new StringContent(_serializationService.Serialize(resultObj));

            var member = new MailChimpMember();
            var req = new HttpRequestMessage();
            var response = new HttpResponseMessage();
            response.StatusCode = System.Net.HttpStatusCode.OK;
            response.Content = resultMemberContent;

            _mockRequestFactory.Setup(x => x.GenerateGetListRequest(testListId))
                .Returns(req);

            _mockClientHelper.Setup(x => x.SendAsync(req)).Returns(Task.FromResult(response));

            var result = await _sut.GetListAsync(testListId);

            Assert.Equal(resultObj.name, result.name);

            _mockRequestFactory.Verify(x => x.GenerateGetListRequest(testListId), Times.Once);

            _mockClientHelper.Verify(x => x.SendAsync(req), Times.Once);
        }

        #endregion

        #endregion
    }
}
