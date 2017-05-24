using Ivy.Data.Core.Interfaces.Pagination;
using Ivy.Transformer.Core.Interfaces.Entity;
using Ivy.Transformer.Core.Interfaces.Models;

namespace Ivy.Transformer.Core.Interfaces.Pagination
{
    public interface IPaginatedResponseTransformer<TEntity, TModel, TTransformer>
        where TTransformer : IEntityToViewModelListTransformer<TEntity, TModel>
    {
        IPaginatedResponseViewModel<TModel> Transform(IPaginationResponse<TEntity> paginationResponse);
    }
}
