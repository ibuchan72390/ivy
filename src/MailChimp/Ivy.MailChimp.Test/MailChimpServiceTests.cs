using Ivy.IoC;
using Ivy.IoC.Core;
using Ivy.MailChimp.Core.Enums;
using Ivy.MailChimp.Core.Models;
using Ivy.MailChimp.Core.Services;
using Ivy.MailChimp.Test.Base;
using Moq;
using Xunit;

namespace Ivy.MailChimp.Test
{
    public class MailChimpServiceTests : MailChimpTestBase
    {
        #region Variables & Constants

        private readonly IMailChimpService _sut;

        private readonly Mock<IMailChimpApiHelper> _mockApiHelper;

        private const string testEmail = "test@gmail.com";

        private const string successValidation = "We have successfully received your contact information.  " +
            "You should receive an email in your inbox shortly requesting you to confirm our mailing list. " +
            "Please confirm the email or you may not receive our newsletter.";

        #endregion

        #region SetUp & TearDown

        public MailChimpServiceTests()
        {
            _mockApiHelper = new Mock<IMailChimpApiHelper>();

            var containerGen = ServiceLocator.Instance.Resolve<IContainerGenerator>();

            base.ConfigureContainer(containerGen);

            containerGen.RegisterInstance<IMailChimpApiHelper>(_mockApiHelper.Object);

            var container = containerGen.GenerateContainer();

            _sut = container.Resolve<IMailChimpService>();
        }

        #endregion

        #region Tests

        #region ProcessContactInfoAsync

        [Fact]
        public async void ProcessContactInfoAsync_Returns_As_Expected_For_Existing_Subscribed_User()
        {
            var contactInfo = new MailChimpContactInfo { email_address = testEmail };

            var returnedMember = new MailChimpMember {status = MailChimpStatusName.subscribed.ToString() };

            _mockApiHelper.Setup(x => x.GetMemberAsync(testEmail)).ReturnsAsync(returnedMember);

            var result = await _sut.ProcessContactInfoAsync(contactInfo);

            _mockApiHelper.Verify(x => x.GetMemberAsync(testEmail), Times.Once);

            Assert.True(result.IsValid);
            Assert.Equal("You have already signed up for our mailing list!", result.Message);
        }

        [Fact]
        public async void ProcessContactInfoAsync_Saves_New_Contact_Info_If_Member_Is_Null()
        {
            var contactInfo = new MailChimpContactInfo { email_address = testEmail };

            MailChimpMember returnedMember = null;

            _mockApiHelper.Setup(x => x.GetMemberAsync(testEmail)).ReturnsAsync(returnedMember);

            _mockApiHelper.Setup(x => x.SaveContactInfoAsync(It.IsAny<MailChimpContactInfo>()))
                .ReturnsAsync(returnedMember);

            var result = await _sut.ProcessContactInfoAsync(contactInfo);

            _mockApiHelper.Verify(x => x.GetMemberAsync(testEmail), Times.Once);

            _mockApiHelper.Verify(x => x.SaveContactInfoAsync(It.IsAny<MailChimpContactInfo>()), Times.Once);

            Assert.True(result.IsValid);
            Assert.Equal(successValidation, result.Message);
        }

        [Fact]
        public async void ProcessContactInfoAsync_Updates_Pending_Status_If_Member_Exists()
        {
            var contactInfo = new MailChimpContactInfo { email_address = testEmail };

            MailChimpMember returnedMember = new MailChimpMember { status = "TEST" };

            _mockApiHelper.Setup(x => x.GetMemberAsync(testEmail)).ReturnsAsync(returnedMember);

            _mockApiHelper.Setup(x => x.EditMemberAsync(returnedMember)).ReturnsAsync(returnedMember);

            var result = await _sut.ProcessContactInfoAsync(contactInfo);

            _mockApiHelper.Verify(x => x.GetMemberAsync(testEmail), Times.Once);

            _mockApiHelper.Verify(x => x.EditMemberAsync(returnedMember), Times.Once);

            // Gets set during the process...
            Assert.Equal(MailChimpStatusName.pending.ToString(), returnedMember.status);

            Assert.True(result.IsValid);
            Assert.Equal(successValidation, result.Message);
        }

        [Fact]
        public async void ProcessContactInfoAsync_Returns_Custom_Message_If_New_Submission_Received_While_Pending_Status()
        {
            var contactInfo = new MailChimpContactInfo { email_address = testEmail };

            MailChimpMember returnedMember = new MailChimpMember { status = MailChimpStatusName.pending.ToString() };

            _mockApiHelper.Setup(x => x.GetMemberAsync(testEmail)).ReturnsAsync(returnedMember);

            _mockApiHelper.Setup(x => x.EditMemberAsync(returnedMember)).ReturnsAsync(returnedMember);

            var result = await _sut.ProcessContactInfoAsync(contactInfo);

            _mockApiHelper.Verify(x => x.GetMemberAsync(testEmail), Times.Once);

            _mockApiHelper.Verify(x => x.EditMemberAsync(returnedMember), Times.Never);

            // Gets set during the process...
            Assert.Equal(MailChimpStatusName.pending.ToString(), returnedMember.status);

            Assert.True(result.IsValid);
            Assert.Equal("We have already received your email, " +
                        "but it does not appear that you have validated the acceptance email. " +
                        "Please check your inbox for a confirmation email.", result.Message);
        }

        #endregion

        #endregion
    }
}
