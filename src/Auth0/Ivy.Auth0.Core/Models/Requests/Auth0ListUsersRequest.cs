namespace Ivy.Auth0.Core.Models.Requests
{
    public class Auth0ListUsersRequest
    {
        public Auth0ListUsersRequest()
        {
            IncludeTotals = true;
            Sort = "email:1";
            IncludeFields = true;
            SearchEngine = "v2";
        }

        public int PerPage { get; set; }
        public int Page { get; set; }
        public bool IncludeTotals { get; set; }
        public string Sort { get; set; }
        public string Connection { get; set; }
        public string Fields { get; set; }
        public bool IncludeFields { get; set; }
        public string QueryString { get; set; }
        public string SearchEngine { get; set; }
    }
}
