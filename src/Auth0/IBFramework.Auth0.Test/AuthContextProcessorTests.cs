using IBFramework.Auth0.Core.Services;
using IBFramework.IoC;
using IBFramework.IoC.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Microsoft.AspNetCore.Http;
using IBFramework.Auth0.Core.Models;
using Microsoft.AspNetCore.Http.Internal;
using IBFramework.Auth0.Test.Base;

namespace IBFramework.Auth0.Test
{
    public class AuthContextProcessorTests : Auth0TestBase
    {
        #region Variables & Constants

        private readonly IAuth0ContextProcessor _sut;

        private Mock<IJwtProcessor> _jwtProcessorMock;
        private Mock<ILogger<IAuth0ContextProcessor>> _loggerMock;

        #endregion

        #region SetUp & TearDown

        public AuthContextProcessorTests()
        {
            var containerGen = ServiceLocator.Instance.Resolve<IContainerGenerator>();

            //base.ConfigureContainer(containerGen);

            _jwtProcessorMock = new Mock<IJwtProcessor>();
            containerGen.RegisterInstance<IJwtProcessor>(_jwtProcessorMock.Object);

            /*
             * Don't set it up, mocking this guy is super weird
             */
            _loggerMock = new Mock<ILogger<IAuth0ContextProcessor>>();
            containerGen.RegisterInstance<ILogger<IAuth0ContextProcessor>>(_loggerMock.Object);

            var testContainer = containerGen.GenerateContainer();

            _sut = testContainer.Resolve<IAuth0ContextProcessor>();
        }

        #endregion

        #region Tests

        [Fact]
        public async void Auth0ContextProcessor_Does_Nothing_If_No_Auth_Header()
        {
            var context = new DefaultHttpContext();

            Assert.False(context.Request.Headers.ContainsKey("Authorization"));

            await _sut.ProcessContextAsync(context);

            Assert.Equal(1, context.User.Identities.Count());
        }

        [Fact]
        public async void Auth0ContextProcessor_Does_Nothing_If_Auth_Header_Length_Not_Equal_To_2()
        {
            var context = new DefaultHttpContext();

            context.Request.Headers.Add(new KeyValuePair<string, StringValues>(
                "Authorization", new StringValues("Bearer TEST RAWR")
            ));

            await _sut.ProcessContextAsync(context);

            Assert.Equal(1, context.User.Identities.Count());
        }

        [Fact]
        public async void Auth0ContextProcessor_Does_Nothing_If_Schema_Type_Not_Bearer()
        {
            var context = new DefaultHttpContext();

            context.Request.Headers.Add(new KeyValuePair<string, StringValues>(
                "Authorization", new StringValues("TEST TEST")
            ));

            await _sut.ProcessContextAsync(context);

            Assert.Equal(1, context.User.Identities.Count());
        }

        [Fact]
        public async void Auth0ContextProcessor_Throws_Exception_If_Standard_Exception()
        {
            var context = new DefaultHttpContext();

            const string bearer = "TEST";

            context.Request.Headers.Add(new KeyValuePair<string, StringValues>(
                "Authorization", new StringValues($"Bearer {bearer}")
            ));

            _jwtProcessorMock.Setup(x => x.DecodeClaimsPrincipalAsync(bearer))
                .Throws(new FormatException());

            await _sut.ProcessContextAsync(context);

            Assert.Equal(1, context.User.Identities.Count());

            _jwtProcessorMock.Verify(x => x.DecodeClaimsPrincipalAsync(bearer), Times.Once);
        }

        [Fact]
        public async void Auth0ContextProcessor_Assigns_User_From_Authorization_Header_If_Possible()
        {
            var testToken = new JsonWebToken
            {
                Issuer = "Issuer",
                Subject = "Subject",
                Audience = "Audience",
                Expiry = 123456,
                IssuedAt = 456789
            };

            var resultDecode = Newtonsoft.Json.JsonConvert.SerializeObject(testToken);

            var context = new DefaultHttpContext();

            const string bearer = "TEST";

            context.Request.Headers.Add(new KeyValuePair<string, StringValues>(
                "Authorization", new StringValues($"Bearer {bearer}")
            ));

            _jwtProcessorMock.Setup(x => x.DecodeClaimsPrincipalAsync(bearer))
                .ReturnsAsync(new System.Security.Claims.ClaimsPrincipal());

            await _sut.ProcessContextAsync(context);

            Assert.NotNull(context.User);

            /*
             * Probably want to validate the aspects of the assigned user
             * Not exactly sure how to do that yet, figure out as you go.
             */

            _jwtProcessorMock.Verify(x => x.DecodeClaimsPrincipalAsync(bearer), Times.Once);
        }


        [Fact]
        public async void Auth0ContextProcessor_Assigns_User_From_Authorization_Query_Param_If_Possible()
        {
            var testToken = new JsonWebToken
            {
                Issuer = "Issuer",
                Subject = "Subject",
                Audience = "Audience",
                Expiry = 123456,
                IssuedAt = 456789
            };

            var resultDecode = Newtonsoft.Json.JsonConvert.SerializeObject(testToken);

            var context = new DefaultHttpContext();

            const string bearer = "TEST";

            context.Request.Query = new QueryCollection(new Dictionary<string, StringValues>
            {
                { "Authorization", new StringValues(bearer) }
            });

            _jwtProcessorMock.Setup(x => x.DecodeClaimsPrincipalAsync(bearer))
                .ReturnsAsync(new System.Security.Claims.ClaimsPrincipal());

            await _sut.ProcessContextAsync(context);

            Assert.NotNull(context.User);

            /*
             * Probably want to validate the aspects of the assigned user
             * Not exactly sure how to do that yet, figure out as you go.
             */

            _jwtProcessorMock.Verify(x => x.DecodeClaimsPrincipalAsync(bearer), Times.Once);
        }

        #endregion
    }
}
