using Ivy.Data.Core.Interfaces.Pagination;

namespace Ivy.Data.Common.Pagination
{
    public class PaginationRequest : IPaginationRequest
    {
        public int PageCount { get; set; }

        public int PageNumber { get; set; }

        public string Search { get; set; }
    }
}
