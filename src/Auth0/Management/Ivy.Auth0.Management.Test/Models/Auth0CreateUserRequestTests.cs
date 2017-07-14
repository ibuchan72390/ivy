using Ivy.Auth0.Management.Core.Models.Requests;
using Xunit;

namespace Ivy.Auth0.Test.Models
{
    public class Auth0CreateUserRequestTests
    {
        [Fact]
        public void DefaultConstructor_Sets_Up_As_Expected()
        {
            var entity = new Auth0CreateUserRequest();

            Assert.Equal(true, entity.verify_email);
        }
    }
}
