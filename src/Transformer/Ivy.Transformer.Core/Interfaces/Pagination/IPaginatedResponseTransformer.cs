using Ivy.Data.Common.Pagination;
using Ivy.Transformer.Core.Interfaces.Entity;
using Ivy.Transformer.Core.Models.Pagination;

namespace Ivy.Transformer.Core.Interfaces.Pagination
{
    public interface IPaginatedResponseTransformer<TEntity, TModel, TTransformer>
        where TTransformer : IEntityToViewModelListTransformer<TEntity, TModel>
    {
        PaginatedResponseViewModel<TModel> Transform(PaginationResponse<TEntity> paginationResponse);
    }
}
