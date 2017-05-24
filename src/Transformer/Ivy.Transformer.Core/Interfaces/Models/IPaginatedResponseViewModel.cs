using System.Collections.Generic;

namespace Ivy.Transformer.Core.Interfaces.Models
{
    public interface IPaginatedResponseViewModel<TModel>
    {
        IEnumerable<TModel> Data { get; set; }

        int TotalCount { get; set; }
    }
}
