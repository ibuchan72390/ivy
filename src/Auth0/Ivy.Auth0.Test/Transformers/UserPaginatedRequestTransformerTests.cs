using Ivy.Auth0.Core.Transformers;
using Ivy.Auth0.Test.Base;
using Ivy.Data.Common.Pagination;
using Ivy.IoC;
using Xunit;

namespace Ivy.Auth0.Test.Transformers
{
    public class UserPaginatedRequestTransformerTests : Auth0TestBase
    {
        #region Variables & Constants

        private IUserPaginatedRequestTransformer _sut;

        #endregion

        #region SetUp & TearDown

        public UserPaginatedRequestTransformerTests()
        {
            _sut = ServiceLocator.Instance.Resolve<IUserPaginatedRequestTransformer>();
        }

        #endregion

        #region Tests

        #region Transform
        
        [Fact]
        public void Transform_Maps_As_Expected()
        {
            var req = new PaginationRequest
            {
                PageCount = 123,
                PageNumber = 234,
                Search = "TEST"
            };

            var model = _sut.Transform(req);

            Assert.Equal(req.PageNumber, model.Page);
            Assert.Equal(req.PageCount, model.PerPage);
            Assert.Equal(req.Search, model.QueryString);
        }
        
        #endregion

        #endregion
    }
}
