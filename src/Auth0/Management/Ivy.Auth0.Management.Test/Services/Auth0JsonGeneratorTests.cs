using Ivy.Auth0.Management.Core.Models.Requests;
using Ivy.Auth0.Management.Core.Services;
using Ivy.Auth0.Management.Test.Base;
using Ivy.IoC;
using Ivy.IoC.Core;
using Ivy.Web.Core.Json;
using Moq;
using Xunit;

namespace Ivy.Auth0.Management.Test.Services
{
    public class Auth0JsonGeneratorTests : 
        Auth0ManagementTestBase<IAuth0JsonGenerator>
    {
        #region Variables & Constants

        private readonly IJsonSerializationService _serializationService;

        private Mock<IAuth0JsonManipulator> _mockJsonManipulator;

        #endregion

        #region SetUp & TearDown

        public Auth0JsonGeneratorTests()
        {
            _serializationService = ServiceLocator.Instance.GetService<IJsonSerializationService>();
        }

        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            _mockJsonManipulator = InitializeMoq<IAuth0JsonManipulator>(containerGen);
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

            var result = Sut.ConfigureCreateUserJson(req);

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

            Assert.Contains("user_id", json);

            _mockJsonManipulator.Setup(x => x.EditPhoneJson(json, req)).Returns(json);
            _mockJsonManipulator.Setup(x => x.EditUsernameJson(json, req)).Returns(json);

            var result = Sut.ConfigureUpdateUserJson(req);

            _mockJsonManipulator.Verify(x => x.EditPhoneJson(It.IsAny<string>(), req), Times.Once);
            _mockJsonManipulator.Verify(x => x.EditUsernameJson(It.IsAny<string>(), req), Times.Once);

            Assert.DoesNotContain("user_id", result);
        }

        [Fact]
        public void ConfigureUpdateUserJson_Makes_Extra_Phone_Edits_As_Expected_If_Phone_Null()
        {
            var req = new Auth0UpdateUserRequest { phone_number = null };

            var json = _serializationService.Serialize(req);

            _mockJsonManipulator.Setup(x => x.EditPhoneJson(json, req)).Returns(json);
            _mockJsonManipulator.Setup(x => x.EditUsernameJson(json, req)).Returns(json);

            var result = Sut.ConfigureUpdateUserJson(req);

            Assert.DoesNotContain("verify_phone_number", result);
        }

        [Fact]
        public void ConfigureUpdateUserJson_Makes_Extra_Phone_Edits_As_Expected_If_Phone_Empty()
        {
            var req = new Auth0UpdateUserRequest { phone_number = "" };

            var json = _serializationService.Serialize(req);

            _mockJsonManipulator.Setup(x => x.EditPhoneJson(json, req)).Returns(json);
            _mockJsonManipulator.Setup(x => x.EditUsernameJson(json, req)).Returns(json);

            var result = Sut.ConfigureUpdateUserJson(req);

            Assert.DoesNotContain("verify_phone_number", result);
        }

        [Fact]
        public void ConfigureUpdateUserJson_Skips_Phone_Edits_As_Expected_If_Phone_Populated()
        {
            var req = new Auth0UpdateUserRequest { phone_number = "TESTING" };

            var json = _serializationService.Serialize(req);

            _mockJsonManipulator.Setup(x => x.EditPhoneJson(json, req)).Returns(json);
            _mockJsonManipulator.Setup(x => x.EditUsernameJson(json, req)).Returns(json);

            var result = Sut.ConfigureUpdateUserJson(req);

            Assert.Contains("verify_phone_number", result);
        }

        [Fact]
        public void ConfigureUpdateUserJson_Makes_Extra_Email_Edits_As_Expected_If_Email_Null()
        {
            var req = new Auth0UpdateUserRequest { email = null };

            var json = _serializationService.Serialize(req);

            _mockJsonManipulator.Setup(x => x.EditPhoneJson(json, req)).Returns(json);
            _mockJsonManipulator.Setup(x => x.EditUsernameJson(json, req)).Returns(json);

            var result = Sut.ConfigureUpdateUserJson(req);

            Assert.DoesNotContain("email", result);
            Assert.DoesNotContain("email_verified", result);
            Assert.DoesNotContain("verify_email", result);
        }

        [Fact]
        public void ConfigureUpdateUserJson_Makes_Extra_Email_Edits_As_Expected_If_Email_Empty()
        {
            var req = new Auth0UpdateUserRequest { email = "" };

            var json = _serializationService.Serialize(req);

            _mockJsonManipulator.Setup(x => x.EditPhoneJson(json, req)).Returns(json);
            _mockJsonManipulator.Setup(x => x.EditUsernameJson(json, req)).Returns(json);

            var result = Sut.ConfigureUpdateUserJson(req);

            Assert.DoesNotContain("email", result);
            Assert.DoesNotContain("email_verified", result);
            Assert.DoesNotContain("verify_email", result);
        }

        [Fact]
        public void ConfigureUpdateUserJson_Skips_Email_Edits_As_Expected_If_Email_Populated()
        {
            var req = new Auth0UpdateUserRequest { email = "TESTING" };

            var json = _serializationService.Serialize(req);

            _mockJsonManipulator.Setup(x => x.EditPhoneJson(json, req)).Returns(json);
            _mockJsonManipulator.Setup(x => x.EditUsernameJson(json, req)).Returns(json);

            var result = Sut.ConfigureUpdateUserJson(req);

            Assert.Contains("email", result);
            Assert.Contains("email_verified", result);
            Assert.Contains("verify_email", result);
        }

        [Fact]
        public void ConfigureUpdateUserJson_Makes_Extra_Password_Edits_As_Expected_If_Password_Null()
        {
            var req = new Auth0UpdateUserRequest { password = null };

            var json = _serializationService.Serialize(req);

            _mockJsonManipulator.Setup(x => x.EditPhoneJson(json, req)).Returns(json);
            _mockJsonManipulator.Setup(x => x.EditUsernameJson(json, req)).Returns(json);

            var result = Sut.ConfigureUpdateUserJson(req);

            Assert.DoesNotContain("password", result);
            Assert.DoesNotContain("verify_password", result);
        }

        [Fact]
        public void ConfigureUpdateUserJson_Makes_Extra_Password_Edits_As_Expected_If_Password_Empty()
        {
            var req = new Auth0UpdateUserRequest { password = "" };

            var json = _serializationService.Serialize(req);

            _mockJsonManipulator.Setup(x => x.EditPhoneJson(json, req)).Returns(json);
            _mockJsonManipulator.Setup(x => x.EditUsernameJson(json, req)).Returns(json);

            var result = Sut.ConfigureUpdateUserJson(req);

            Assert.DoesNotContain("password", result);
            Assert.DoesNotContain("verify_password", result);
        }

        [Fact]
        public void ConfigureUpdateUserJson_Skips_Password_Edits_As_Expected_If_Password_Populated()
        {
            var req = new Auth0UpdateUserRequest { password = "TESTING" };

            var json = _serializationService.Serialize(req);

            _mockJsonManipulator.Setup(x => x.EditPhoneJson(json, req)).Returns(json);
            _mockJsonManipulator.Setup(x => x.EditUsernameJson(json, req)).Returns(json);

            var result = Sut.ConfigureUpdateUserJson(req);

            Assert.Contains("password", result);
            Assert.Contains("verify_password", result);
        }

        #endregion

        #endregion

    }
}
