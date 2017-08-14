using Ivy.Data.Core.Interfaces.Pagination;
using Ivy.Transformer.Core.Models.Pagination;
using System.Collections.Generic;

namespace Ivy.Transformer.Base.Pagination
{
    public abstract class BasePaginatedResponseTransformer<TEntity, TModel>
    {
        #region Helper Methods

        protected PaginatedResponseViewModel<TModel> GenerateResponseModel(IPaginationResponse<TEntity> response, 
            IEnumerable<TModel> models)
        {
            return new PaginatedResponseViewModel<TModel>
            {
                Data = models,
                TotalCount = response.TotalCount
            };
        }

        #endregion
    }
}
