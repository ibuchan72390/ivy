using System.Collections.Generic;

namespace Ivy.Transformer.Core.Models.Pagination
{
    public class PaginatedResponseViewModel<TModel>
    {
        public IEnumerable<TModel> Data { get; set; }

        public int TotalCount { get; set; }
    }
}
