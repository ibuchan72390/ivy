using Ivy.Auth0.Web.Core.Services;
using Ivy.Auth0.Web.Test.Base;
using Ivy.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using Xunit;

namespace Ivy.Auth0.Web.Test.Services
{
    public class AuthTokenExtractorTests : Auth0WebTestBase
    {
        #region Variables & Constants

        private readonly IAuthTokenExtractor _sut;

        #endregion

        #region Constructor

        public AuthTokenExtractorTests()
        {
            _sut = ServiceLocator.Instance.Resolve<IAuthTokenExtractor>();
        }

        #endregion

        #region Tests

        [Fact]
        public void ExtractAuthToken_Returns_Null_If_No_Auth_Token()
        {
            var context = new DefaultHttpContext();

            Assert.Null(_sut.ExtractAuthToken(context.Request));
        }

        [Fact]
        public void ExtractAuthToken_Returns_Null_If_Auth_Split_Returns_More_Than_Two_Parts()
        {
            var context = new DefaultHttpContext();

            var headerVal = new StringValues("Bearer Test Meow");

            context.Request.Headers.Add(new KeyValuePair<string, StringValues>("Authorization", headerVal));

            Assert.Null(_sut.ExtractAuthToken(context.Request));
        }

        [Fact]
        public void ExtractAuthToken_Returns_Null_If_Schema_Isnt_Bearer()
        {
            var context = new DefaultHttpContext();

            var headerVal = new StringValues("Test Meow");

            context.Request.Headers.Add(new KeyValuePair<string, StringValues>("Authorization", headerVal));

            Assert.Null(_sut.ExtractAuthToken(context.Request));
        }

        [Fact]
        public void ExtractAuthToken_Returns_Bearer_Header_Value_If_Exists()
        {
            const string bearerVal = "meow";

            var context = new DefaultHttpContext();

            var headerVal = new StringValues($"Bearer {bearerVal}");

            context.Request.Headers.Add(new KeyValuePair<string, StringValues>("Authorization", headerVal));

            Assert.Equal(bearerVal, _sut.ExtractAuthToken(context.Request));
        }

        [Fact]
        public void ExtractAuthToken_Returns_Bearer_Query_Param_Value_If_Exists()
        {
            const string bearerVal = "meow";

            var context = new DefaultHttpContext();

            context.Request.QueryString = new QueryString($"?Authorization={bearerVal}");

            Assert.Equal(bearerVal, _sut.ExtractAuthToken(context.Request));
        }

        #endregion
    }
}
