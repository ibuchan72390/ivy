using System.Collections.Generic;
using Microsoft.AspNetCore.WebUtilities;
using System;
using Ivy.Auth0.Enums;
using Ivy.Auth0.Management.Core.Services;
using Ivy.Auth0.Management.Core.Models.Requests;

namespace Ivy.Auth0.Management.Services
{
    public class Auth0QueryStringUriGenerator : IAuth0QueryStringUriGenerator
    {
        #region Variables & Constants

        public Uri GenerateGetUsersQueryString(string currentUri, Auth0ListUsersRequest req)
        {
            var dict = new Dictionary<string, string>();

            // They use zero-based indexing, we need to subtract one to get the real value
            var pageNum = req.Page < 1 ? 0 : req.Page - 1;

            dict.Add("page", pageNum.ToString());
            dict.Add("include_totals", req.IncludeTotals.ToString().ToLower());

            AppendIfNot(ref dict, "per_page", req.PerPage, 0);
            AppendIfNot(ref dict, "sort", req.Sort, null);
            AppendIfNot(ref dict, "fields", req.Fields, null);
            AppendIfNot(ref dict, "include_fields", req.IncludeFields, false);
            AppendIfNot(ref dict, "q", req.QueryString, null, false); // This will get encoded on connection string append
            AppendIfNot(ref dict, "search_engine", req.SearchEngine, null);

            SetupConnectionQueryString(ref dict, req);

            return DictionaryToUri(currentUri, dict);
        }

        #endregion

        #region Helper Methods

        private Uri DictionaryToUri(string currentUri, Dictionary<string, string> dict)
        {
            var newUri = QueryHelpers.AddQueryString(currentUri, dict);

            return new Uri(newUri);
        }

        private void AppendIfNot<T>(ref Dictionary<string, string> dict, string name, T value, T compare, bool toLower = true)
        {
            if (value == null) return;

            if (!value.Equals(compare))
            {
                // Seems that we need to make these lower case
                // Upper case "True" value throws an error due to "Bad Request"
                string str = value.ToString();

                if (toLower)
                {
                    str = str.ToLower();
                }

                dict.Add(name, str);
            }
        }

        private void SetupConnectionQueryString(ref Dictionary<string, string> dict, Auth0ListUsersRequest req)
        {
            if (req.Connection != null)
            {
                if (req.SearchEngine == Auth0ApiVersionNames.v2.ToString())
                {
                    var str = $"identities.connection:\"{req.Connection}\"";

                    if (dict.ContainsKey("q") && !string.IsNullOrEmpty(dict["q"]))
                    {
                        dict["q"] = $"({dict["q"]}) AND {str}";
                    }
                    else
                    {
                        dict["q"] = str;
                    }
                }
                else if (req.SearchEngine == Auth0ApiVersionNames.v1.ToString())
                {
                    AppendIfNot(ref dict, "connection", req.Connection, null);
                }
            }

            if (dict.ContainsKey("q") &&
                !string.IsNullOrEmpty(dict["q"]))
            {
                var newQ = System.Net.WebUtility.UrlEncode(dict["q"]);

                // I have no idea why this is necessary or something required by Auth0 API
                // This does not match the standard UrlEncode from C#
                dict["q"] = newQ.Replace("+", "%20");
            }
        }

        #endregion
    }
}