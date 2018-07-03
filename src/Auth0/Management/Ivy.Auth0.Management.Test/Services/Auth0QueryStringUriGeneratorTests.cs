using Ivy.Auth0.Enums;
using Ivy.Auth0.Management.Core.Models.Requests;
using Ivy.Auth0.Management.Core.Services;
using Ivy.Auth0.Management.Test.Base;
using Ivy.IoC;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using Xunit;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;

namespace Ivy.Auth0.Management.Test.Services
{
    public class Auth0QueryStringUriGeneratorTests : 
        Auth0ManagementTestBase<IAuth0QueryStringUriGenerator>
    {
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

            var result = Sut.GenerateGetUsersQueryString(currentUri, req);

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

            var result = Sut.GenerateGetUsersQueryString(currentUri, req);

            var resultQuery = QueryHelpers.ParseQuery(result.Query);

            Assert.Equal(9, resultQuery.Count);
            AssertDictEquality(resultQuery, "page", 0);
            AssertDictEquality(resultQuery, "include_totals", req.IncludeTotals);

            AssertDictEquality(resultQuery, "per_page", req.PerPage);
            AssertDictEquality(resultQuery, "sort", req.Sort);
            AssertDictEquality(resultQuery, "connection", req.Connection);
            AssertDictEquality(resultQuery, "fields", req.Fields);
            AssertDictEquality(resultQuery, "include_fields", req.IncludeFields);
            AssertDictEquality(resultQuery, "q", req.QueryString, false);
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

            var result = Sut.GenerateGetUsersQueryString(currentUri, req);

            var resultQuery = QueryHelpers.ParseQuery(result.Query);

            Assert.Equal(8, resultQuery.Count);
            AssertDictEquality(resultQuery, "page", 0);
            AssertDictEquality(resultQuery, "include_totals", req.IncludeTotals);

            AssertDictEquality(resultQuery, "per_page", req.PerPage);
            AssertDictEquality(resultQuery, "sort", req.Sort);
            AssertDictEquality(resultQuery, "fields", req.Fields);
            AssertDictEquality(resultQuery, "include_fields", req.IncludeFields);
            AssertDictEquality(resultQuery, "search_engine", req.SearchEngine);

            var expected = $"({req.QueryString}) AND identities.connection:\"{req.Connection}\"";
            AssertDictEquality(resultQuery, "q", expected, false);
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

            var result = Sut.GenerateGetUsersQueryString(currentUri, req);

            var resultQuery = QueryHelpers.ParseQuery(result.Query);

            Assert.Equal(8, resultQuery.Count);
            AssertDictEquality(resultQuery, "page", req.Page - 1);
            AssertDictEquality(resultQuery, "include_totals", req.IncludeTotals);

            AssertDictEquality(resultQuery, "per_page", req.PerPage);
            AssertDictEquality(resultQuery, "sort", req.Sort);
            AssertDictEquality(resultQuery, "fields", req.Fields);
            AssertDictEquality(resultQuery, "include_fields", req.IncludeFields);
            AssertDictEquality(resultQuery, "search_engine", req.SearchEngine);

            var expected = $"identities.connection:\"{req.Connection}\"";
            AssertDictEquality(resultQuery, "q", expected, false);
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

            var result = Sut.GenerateGetUsersQueryString(currentUri, req);

            var resultQuery = QueryHelpers.ParseQuery(result.Query);

            Assert.Equal(8, resultQuery.Count);
            AssertDictEquality(resultQuery, "page", req.Page - 1);
            AssertDictEquality(resultQuery, "include_totals", req.IncludeTotals);

            AssertDictEquality(resultQuery, "per_page", req.PerPage);
            AssertDictEquality(resultQuery, "sort", req.Sort);
            AssertDictEquality(resultQuery, "fields", req.Fields);
            AssertDictEquality(resultQuery, "include_fields", req.IncludeFields);
            AssertDictEquality(resultQuery, "search_engine", req.SearchEngine);

            var expected = $"identities.connection:\"{req.Connection}\"";
            AssertDictEquality(resultQuery, "q", expected, false);
        }


        [Fact]
        public void Auth0QueryStringGenerator_Generates_Get_User_List_With_v2_And_Complex_Query_String()
        {
            var currentUri = "https://iamglobaleducation.auth0.com/api/v2/users";

            var req = new Auth0ListUsersRequest();
            req.Page = 1;
            req.IncludeTotals = false;

            req.PerPage = 25;
            req.Sort = "email:1";
            req.Connection = "Development";
            req.IncludeFields = true;
            req.QueryString = "user_id:\"auth0|58f7fa9ddb5a3927da1aa529\" OR user_id:\"auth0|59946d0d31d6c842b92e6922\"";
            req.SearchEngine = Auth0ApiVersionNames.v2.ToString();

            var result = Sut.GenerateGetUsersQueryString(currentUri, req);

            var resultQuery = QueryHelpers.ParseQuery(result.Query);

            Assert.Equal(7, resultQuery.Count);
            AssertDictEquality(resultQuery, "page", 0);
            AssertDictEquality(resultQuery, "include_totals", req.IncludeTotals);

            AssertDictEquality(resultQuery, "per_page", req.PerPage);
            AssertDictEquality(resultQuery, "sort", req.Sort);
            AssertDictEquality(resultQuery, "include_fields", req.IncludeFields);
            AssertDictEquality(resultQuery, "search_engine", req.SearchEngine);

            //var expected = "(user_id%3A%22auth0%7C58f7fa9ddb5a3927da1aa529%22%20OR%20user_id%3A%22auth0%7C59946d0d31d6c842b92e6922%22)%20AND%20identities.connection%3A%22Development%22";
            var expected = $"({req.QueryString}) AND identities.connection:\"{req.Connection}\"";
            AssertDictEquality(resultQuery, "q", expected, false);

            // This query string seems to work...but we're not properly matching it, let's see what's not matching up here
            const string expectedQuery = "(user_id%3A%22auth0%7C58f7fa9ddb5a3927da1aa529%22%20OR%20user_id%3A%22auth0%7C59946d0d31d6c842b92e6922%22)%20AND%20identities.connection%3A%22Development%22";

            var queryVal = resultQuery["q"];
            Assert.Equal(expectedQuery, GetAuth0QueryString(queryVal.ToString()));
            Assert.Equal(expectedQuery, GetAuth0QueryString(expected));
        }

        [Fact]
        public void Auth0QueryStringGenerator_Generates_Get_User_List_With_v2_And_Complex_No_Query_String()
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

            var result = Sut.GenerateGetUsersQueryString(currentUri, req);

            var resultQuery = QueryHelpers.ParseQuery(result.Query);

            Assert.Equal(8, resultQuery.Count);
            AssertDictEquality(resultQuery, "page", req.Page - 1);
            AssertDictEquality(resultQuery, "include_totals", req.IncludeTotals);

            AssertDictEquality(resultQuery, "per_page", req.PerPage);
            AssertDictEquality(resultQuery, "sort", req.Sort);
            AssertDictEquality(resultQuery, "fields", req.Fields);
            AssertDictEquality(resultQuery, "include_fields", req.IncludeFields);
            AssertDictEquality(resultQuery, "search_engine", req.SearchEngine);

            var expected = $"identities.connection:\"{req.Connection}\"";
            AssertDictEquality(resultQuery, "q", expected, false);
        }

        [Fact]
        public void Auth0QueryStringGenerator_Generates_Get_User_List_With_v2_And_Complex_Empty_Query_String()
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

            var result = Sut.GenerateGetUsersQueryString(currentUri, req);

            var resultQuery = QueryHelpers.ParseQuery(result.Query);

            Assert.Equal(8, resultQuery.Count);
            AssertDictEquality(resultQuery, "page", req.Page - 1);
            AssertDictEquality(resultQuery, "include_totals", req.IncludeTotals);

            AssertDictEquality(resultQuery, "per_page", req.PerPage);
            AssertDictEquality(resultQuery, "sort", req.Sort);
            AssertDictEquality(resultQuery, "fields", req.Fields);
            AssertDictEquality(resultQuery, "include_fields", req.IncludeFields);
            AssertDictEquality(resultQuery, "search_engine", req.SearchEngine);

            var expected = $"identities.connection:\"{req.Connection}\"";
            AssertDictEquality(resultQuery, "q", expected, false);
        }

        [Fact]
        public void QueryString_Is_Converted_Appropriately()
        {
            var currentUri = "https://google.com";

            var req = new Auth0ListUsersRequest();
            
            req.Connection = "Development";
            req.QueryString = "user_id:\"auth0|58f7fa9ddb5a3927da1aa529\" OR user_id:\"auth0|59946d0d31d6c842b92e6922\"";
            req.SearchEngine = Auth0ApiVersionNames.v2.ToString();

            var result = Sut.GenerateGetUsersQueryString(currentUri, req);

            var resultQuery = QueryHelpers.ParseQuery(result.Query);

            var queryString = resultQuery["q"];

            const string expectedQuery = "(user_id%3A%22auth0%7C58f7fa9ddb5a3927da1aa529%22%20OR%20user_id%3A%22auth0%7C59946d0d31d6c842b92e6922%22)%20AND%20identities.connection%3A%22Development%22";

            // WTF is this shit!?!?! - WHY IS THIS NECESSARY!?
            //queryString = queryString.ToString().Replace("+", "%20");

            Assert.Equal(expectedQuery, GetAuth0QueryString(queryString));
        }

        #endregion

        #endregion

        #region Helper Methods

        private void AssertDictEquality<T>(Dictionary<string, StringValues> dict, string key, T item, bool toLower = true)
        {
            var dictEntry = dict[key];
            var val = item.ToString();

            string value = val;

            if (toLower)
            {
                value = val.ToLower();
            }
            
            Assert.Equal(dictEntry, value);
        }

        private string GetAuth0QueryString(string query)
        {
            var encoded = WebUtility.UrlEncode(query);
            return encoded.Replace("+", "%20");
        }

        #endregion
    }
}