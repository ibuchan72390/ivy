using Ivy.Mailing.ActiveCampaign.Core.Interfaces.Transformers;
using Ivy.Mailing.ActiveCampaign.Core.Models;
using Ivy.Mailing.ActiveCampaign.Test.Base;
using Ivy.Mailing.Core.Enums;
using Moq;
using System;
using Xunit;
using Ivy.Mailing.Core.Models;
using Ivy.IoC.Core;

namespace Ivy.Mailing.ActiveCampaign.Test.Transformers
{
    public class ActiveCampaignContactTransformerTests : 
        BaseActiveCampaignTest<IActiveCampaignContactTransformer>
    {
        #region Variables & Constants

        private Mock<IActiveCampaignExtraDataTransformer> _mockExtraDataTransformer;

        #endregion

        #region SetUp & TearDown

        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            _mockExtraDataTransformer = InitializeMoq<IActiveCampaignExtraDataTransformer>(containerGen);
        }

        #endregion

        #region Tests

        [Fact]
        public void Transform_Converts_ActiveCampaignContact_As_Expected_For_Subscribed()
        {
            // Arrange
            var obj = new ActiveCampaignContact
            {
                id  = "123",
                email = "test@email.com",
                first_name = "First Name",
                last_name = "Last Name",
                phone = "testphone",
                status = "1"
            };

            _mockExtraDataTransformer.Setup(x => x.Transform(It.IsAny<MailingMember>(), obj)).
                Returns((MailingMember mem, ActiveCampaignContact contact) => mem);

            // Act
            var result = Sut.Transform(obj);

            // Assert
            Assert.Equal(obj.id, result.Id);
            Assert.Equal(obj.email, result.Email);
            Assert.Equal(obj.first_name, result.FirstName);
            Assert.Equal(obj.last_name, result.LastName);
            Assert.Equal(obj.phone, result.Phone);
            Assert.Equal(MailingStatusName.Subscribed, result.Status);

            _mockExtraDataTransformer.Verify(x => x.Transform(It.IsAny<MailingMember>(), obj), 
                Times.Once);
        }

        [Fact]
        public void Transform_Converts_ActiveCampaignContact_As_Expected_For_Unsubscribed()
        {
            // Arrange
            var obj = new ActiveCampaignContact
            {
                id = "123",
                email = "test@email.com",
                first_name = "First Name",
                last_name = "Last Name",
                phone = "testphone",
                status = "2"
            };

            var expected = new MailingMember();

            _mockExtraDataTransformer.Setup(x => x.Transform(It.IsAny<MailingMember>(), obj)).
                Returns((MailingMember mem, ActiveCampaignContact contact) => mem);

            // Act
            var result = Sut.Transform(obj);

            // Assert
            Assert.Equal(obj.id, result.Id);
            Assert.Equal(obj.email, result.Email);
            Assert.Equal(obj.first_name, result.FirstName);
            Assert.Equal(obj.last_name, result.LastName);
            Assert.Equal(obj.phone, result.Phone);
            Assert.Equal(MailingStatusName.Unsubscribed, result.Status);

            _mockExtraDataTransformer.Verify(x => x.Transform(It.IsAny<MailingMember>(), obj), 
                Times.Once);
        }

        [Fact]
        public void Transform_Throws_Exception_For_Unknown_Status()
        {
            // Arrange
            var obj = new ActiveCampaignContact
            {
                id = "123",
                email = "test@email.com",
                first_name = "First Name",
                last_name = "Last Name",
                phone = "testphone",
                status = "FAKE"
            };

            var expected = new MailingMember();

            _mockExtraDataTransformer.Setup(x => x.Transform(It.IsAny<MailingMember>(), obj)).
                Returns((MailingMember mem, ActiveCampaignContact contact) => mem);

            // Act
            var e = Assert.Throws<Exception>(() => Sut.Transform(obj));

            // Assert
            var err = $"Unknown ActiveCampaignContact.status received! status: {obj.status}";
            Assert.Equal(err, e.Message);

            _mockExtraDataTransformer.Verify(x => x.Transform(It.IsAny<MailingMember>(), obj), 
                Times.Never);
        }

        #endregion
    }
}
