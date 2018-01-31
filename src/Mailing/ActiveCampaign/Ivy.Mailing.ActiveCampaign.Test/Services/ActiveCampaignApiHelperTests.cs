using Ivy.Mailing.ActiveCampaign.Core.Interfaces.Services;
using Ivy.Mailing.ActiveCampaign.Core.Interfaces.Transformers;
using Ivy.Mailing.ActiveCampaign.Core.Models;
using Ivy.Mailing.ActiveCampaign.Test.Base;
using Ivy.IoC;
using Ivy.IoC.Core;
using Ivy.Mailing.Core.Interfaces.Services;
using Ivy.Mailing.Core.Models;
using Ivy.Web.Core.Client;
using Moq;
using System;
using System.Net.Http;
using Xunit;
using Ivy.Web.Core.Json;

namespace Ivy.Mailing.ActiveCampaign.Test.Services
{
    public class ActiveCampaignApiHelperTests : BaseActiveCampaignTest
    {
        #region Variables & Constants

        private readonly IMailingApiHelper _sut;
        private readonly IJsonSerializationService _serializer;

        private readonly Mock<IApiHelper> _mockApiHelper;
        private readonly Mock<IMailingRequestFactory> _mockRequestFactory;
        private readonly Mock<IActiveCampaignContactListDeserializer> _mockContactDeserializer;
        private readonly Mock<IActiveCampaignContactTransformer> _mockContactTransformer;

        #endregion

        #region SetUp & TearDown

        public ActiveCampaignApiHelperTests()
        {
            _serializer = ServiceLocator.Instance.GetService<IJsonSerializationService>();

            var containerGen = ServiceLocator.Instance.GetService<IContainerGenerator>();

            base.ConfigureContainer(containerGen);

            _mockApiHelper = new Mock<IApiHelper>();
            containerGen.RegisterInstance<IApiHelper>(_mockApiHelper.Object);

            _mockRequestFactory = new Mock<IMailingRequestFactory>();
            containerGen.RegisterInstance<IMailingRequestFactory>(_mockRequestFactory.Object);

            _mockContactDeserializer = new Mock<IActiveCampaignContactListDeserializer>();
            containerGen.RegisterInstance<IActiveCampaignContactListDeserializer>(_mockContactDeserializer.Object);

            _mockContactTransformer = new Mock<IActiveCampaignContactTransformer>();
            containerGen.RegisterInstance<IActiveCampaignContactTransformer>(_mockContactTransformer.Object);

            _sut = containerGen.GenerateContainer().GetService<IMailingApiHelper>();
        }

        #endregion

        #region Tests

        #region GetMemberAsync

        [Fact]
        public async void GetMemberAsync_Executes_As_Expected_With_Single_Return()
        {
            // Arrange
            const string email = "test@email.com";
            const string resultContent = "this is my test result content";
            HttpRequestMessage req = new HttpRequestMessage();
            HttpResponseMessage resp = new HttpResponseMessage
            {
                Content = new StringContent(resultContent)
            };

            var contact = new ActiveCampaignContact();
            var resultList = new ActiveCampaignContactList
            {
                { 1, contact }
            };

            var finalMember = new MailingMember();

            _mockRequestFactory.Setup(x => x.GenerateGetMemberRequest(email)).Returns(req);

            _mockApiHelper.Setup(x => x.SendApiDataAsync(req)).ReturnsAsync(resp);

            _mockContactDeserializer.Setup(x => x.Deserialize(resultContent)).Returns(resultList);

            _mockContactTransformer.Setup(x => x.Transform(contact)).Returns(finalMember);

            // Act
            var result = await _sut.GetMemberAsync(email);

            // Assert
            Assert.Same(finalMember, result);

            _mockRequestFactory.Verify(x => x.GenerateGetMemberRequest(email), Times.Once);

            _mockApiHelper.Verify(x => x.SendApiDataAsync(req), Times.Once);

            _mockContactDeserializer.Verify(x => x.Deserialize(resultContent), Times.Once);

            _mockContactTransformer.Verify(x => x.Transform(contact), Times.Once);
        }

        [Fact]
        public async void GetMemberAsync_Executes_As_Expected_With_Null_Return()
        {
            // Arrange
            const string email = "test@email.com";
            const string resultContent = "this is my test result content";
            HttpRequestMessage req = new HttpRequestMessage();
            HttpResponseMessage resp = new HttpResponseMessage
            {
                Content = new StringContent(resultContent)
            };

            var contact = new ActiveCampaignContact();
            var resultList = new ActiveCampaignContactList
            {
            };

            var finalMember = new MailingMember();

            _mockRequestFactory.Setup(x => x.GenerateGetMemberRequest(email)).Returns(req);

            _mockApiHelper.Setup(x => x.SendApiDataAsync(req)).ReturnsAsync(resp);

            _mockContactDeserializer.Setup(x => x.Deserialize(resultContent)).Returns(resultList);

            _mockContactTransformer.Setup(x => x.Transform(contact)).Returns(finalMember);

            // Act
            var result = await _sut.GetMemberAsync(email);

            // Assert
            Assert.Null(result);

            _mockRequestFactory.Verify(x => x.GenerateGetMemberRequest(email), Times.Once);

            _mockApiHelper.Verify(x => x.SendApiDataAsync(req), Times.Once);

            _mockContactDeserializer.Verify(x => x.Deserialize(resultContent), Times.Once);

            _mockContactTransformer.Verify(x => x.Transform(contact), Times.Never);
        }

        [Fact]
        public async void GetMemberAsync_Throws_Exception_On_Multiple_Return()
        {
            // Arrange
            const string email = "test@email.com";
            const string resultContent = "this is my test result content";
            HttpRequestMessage req = new HttpRequestMessage();
            HttpResponseMessage resp = new HttpResponseMessage
            {
                Content = new StringContent(resultContent)
            };

            var contact = new ActiveCampaignContact();
            var resultList = new ActiveCampaignContactList
            {
                { 1, contact },
                { 2, contact }
            };

            var finalMember = new MailingMember();

            _mockRequestFactory.Setup(x => x.GenerateGetMemberRequest(email)).Returns(req);

            _mockApiHelper.Setup(x => x.SendApiDataAsync(req)).ReturnsAsync(resp);

            _mockContactDeserializer.Setup(x => x.Deserialize(resultContent)).Returns(resultList);

            _mockContactTransformer.Setup(x => x.Transform(contact)).Returns(finalMember);

            // Act
            var e = await Assert.ThrowsAsync<Exception>(() => _sut.GetMemberAsync(email));

            // Assert
            var err = $"Found multiple ActiveCampaign users with the same email! Email: {email}";
            Assert.Equal(err, e.Message);

            _mockRequestFactory.Verify(x => x.GenerateGetMemberRequest(email), Times.Once);

            _mockApiHelper.Verify(x => x.SendApiDataAsync(req), Times.Once);

            _mockContactDeserializer.Verify(x => x.Deserialize(resultContent), Times.Once);

            _mockContactTransformer.Verify(x => x.Transform(contact), Times.Never);
        }

        #endregion

        #region AddMemberAsync

        [Fact]
        public async void SaveContactInfoAsync_Executes_As_Expected()
        {
            // Arrange
            var member = new MailingMember();
            var req = new HttpRequestMessage();
            var resp = new HttpResponseMessage();

            var acResp = new ActiveCampaignResponse { result_code = 1 };
            resp.Content = new StringContent(_serializer.Serialize(acResp));

            _mockRequestFactory.Setup(x => x.GenerateAddMemberRequest(member)).Returns(req);

            _mockApiHelper.Setup(x => x.SendApiDataAsync(req)).ReturnsAsync(resp);

            // Act
            var result = await _sut.AddMemberAsync(member);

            // Assert
            Assert.Same(member, result);

            _mockRequestFactory.Verify(x => x.GenerateAddMemberRequest(member), Times.Once);

            _mockApiHelper.Verify(x => x.SendApiDataAsync(req), Times.Once);
        }

        #endregion

        #region EditMemberAsync

        [Fact]
        public async void EditMemberAsync_Executes_As_Expected()
        {
            // Arrange
            var member = new MailingMember();
            var req = new HttpRequestMessage();
            var resp = new HttpResponseMessage();

            var acResp = new ActiveCampaignResponse { result_code = 1 };
            resp.Content = new StringContent(_serializer.Serialize(acResp));

            _mockRequestFactory.Setup(x => x.GenerateEditMemberRequest(member)).Returns(req);

            _mockApiHelper.Setup(x => x.SendApiDataAsync(req)).ReturnsAsync(resp);

            // Act
            var result = await _sut.EditMemberAsync(member);

            // Assert
            Assert.Same(member, result);

            _mockRequestFactory.Verify(x => x.GenerateEditMemberRequest(member), Times.Once);

            _mockApiHelper.Verify(x => x.SendApiDataAsync(req), Times.Once);
        }

        #endregion

        #endregion
    }
}
