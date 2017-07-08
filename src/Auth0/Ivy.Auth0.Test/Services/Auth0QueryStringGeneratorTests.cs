using Ivy.Auth0.Core.Models.Requests;
using Ivy.Auth0.Core.Services;
using Ivy.Auth0.Test.Base;
using Ivy.IoC;
using Microsoft.AspNetCore.WebUtilities;
using Xunit;

namespace Ivy.Auth0.Test.Services
{
    public class Auth0QueryStringGeneratorTests : Auth0TestBase
    {
        #region Variables & Constants

        private IAuth0QueryStringUriGenerator _sut;

        #endregion

        #region SetUp & TearDown

        public Auth0QueryStringGeneratorTests()
        {
            _sut = ServiceLocator.Instance.Resolve<IAuth0QueryStringUriGenerator>();
        }

        #endregion

        #region Tests

        #region Auth0QueryStringGenerator

        [Fact]
        public void Auth0QueryStringGenerator_Generates_Get_User_List_With_Skipped_Items()
        {
            var currentUri = "https://google.com";

            var req = new Auth0ListUsersRequest();
            req.Page = 10;
            req.IncludeTotals = true;

            req.PerPage = 0;
            req.Sort = null;
            req.Connection = null;
            req.Fields = null;
            req.IncludeFields = false;
            req.QueryString = null;
            req.SearchEngine = null;

            var result = _sut.GenerateGetUsersQueryString(currentUri, req);

            var resultQuery = QueryHelpers.ParseQuery(result.Query);

            Assert.Equal(2, resultQuery.Count);
            Assert.Equal(resultQuery["page"], req.Page.ToString());
            Assert.Equal(resultQuery["include_totals"], req.IncludeTotals.ToString());
        }

        [Fact]
        public void Auth0QueryStringGenerator_Generates_Get_User_List_With_Non_Skipped_Items()
        {
            var currentUri = "https://google.com";

            var req = new Auth0ListUsersRequest();
            req.Page = 0;
            req.IncludeTotals = false;

            req.PerPage = 25;
            req.Sort = "SORT";
            req.Connection = "CONNECTION";
            req.Fields = "FIELDS";
            req.IncludeFields = true;
            req.QueryString = "QUERYSTRING";
            req.SearchEngine = "SEARCHENGINE";

            var result = _sut.GenerateGetUsersQueryString(currentUri, req);

            var resultQuery = QueryHelpers.ParseQuery(result.Query);

            Assert.Equal(9, resultQuery.Count);
            Assert.Equal(resultQuery["page"], req.Page.ToString());
            Assert.Equal(resultQuery["include_totals"], req.IncludeTotals.ToString());

            Assert.Equal(resultQuery["per_page"], req.PerPage.ToString());
            Assert.Equal(resultQuery["sort"], req.Sort.ToString());
            Assert.Equal(resultQuery["connection"], req.Connection.ToString());
            Assert.Equal(resultQuery["fields"], req.Fields.ToString());
            Assert.Equal(resultQuery["include_fields"], req.IncludeFields.ToString());
            Assert.Equal(resultQuery["q"], req.QueryString.ToString());
            Assert.Equal(resultQuery["search_engine"], req.SearchEngine.ToString());
        }

        #endregion

        #endregion
    }
}