using IBFramework.Data.Core.Interfaces.Pagination;

namespace IBFramework.Data.Common.Pagination
{
    public class PaginationRequest : IPaginationRequest
    {
        public int PageCount { get; set; }

        public int PageNumber { get; set; }

        public string Search { get; set; }
    }
}
