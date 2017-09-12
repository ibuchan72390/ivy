using Ivy.Data.Core.Interfaces.Pagination;

namespace Ivy.Data.Common.Pagination
{
    public class PaginationRequest : IPaginationRequest
    {
        public PaginationRequest()
        {
            PageCount = 10;
            PageNumber = 1;
        }

        public int PageCount { get; set; }

        public int PageNumber { get; set; }

        public string Search { get; set; }
    }
}
