using Ivy.Auth0.Core.Models.Requests;
using Ivy.Auth0.Management.Core.Models.Requests;
using Xunit;

namespace Ivy.Auth0.Test.Models
{
    public class Auth0ListUsersRequestTests
    {
        [Fact]
        public void DefaultConstructor_Sets_Up_As_Expected()
        {
            var entity = new Auth0ListUsersRequest();

            Assert.Equal(true, entity.IncludeTotals);
            Assert.Equal("email:1", entity.Sort);
            Assert.Equal(true, entity.IncludeFields);
            Assert.Equal("v2", entity.SearchEngine);
        }
    }
}
