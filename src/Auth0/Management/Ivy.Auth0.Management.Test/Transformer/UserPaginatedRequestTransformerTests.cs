using Ivy.Auth0.Core.Models;
using Ivy.Auth0.Management.Core.Models.Responses;
using Ivy.Auth0.Management.Core.Transformers;
using Ivy.Auth0.Management.Test.Base;
using Ivy.Data.Common.Pagination;
using Ivy.IoC;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Ivy.Auth0.Management.Test.Transformers
{
    public class UserPaginatedRequestTransformerTests : 
        Auth0ManagementTestBase<IUserPaginatedRequestTransformer>
    {
        #region Tests

        #region Model -> Request

        [Fact]
        public void Model_To_Request_Maps_As_Expected()
        {
            var req = new PaginationRequest
            {
                PageCount = 123,
                PageNumber = 234,
                Search = "TEST"
            };

            var model = Sut.Transform(req);

            Assert.Equal(req.PageNumber, model.Page);
            Assert.Equal(req.PageCount, model.PerPage);
            Assert.Equal(req.Search, model.QueryString);
        }

        #endregion

        #region Response -> Model

        [Fact]
        public void Response_To_Model_Maps_As_Expected()
        {
            var user = new Auth0User();

            var resp = new Auth0ListUsersResponse
            {
                total = 123,
                users = new List<Auth0User> { user }
            };

            var result = Sut.Transform(resp);

            Assert.Equal(resp.total, result.TotalCount);
            Assert.Single(resp.users);
            Assert.Same(user, resp.users.First());
        }

        #endregion

        #endregion
    }
}
