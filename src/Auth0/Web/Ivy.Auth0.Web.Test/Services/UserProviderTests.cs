using Ivy.Auth0.Web.Core.Services;
using Ivy.Auth0.Web.Test.Base;
using Ivy.IoC.Core;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Xunit;

namespace Ivy.Auth0.Web.Test.Services
{
    public class UserProviderTests : 
        Auth0WebTestBase<IUserProvider>
    {
        #region Variables & Constants

        private Mock<IHttpContextAccessor> _mockContext;

        #endregion

        #region SetUp & TearDown

        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            _mockContext = InitializeMoq<IHttpContextAccessor>(containerGen);
        }

        #endregion

        #region Tests

        //[Fact]
        //public void AuthenticationId_Fails_If_No_User_On_Context()
        //{
        //    var context = new DefaultHttpContext();
        //    context.User = null; // This is not nulling out the user

        //    _mockContext.Setup(x => x.HttpContext).Returns(context);

        //    var e = Assert.Throws<Exception>(() => Sut.AuthenticationId);

        //    Assert.Equal("No user has been authenticated in the provided context!", e.Message);
        //}

        [Fact]
        public void AuthenticationId_Fails_If_Value_Not_Found()
        {
            var context = new DefaultHttpContext();
            var identity = new ClaimsIdentity(new List<Claim> { });
            context.User = new ClaimsPrincipal(identity);

            _mockContext.Setup(x => x.HttpContext).Returns(context);

            var e = Assert.Throws<Exception>(() => Sut.AuthenticationId);

            var err = $"The provided claim key does not exist on the User! Claim Key: {ClaimTypes.NameIdentifier}";

            Assert.Equal(err, e.Message);
        }

        [Fact]
        public void AuthenticationId_Returns_As_Expected_ClaimType_From_Context_User()
        {
            const string val = "TEST";

            var context = new DefaultHttpContext();
            var claim = new Claim(ClaimTypes.NameIdentifier, val);
            var identity = new ClaimsIdentity(new List<Claim> { claim });
            context.User = new ClaimsPrincipal(identity);

            _mockContext.Setup(x => x.HttpContext).Returns(context);

            Assert.Equal(val, Sut.AuthenticationId);
        }

        #endregion
    }
}
