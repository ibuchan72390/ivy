using Ivy.IoC;
using Ivy.IoC.Core;
using Ivy.Mailing.MailChimp.Test.Base;
using Ivy.Mailing.Core.Interfaces.Services;
using Ivy.Mailing.Core.Models;
using Moq;
using Xunit;
using Ivy.Mailing.Core.Enums;
using Ivy.Mailing.MailChimp.Core.Providers;

namespace Ivy.Mailing.MailChimp.Test
{
    public class MailChimpServiceTests : 
        MailChimpTestBase<IMailingService>
    {
        #region Variables & Constants

        private Mock<IMailingApiHelper> _mockApiHelper;
        private Mock<IMailChimpConfigurationProvider> _configProvider;

        private const string testEmail = "test@gmail.com";

        private const string newMsg = "New";
        private const string pendingMsg = "Pending";
        private const string existingMsg = "Existing";

        #endregion

        #region SetUp & TearDown

        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            _mockApiHelper = InitializeMoq<IMailingApiHelper>(containerGen);
            _configProvider = InitializeMoq<IMailChimpConfigurationProvider>(containerGen);

            _configProvider.Setup(x => x.AlreadyEnrolledMessage).Returns(existingMsg);
            _configProvider.Setup(x => x.NewEnrollmentMessage).Returns(newMsg);
            _configProvider.Setup(x => x.PendingEnrollmentMessage).Returns(pendingMsg);
        }

        #endregion

        #region Tests

        #region ProcessContactInfoAsync

        [Fact]
        public async void ProcessContactInfoAsync_Returns_As_Expected_For_Existing_Subscribed_User()
        {
            var contactInfo = new MailingMember { Email = testEmail };

            var returnedMember = new MailingMember { Status = MailingStatusName.Subscribed };

            _mockApiHelper.Setup(x => x.GetMemberAsync(testEmail)).ReturnsAsync(returnedMember);

            var result = await Sut.ProcessContactInfoAsync(contactInfo);

            _mockApiHelper.Verify(x => x.GetMemberAsync(testEmail), Times.Once);

            Assert.True(result.IsValid);
            Assert.Equal(existingMsg, result.Message);
        }

        [Fact]
        public async void ProcessContactInfoAsync_Saves_New_Contact_Info_If_Member_Is_Null()
        {
            var contactInfo = new MailingMember { Email = testEmail };

            MailingMember returnedMember = null;

            _mockApiHelper.Setup(x => x.GetMemberAsync(testEmail)).ReturnsAsync(returnedMember);

            _mockApiHelper.Setup(x => x.AddMemberAsync(It.IsAny<MailingMember>()))
                .ReturnsAsync(returnedMember);

            var result = await Sut.ProcessContactInfoAsync(contactInfo);

            _mockApiHelper.Verify(x => x.GetMemberAsync(testEmail), Times.Once);

            _mockApiHelper.Verify(x => x.AddMemberAsync(It.IsAny<MailingMember>()), Times.Once);

            Assert.True(result.IsValid);
            Assert.Equal(newMsg, result.Message);
        }

        [Fact]
        public async void ProcessContactInfoAsync_Updates_Pending_Status_If_Member_Exists()
        {
            var contactInfo = new MailingMember { Email = testEmail };

            MailingMember returnedMember = new MailingMember { Status = MailingStatusName.Unsubscribed };

            _mockApiHelper.Setup(x => x.GetMemberAsync(testEmail)).ReturnsAsync(returnedMember);

            _mockApiHelper.Setup(x => x.EditMemberAsync(returnedMember)).ReturnsAsync(returnedMember);

            var result = await Sut.ProcessContactInfoAsync(contactInfo);

            _mockApiHelper.Verify(x => x.GetMemberAsync(testEmail), Times.Once);

            _mockApiHelper.Verify(x => x.EditMemberAsync(returnedMember), Times.Never);

            Assert.True(result.IsValid);
            Assert.Equal(existingMsg, result.Message);
        }

        [Fact]
        public async void ProcessContactInfoAsync_Returns_Custom_Message_If_New_Submission_Received_While_Pending_Status()
        {
            var contactInfo = new MailingMember { Email = testEmail };

            MailingMember returnedMember = new MailingMember { Status = MailingStatusName.Pending };

            _mockApiHelper.Setup(x => x.GetMemberAsync(testEmail)).ReturnsAsync(returnedMember);

            _mockApiHelper.Setup(x => x.EditMemberAsync(returnedMember)).ReturnsAsync(returnedMember);

            var result = await Sut.ProcessContactInfoAsync(contactInfo);

            _mockApiHelper.Verify(x => x.GetMemberAsync(testEmail), Times.Once);

            _mockApiHelper.Verify(x => x.EditMemberAsync(returnedMember), Times.Never);

            // Gets set during the process...
            Assert.Equal(MailingStatusName.Pending, returnedMember.Status);

            Assert.True(result.IsValid);
            Assert.Equal(pendingMsg, result.Message);
        }

        #endregion

        #endregion
    }
}
