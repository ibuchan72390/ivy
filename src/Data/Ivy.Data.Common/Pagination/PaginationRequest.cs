using Ivy.Data.Core.Interfaces.Pagination;

namespace Ivy.Data.Common.Pagination
{
    public class PaginationRequest : IPaginationRequest
    {
        public PaginationRequest()
        {
            PageCount = 1;
            PageNumber = 10;
        }

        public int PageCount { get; set; }

        public int PageNumber { get; set; }

        public string Search { get; set; }
    }
}
