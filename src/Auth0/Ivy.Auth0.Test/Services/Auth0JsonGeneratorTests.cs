using Ivy.Auth0.Core.Models.Requests;
using Ivy.Auth0.Core.Services;
using Ivy.Auth0.Test.Base;
using Ivy.IoC;
using Ivy.IoC.Core;
using Ivy.Web.Json;
using Moq;
using Xunit;

namespace Ivy.Auth0.Test.Services
{
    public class Auth0JsonGeneratorTests : Auth0TestBase
    {
        #region Variables & Constants

        private readonly IAuth0JsonGenerator _sut;
        private readonly IJsonSerializationService _serializationService;

        private readonly Mock<IAuth0JsonManipulator> _mockJsonManipulator;

        #endregion

        #region SetUp & TearDown

        public Auth0JsonGeneratorTests()
        {
            _serializationService = ServiceLocator.Instance.Resolve<IJsonSerializationService>();

            _mockJsonManipulator = new Mock<IAuth0JsonManipulator>();

            var containerGen = ServiceLocator.Instance.Resolve<IContainerGenerator>();

            base.ConfigureContainer(containerGen);

            containerGen.RegisterInstance<IAuth0JsonManipulator>(_mockJsonManipulator.Object);

            _sut = containerGen.GenerateContainer().Resolve<IAuth0JsonGenerator>();
        }

        #endregion

        #region Tests

        #region ConfigureCreateUserJson

        [Fact]
        public void ConfigureCreateUserJson_Edits_Username_And_Phone_Appropriately()
        {
            var req = new Auth0CreateUserRequest();

            var json = _serializationService.Serialize(req);

            _mockJsonManipulator.Setup(x => x.EditPhoneJson(json, req)).Returns(json);
            _mockJsonManipulator.Setup(x => x.EditUsernameJson(json, req)).Returns(json);

            var result = _sut.ConfigureCreateUserJson(req);

            _mockJsonManipulator.Verify(x => x.EditPhoneJson(json, req), Times.Once);
            _mockJsonManipulator.Verify(x => x.EditUsernameJson(json, req), Times.Once);
        }

        #endregion

        #region ConfigureUpdateUserJson

        [Fact]
        public void ConfigureUpdateUserJson_Edits_Username_And_Phone_Appropriately()
        {
            var req = new Auth0UpdateUserRequest();

            var json = _serializationService.Serialize(req);

            Assert.True(json.Contains("user_id"));

            _mockJsonManipulator.Setup(x => x.EditPhoneJson(json, req)).Returns(json);
            _mockJsonManipulator.Setup(x => x.EditUsernameJson(json, req)).Returns(json);

            var result = _sut.ConfigureUpdateUserJson(req);

            _mockJsonManipulator.Verify(x => x.EditPhoneJson(It.IsAny<string>(), req), Times.Once);
            _mockJsonManipulator.Verify(x => x.EditUsernameJson(It.IsAny<string>(), req), Times.Once);

            Assert.False(result.Contains("user_id"));
        }

        [Fact]
        public void ConfigureUpdateUserJson_Makes_Extra_Phone_Edits_As_Expected_If_Phone_Null()
        {
            var req = new Auth0UpdateUserRequest { phone_number = null };

            var json = _serializationService.Serialize(req);

            _mockJsonManipulator.Setup(x => x.EditPhoneJson(json, req)).Returns(json);
            _mockJsonManipulator.Setup(x => x.EditUsernameJson(json, req)).Returns(json);

            var result = _sut.ConfigureUpdateUserJson(req);

            Assert.False(result.Contains("verify_phone_number"));
        }

        [Fact]
        public void ConfigureUpdateUserJson_Makes_Extra_Phone_Edits_As_Expected_If_Phone_Empty()
        {
            var req = new Auth0UpdateUserRequest { phone_number = "" };

            var json = _serializationService.Serialize(req);

            _mockJsonManipulator.Setup(x => x.EditPhoneJson(json, req)).Returns(json);
            _mockJsonManipulator.Setup(x => x.EditUsernameJson(json, req)).Returns(json);

            var result = _sut.ConfigureUpdateUserJson(req);

            Assert.False(result.Contains("verify_phone_number"));
        }

        [Fact]
        public void ConfigureUpdateUserJson_Skips_Phone_Edits_As_Expected_If_Phone_Populated()
        {
            var req = new Auth0UpdateUserRequest { phone_number = "TESTING" };

            var json = _serializationService.Serialize(req);

            _mockJsonManipulator.Setup(x => x.EditPhoneJson(json, req)).Returns(json);
            _mockJsonManipulator.Setup(x => x.EditUsernameJson(json, req)).Returns(json);

            var result = _sut.ConfigureUpdateUserJson(req);

            Assert.True(result.Contains("verify_phone_number"));
        }

        [Fact]
        public void ConfigureUpdateUserJson_Makes_Extra_Email_Edits_As_Expected_If_Email_Null()
        {
            var req = new Auth0UpdateUserRequest { email = null };

            var json = _serializationService.Serialize(req);

            _mockJsonManipulator.Setup(x => x.EditPhoneJson(json, req)).Returns(json);
            _mockJsonManipulator.Setup(x => x.EditUsernameJson(json, req)).Returns(json);

            var result = _sut.ConfigureUpdateUserJson(req);

            Assert.False(result.Contains("email"));
            Assert.False(result.Contains("email_verified"));
            Assert.False(result.Contains("verify_email"));
        }

        [Fact]
        public void ConfigureUpdateUserJson_Makes_Extra_Email_Edits_As_Expected_If_Email_Empty()
        {
            var req = new Auth0UpdateUserRequest { email = "" };

            var json = _serializationService.Serialize(req);

            _mockJsonManipulator.Setup(x => x.EditPhoneJson(json, req)).Returns(json);
            _mockJsonManipulator.Setup(x => x.EditUsernameJson(json, req)).Returns(json);

            var result = _sut.ConfigureUpdateUserJson(req);

            Assert.False(result.Contains("email"));
            Assert.False(result.Contains("email_verified"));
            Assert.False(result.Contains("verify_email"));
        }

        [Fact]
        public void ConfigureUpdateUserJson_Skips_Email_Edits_As_Expected_If_Email_Populated()
        {
            var req = new Auth0UpdateUserRequest { email = "TESTING" };

            var json = _serializationService.Serialize(req);

            _mockJsonManipulator.Setup(x => x.EditPhoneJson(json, req)).Returns(json);
            _mockJsonManipulator.Setup(x => x.EditUsernameJson(json, req)).Returns(json);

            var result = _sut.ConfigureUpdateUserJson(req);

            Assert.True(result.Contains("email"));
            Assert.True(result.Contains("email_verified"));
            Assert.True(result.Contains("verify_email"));
        }

        [Fact]
        public void ConfigureUpdateUserJson_Makes_Extra_Password_Edits_As_Expected_If_Password_Null()
        {
            var req = new Auth0UpdateUserRequest { password = null };

            var json = _serializationService.Serialize(req);

            _mockJsonManipulator.Setup(x => x.EditPhoneJson(json, req)).Returns(json);
            _mockJsonManipulator.Setup(x => x.EditUsernameJson(json, req)).Returns(json);

            var result = _sut.ConfigureUpdateUserJson(req);

            Assert.False(result.Contains("password"));
            Assert.False(result.Contains("verify_password"));
        }

        [Fact]
        public void ConfigureUpdateUserJson_Makes_Extra_Password_Edits_As_Expected_If_Password_Empty()
        {
            var req = new Auth0UpdateUserRequest { password = "" };

            var json = _serializationService.Serialize(req);

            _mockJsonManipulator.Setup(x => x.EditPhoneJson(json, req)).Returns(json);
            _mockJsonManipulator.Setup(x => x.EditUsernameJson(json, req)).Returns(json);

            var result = _sut.ConfigureUpdateUserJson(req);

            Assert.False(result.Contains("password"));
            Assert.False(result.Contains("verify_password"));
        }

        [Fact]
        public void ConfigureUpdateUserJson_Skips_Password_Edits_As_Expected_If_Password_Populated()
        {
            var req = new Auth0UpdateUserRequest { password = "TESTING" };

            var json = _serializationService.Serialize(req);

            _mockJsonManipulator.Setup(x => x.EditPhoneJson(json, req)).Returns(json);
            _mockJsonManipulator.Setup(x => x.EditUsernameJson(json, req)).Returns(json);

            var result = _sut.ConfigureUpdateUserJson(req);

            Assert.True(result.Contains("password"));
            Assert.True(result.Contains("verify_password"));
        }

        #endregion

        #endregion

    }
}
