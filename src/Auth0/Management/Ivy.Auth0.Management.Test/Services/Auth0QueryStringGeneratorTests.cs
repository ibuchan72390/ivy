using Ivy.Auth0.Enums;
using Ivy.Auth0.Management.Core.Models.Requests;
using Ivy.Auth0.Management.Core.Services;
using Ivy.Auth0.Management.Test.Base;
using Ivy.IoC;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using Xunit;
using Microsoft.AspNetCore.WebUtilities;

namespace Ivy.Auth0.Management.Test.Services
{
    public class Auth0QueryStringGeneratorTests : Auth0ManagementTestBase
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
            req.Page = -10;
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
            AssertDictEquality(resultQuery, "page", 0);
            AssertDictEquality(resultQuery, "include_totals", req.IncludeTotals);
        }

        [Fact]
        public void Auth0QueryStringGenerator_Generates_Get_User_List_With_v1_And_Non_Skipped_Items()
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
            req.SearchEngine = Auth0ApiVersionNames.v1.ToString();

            var result = _sut.GenerateGetUsersQueryString(currentUri, req);

            var resultQuery = QueryHelpers.ParseQuery(result.Query);

            Assert.Equal(9, resultQuery.Count);
            AssertDictEquality(resultQuery, "page", 0);
            AssertDictEquality(resultQuery, "include_totals", req.IncludeTotals);

            AssertDictEquality(resultQuery, "per_page", req.PerPage);
            AssertDictEquality(resultQuery, "sort", req.Sort);
            AssertDictEquality(resultQuery, "connection", req.Connection);
            AssertDictEquality(resultQuery, "fields", req.Fields);
            AssertDictEquality(resultQuery, "include_fields", req.IncludeFields);
            AssertDictEquality(resultQuery, "q", req.QueryString);
            AssertDictEquality(resultQuery, "search_engine", req.SearchEngine);
        }


        [Fact]
        public void Auth0QueryStringGenerator_Generates_Get_User_List_With_v2_And_Query_String()
        {
            var currentUri = "https://google.com";

            var req = new Auth0ListUsersRequest();
            req.Page = 1;
            req.IncludeTotals = false;

            req.PerPage = 25;
            req.Sort = "SORT";
            req.Connection = "CONNECTION";
            req.Fields = "FIELDS";
            req.IncludeFields = true;
            req.QueryString = "QUERYSTRING";
            req.SearchEngine = Auth0ApiVersionNames.v2.ToString();

            var result = _sut.GenerateGetUsersQueryString(currentUri, req);

            var resultQuery = QueryHelpers.ParseQuery(result.Query);

            Assert.Equal(8, resultQuery.Count);
            AssertDictEquality(resultQuery, "page", 0);
            AssertDictEquality(resultQuery, "include_totals", req.IncludeTotals);

            AssertDictEquality(resultQuery, "per_page", req.PerPage);
            AssertDictEquality(resultQuery, "sort", req.Sort);
            AssertDictEquality(resultQuery, "fields", req.Fields);
            AssertDictEquality(resultQuery, "include_fields", req.IncludeFields);
            AssertDictEquality(resultQuery, "search_engine", req.SearchEngine);

            Assert.Equal(resultQuery["q"], $"{req.QueryString.ToLower()} AND identities.connection:\"{req.Connection.ToLower()}\"");
        }

        [Fact]
        public void Auth0QueryStringGenerator_Generates_Get_User_List_With_v2_And_No_Query_String()
        {
            var currentUri = "https://google.com";

            var req = new Auth0ListUsersRequest();
            req.Page = 5;
            req.IncludeTotals = false;

            req.PerPage = 25;
            req.Sort = "SORT";
            req.Connection = "CONNECTION";
            req.Fields = "FIELDS";
            req.IncludeFields = true;
            req.SearchEngine = Auth0ApiVersionNames.v2.ToString();

            var result = _sut.GenerateGetUsersQueryString(currentUri, req);

            var resultQuery = QueryHelpers.ParseQuery(result.Query);

            Assert.Equal(8, resultQuery.Count);
            AssertDictEquality(resultQuery, "page", req.Page - 1);
            AssertDictEquality(resultQuery, "include_totals", req.IncludeTotals);

            AssertDictEquality(resultQuery, "per_page", req.PerPage);
            AssertDictEquality(resultQuery, "sort", req.Sort);
            AssertDictEquality(resultQuery, "fields", req.Fields);
            AssertDictEquality(resultQuery, "include_fields", req.IncludeFields);
            AssertDictEquality(resultQuery, "search_engine", req.SearchEngine);

            Assert.Equal(resultQuery["q"], $"identities.connection:\"{req.Connection.ToLower()}\"");
        }

        [Fact]
        public void Auth0QueryStringGenerator_Generates_Get_User_List_With_v2_And_Empty_Query_String()
        {
            var currentUri = "https://google.com";

            var req = new Auth0ListUsersRequest();
            req.Page = 10;
            req.IncludeTotals = false;

            req.PerPage = 25;
            req.Sort = "SORT";
            req.Connection = "CONNECTION";
            req.Fields = "FIELDS";
            req.IncludeFields = true;
            req.QueryString = "";
            req.SearchEngine = Auth0ApiVersionNames.v2.ToString();

            var result = _sut.GenerateGetUsersQueryString(currentUri, req);

            var resultQuery = QueryHelpers.ParseQuery(result.Query);

            Assert.Equal(8, resultQuery.Count);
            AssertDictEquality(resultQuery, "page", req.Page - 1);
            AssertDictEquality(resultQuery, "include_totals", req.IncludeTotals);

            AssertDictEquality(resultQuery, "per_page", req.PerPage);
            AssertDictEquality(resultQuery, "sort", req.Sort);
            AssertDictEquality(resultQuery, "fields", req.Fields);
            AssertDictEquality(resultQuery, "include_fields", req.IncludeFields);
            AssertDictEquality(resultQuery, "search_engine", req.SearchEngine);

            Assert.Equal(resultQuery["q"], $"identities.connection:\"{req.Connection.ToLower()}\"");
        }

        #endregion

        #endregion

        #region Helper Methods

        private void AssertDictEquality<T>(Dictionary<string, StringValues> dict, string key, T item)
        {
            Assert.Equal(dict[key], item.ToString().ToLower());
        }

        #endregion
    }
}