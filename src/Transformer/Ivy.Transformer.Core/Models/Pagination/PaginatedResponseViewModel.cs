using Ivy.Transformer.Core.Interfaces.Models;
using System.Collections.Generic;

namespace Ivy.Transformer.Core.Models.Pagination
{
    public class PaginatedResponseViewModel<TModel> : IPaginatedResponseViewModel<TModel>
    {
        public IEnumerable<TModel> Data { get; set; }

        public int TotalCount { get; set; }
    }
}
