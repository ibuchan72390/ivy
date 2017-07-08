using Ivy.Auth0.Core.Services;
using Ivy.Auth0.Core.Models.Requests;
using System.Collections.Generic;
using Microsoft.AspNetCore.WebUtilities;
using System;

namespace Ivy.Auth0.Services
{
    public class Auth0QueryStringUriGenerator : IAuth0QueryStringUriGenerator
    {
        #region Variables & Constants

        public Uri GenerateGetUsersQueryString(string currentUri, Auth0ListUsersRequest req)
        {
            var dict = new Dictionary<string, string>();

            dict.Add("page", req.Page.ToString());
            dict.Add("include_totals", req.IncludeTotals.ToString());

            AppendIfNot(ref dict, "per_page", req.PerPage, 0);
            AppendIfNot(ref dict, "sort", req.Sort, null);
            AppendIfNot(ref dict, "connection", req.Connection, null);
            AppendIfNot(ref dict, "fields", req.Fields, null);
            AppendIfNot(ref dict, "include_fields", req.IncludeFields, false);
            AppendIfNot(ref dict, "q", req.QueryString, null);
            AppendIfNot(ref dict, "search_engine", req.SearchEngine, null);

            return DictionaryToUri(currentUri, dict);
        }

        #endregion

        #region Helper Methods

        private Uri DictionaryToUri(string currentUri, Dictionary<string, string> dict)
        {
            var newUri = QueryHelpers.AddQueryString(currentUri, dict);

            return new Uri(newUri);
        }

        private void AppendIfNot<T>(ref Dictionary<string, string> dict, string name, T value, T compare)
        {
            if (value == null) return;

            if (!value.Equals(compare))
            {
                dict.Add(name, value.ToString());
            }
        }

        #endregion
    }
}