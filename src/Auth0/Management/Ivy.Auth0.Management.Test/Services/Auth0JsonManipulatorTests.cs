using Ivy.Auth0.Core.Providers;
using Ivy.Auth0.Management.Core.Models.Requests;
using Ivy.Auth0.Management.Core.Providers;
using Ivy.Auth0.Management.Core.Services;
using Ivy.Auth0.Management.Test.Base;
using Ivy.IoC;
using Ivy.IoC.Core;
using Ivy.Web.Core.Json;
using Moq;
using System;
using Xunit;

namespace Ivy.Auth0.Management.Test.Services
{
    public class Auth0JsonManipulatorTests : Auth0ManagementTestBase
    {
        #region Variables & Constants

        private readonly IAuth0JsonManipulator _sut;
        private readonly IJsonSerializationService _serializationService;

        private readonly Mock<IAuth0ManagementConfigurationProvider> _mockConfig;

        #endregion

        #region SetUp & TearDown

        public Auth0JsonManipulatorTests()
        {
            _serializationService = ServiceLocator.Instance.GetService<IJsonSerializationService>();

            _mockConfig = new Mock<IAuth0ManagementConfigurationProvider>();

            var containerGen = ServiceLocator.Instance.GetService<IContainerGenerator>();

            base.ConfigureContainer(containerGen);

            containerGen.RegisterInstance<IAuth0ManagementConfigurationProvider>(_mockConfig.Object);

            _sut = containerGen.GenerateContainer().GetService<IAuth0JsonManipulator>();
        }

        #endregion

        #region Tests

        #region EditPhoneJson

        [Fact]
        public void EditPhoneJson_Removes_Attributes_If_Phone_Null()
        {
            var phoneReq = new Auth0CreateUserRequest();

            phoneReq.phone_number = null;

            var json = _serializationService.Serialize(phoneReq);

            Assert.Contains("phone_verified", json);
            Assert.Contains("phone_number", json);

            json = _sut.EditPhoneJson(json, phoneReq);

            Assert.DoesNotContain("phone_verified", json);
            Assert.DoesNotContain("phone_number", json);
        }

        [Fact]
        public void EditPhoneJson_Removes_Attributes_If_Phone_Empty()
        {
            var phoneReq = new Auth0CreateUserRequest();

            phoneReq.phone_number = "";

            var json = _serializationService.Serialize(phoneReq);

            Assert.Contains("phone_verified", json);
            Assert.Contains("phone_number", json);

            json = _sut.EditPhoneJson(json, phoneReq);

            Assert.DoesNotContain("phone_verified", json);
            Assert.DoesNotContain("phone_number", json);
        }

        [Fact]
        public void EditPhoneJson_Throws_Exception_If_Phone_Invalid()
        {
            var phoneReq = new Auth0CreateUserRequest();

            phoneReq.phone_number = "TEST";

            var json = _serializationService.Serialize(phoneReq);

            var e = Assert.Throws<Exception>(() => _sut.EditPhoneJson(json, phoneReq));

            var err = "Invalid phone number received! Must match regex - ^\\+[0-9]{1,15}$" +
                      $" / Phone: {phoneReq.phone_number}";

            Assert.Equal(err, e.Message);
        }

        [Fact]
        public void EditPhoneJson_Does_Nothing_If_Phone_Valid()
        {
            var phoneReq = new Auth0CreateUserRequest();

            phoneReq.phone_number = "+199999999999999";

            var json = _serializationService.Serialize(phoneReq);

            Assert.Contains("phone_verified", json);
            Assert.Contains("phone_number", json);

            json = _sut.EditPhoneJson(json, phoneReq);

            Assert.Contains("phone_verified", json);
            Assert.Contains("phone_number", json);
        }

        #endregion

        #region EditUsernameJson

        [Fact]
        public void EditUsernameJson_Removes_Attributes_If_UseUsername_False()
        {
            var model = new Auth0CreateUserRequest();

            _mockConfig.Setup(x => x.UseUsername).Returns(false);

            model.username = "TESTING";

            var json = _serializationService.Serialize(model);

            Assert.Contains("username", json);

            json = _sut.EditUsernameJson(json, model);

            Assert.DoesNotContain("username", json);
        }

        [Fact]
        public void EditUsernameJson_Removes_Attributes_If_UseUsername_True_And_Username_Null()
        {
            var model = new Auth0CreateUserRequest();

            _mockConfig.Setup(x => x.UseUsername).Returns(true);

            model.username = null;

            var json = _serializationService.Serialize(model);

            Assert.Contains("username", json);

            json = _sut.EditUsernameJson(json, model);

            Assert.DoesNotContain("username", json);
        }

        [Fact]
        public void EditUsernameJson_Removes_Attributes_If_UseUsername_True_And_Username_Empty()
        {
            var model = new Auth0CreateUserRequest();

            _mockConfig.Setup(x => x.UseUsername).Returns(true);

            model.username = "";

            var json = _serializationService.Serialize(model);

            Assert.Contains("username", json);

            json = _sut.EditUsernameJson(json, model);

            Assert.DoesNotContain("username", json);
        }

        [Fact]
        public void EditUsernameJson_Doesnt_Edit_If_UseUsername_True_And_Username_Populated()
        {
            var model = new Auth0CreateUserRequest();

            _mockConfig.Setup(x => x.UseUsername).Returns(true);

            model.username = "TESTING";

            var json = _serializationService.Serialize(model);

            Assert.Contains("username", json);

            json = _sut.EditUsernameJson(json, model);

            Assert.Contains("username", json);
        }

        #endregion

        #endregion
    }
}
