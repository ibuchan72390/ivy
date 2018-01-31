using Ivy.Mailing.ActiveCampaign.Core.Interfaces.Transformers;
using Ivy.Mailing.ActiveCampaign.Core.Models;
using Ivy.Mailing.ActiveCampaign.Test.Base;
using Ivy.IoC;
using Ivy.Mailing.Core.Enums;
using Moq;
using System;
using Xunit;
using Ivy.Mailing.Core.Models;

namespace Ivy.Mailing.ActiveCampaign.Test.Transformers
{
    public class ActiveCampaignContactTransformerTests : BaseActiveCampaignTest
    {
        #region Variables & Constants

        private readonly IActiveCampaignContactTransformer _sut;

        private readonly Mock<IActiveCampaignExtraDataTransformer> _mockExtraDataTransformer;

        #endregion

        #region SetUp & TearDown

        public ActiveCampaignContactTransformerTests()
        {
            var containerGen = new ContainerGenerator();

            base.ConfigureContainer(containerGen);

            _mockExtraDataTransformer = new Mock<IActiveCampaignExtraDataTransformer>();

            containerGen.RegisterInstance<IActiveCampaignExtraDataTransformer>(_mockExtraDataTransformer.Object);

            var container = containerGen.GenerateContainer();

            _sut = container.GetService<IActiveCampaignContactTransformer>();
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
            var result = _sut.Transform(obj);

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
            var result = _sut.Transform(obj);

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
            var e = Assert.Throws<Exception>(() => _sut.Transform(obj));

            // Assert
            var err = $"Unknown ActiveCampaignContact.status received! status: {obj.status}";
            Assert.Equal(err, e.Message);

            _mockExtraDataTransformer.Verify(x => x.Transform(It.IsAny<MailingMember>(), obj), 
                Times.Never);
        }

        #endregion
    }
}
