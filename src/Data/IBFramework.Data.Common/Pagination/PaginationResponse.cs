using IBFramework.Data.Core.Interfaces.Pagination;
using System.Collections.Generic;

namespace IBFramework.Data.Common.Pagination
{
    public class PaginationResponse<T> : IPaginationResponse<T>
    {
        public IEnumerable<T> Data { get; set; }

        public int TotalCount { get; set; }
    }
}
