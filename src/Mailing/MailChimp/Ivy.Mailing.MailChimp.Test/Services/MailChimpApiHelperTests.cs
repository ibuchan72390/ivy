using Ivy.IoC;
using Ivy.IoC.Core;
using Ivy.Mailing.MailChimp.Core.Models;
using Ivy.Mailing.MailChimp.Test.Base;
using Ivy.Mailing.Core.Interfaces.Services;
using Ivy.Mailing.Core.Models;
using Ivy.Web.Core.Client;
using Ivy.Web.Core.Json;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Ivy.Mailing.MailChimp.Core.Interfaces.Transformers;

namespace Ivy.Mailing.MailChimp.Test
{
    public class MailChimpApiHelperTests : MailChimpTestBase
    {
        #region Variables & Constants

        private IMailingApiHelper _sut;
        private IJsonSerializationService _serializationService;

        private Mock<IMailingRequestFactory> _mockRequestFactory;
        private Mock<IHttpClientHelper> _mockClientHelper;
        private Mock<ILogger<IMailingApiHelper>> _mockLogger;
        private Mock<IJsonSerializationService> _mockSerialier;
        private Mock<IMailChimpContactTransformer> _mockTransformer;

        #endregion

        #region SetUp & TearDown

        public MailChimpApiHelperTests()
        {
            _serializationService = ServiceLocator.Instance.GetService<IJsonSerializationService>();

            // Configure Test Container
            var containerGen = ServiceLocator.Instance.GetService<IContainerGenerator>();

            base.ConfigureContainer(containerGen);

            // Setup Logger Mock
            _mockLogger = new Mock<ILogger<IMailingApiHelper>>();
            containerGen.RegisterInstance<ILogger<IMailingApiHelper>>(_mockLogger.Object);

            // Setup HttpClientFactory Mock
            _mockClientHelper = new Mock<IHttpClientHelper>();
            containerGen.RegisterInstance<IHttpClientHelper>(_mockClientHelper.Object);

            // Setup MailChimpRequestFactory Mock
            _mockRequestFactory = new Mock<IMailingRequestFactory>();
            containerGen.RegisterInstance<IMailingRequestFactory>(_mockRequestFactory.Object);

            // Setup JsonSerializationService Mock
            _mockSerialier = new Mock<IJsonSerializationService>();
            containerGen.RegisterInstance<IJsonSerializationService>(_mockSerialier.Object);

            // Setup MailChimpContactTransformer Mock
            _mockTransformer = new Mock<IMailChimpContactTransformer>();
            containerGen.RegisterInstance<IMailChimpContactTransformer>(_mockTransformer.Object);

            var container = containerGen.GenerateContainer();

            _sut = container.GetService<IMailingApiHelper>();
        }

        #endregion

        #region Tests

        #region EditMemberAsync

        [Fact]
        public async void EditMemberAsync_Executes_As_Expected()
        {
            var resultMember = new MailChimpMember { email_address = "test@gmail.com", status = "subscribed" };
            var resultMemberContent = new StringContent(_serializationService.Serialize(resultMember));

            var member = new MailingMember();
            var req = new HttpRequestMessage();
            var response = new HttpResponseMessage();
            response.StatusCode = System.Net.HttpStatusCode.OK;
            response.Content = resultMemberContent;

            _mockRequestFactory.Setup(x => x.GenerateEditMemberRequest(member))
                .Returns(req);

            _mockClientHelper.Setup(x => x.SendAsync(req)).ReturnsAsync(response);

            _mockSerialier.Setup(x => x.Deserialize<MailChimpMember>(It.IsAny<string>())).
                Returns(resultMember);

            _mockTransformer.Setup(x => x.Transform(resultMember)).Returns(member);

            var result = await _sut.EditMemberAsync(member);

            Assert.Same(result, member);

            _mockRequestFactory.Verify(x => x.GenerateEditMemberRequest(member), Times.Once);

            _mockClientHelper.Verify(x => x.SendAsync(req), Times.Once);

            _mockSerialier.Verify(x => x.Deserialize<MailChimpMember>(It.IsAny<string>()), Times.Once);

            _mockTransformer.Verify(x => x.Transform(resultMember), Times.Once);
        }

        #endregion

        #region GetMemberAsync

        [Fact]
        public async void GetMemberAsync_Executes_As_Expected()
        {
            const string testEmail = "test@gmail.com";
            var resultMember = new MailChimpMember { email_address = testEmail, status = "subscribed" };
            var resultMemberContent = new StringContent(_serializationService.Serialize(resultMember));

            var member = new MailChimpMember();
            var req = new HttpRequestMessage();
            var response = new HttpResponseMessage();
            response.StatusCode = System.Net.HttpStatusCode.OK;
            response.Content = resultMemberContent;
            var mailingMember = new MailingMember();

            _mockRequestFactory.Setup(x => x.GenerateGetMemberRequest(testEmail))
                .Returns(req);

            _mockClientHelper.Setup(x => x.SendAsync(req)).Returns(Task.FromResult(response));

            _mockSerialier.Setup(x => x.Deserialize<MailChimpMember>(It.IsAny<string>())).
                Returns(resultMember);

            _mockTransformer.Setup(x => x.Transform(resultMember)).Returns(mailingMember);

            var result = await _sut.GetMemberAsync(testEmail);

            Assert.Same(result, mailingMember);

            _mockRequestFactory.Verify(x => x.GenerateGetMemberRequest(testEmail), Times.Once);

            _mockClientHelper.Verify(x => x.SendAsync(req), Times.Once);

            _mockSerialier.Verify(x => x.Deserialize<MailChimpMember>(It.IsAny<string>()), Times.Once);

            _mockTransformer.Verify(x => x.Transform(resultMember), Times.Once);

        }

        #endregion

        #region SaveContactInfoAsync

        [Fact]
        public async void SaveContactInfoAsync_Executes_As_Expected()
        {
            const string testEmail = "test@gmail.com";
            var contactInfo = new MailingMember { Email = testEmail };
            var resultMember = new MailChimpMember { email_address = testEmail, status = "subscribed" };
            var resultMemberContent = new StringContent(_serializationService.Serialize(resultMember));

            var member = new MailChimpMember();
            var req = new HttpRequestMessage();
            var response = new HttpResponseMessage();
            response.StatusCode = System.Net.HttpStatusCode.OK;
            response.Content = resultMemberContent;

            _mockRequestFactory.Setup(x => x.GenerateAddMemberRequest(contactInfo))
                .Returns(req);

            _mockClientHelper.Setup(x => x.SendAsync(req)).Returns(Task.FromResult(response));

            _mockSerialier.Setup(x => x.Deserialize<MailChimpMember>(It.IsAny<string>())).
                Returns(resultMember);

            _mockTransformer.Setup(x => x.Transform(resultMember)).Returns(contactInfo);

            var result = await _sut.AddMemberAsync(contactInfo);

            Assert.Equal(contactInfo, result);

            _mockRequestFactory.Verify(x => x.GenerateAddMemberRequest(contactInfo), Times.Once);

            _mockClientHelper.Verify(x => x.SendAsync(req), Times.Once);

            _mockSerialier.Verify(x => x.Deserialize<MailChimpMember>(It.IsAny<string>()), Times.Once);

            _mockTransformer.Verify(x => x.Transform(resultMember), Times.Once);

        }

        #endregion

        #endregion
    }
}
