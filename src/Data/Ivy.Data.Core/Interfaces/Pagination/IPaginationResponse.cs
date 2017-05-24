using System.Collections.Generic;

namespace Ivy.Data.Core.Interfaces.Pagination
{
    public interface IPaginationResponse<T>
    {
        IEnumerable<T> Data { get; set; }

        int TotalCount { get; set; }
    }
}
