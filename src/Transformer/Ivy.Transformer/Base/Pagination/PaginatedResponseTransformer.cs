using Ivy.Transformer.Core.Interfaces.Pagination;
using Ivy.Transformer.Core.Interfaces.Entity;
using Ivy.Data.Core.Interfaces.Pagination;
using Ivy.Transformer.Core.Interfaces.Models;

namespace Ivy.Transformer.Base.Pagination
{
    public class PaginatedResponseTransformer<TEntity, TModel, TTransformer> :
        BasePaginatedResponseTransformer<TEntity, TModel>,
        IPaginatedResponseTransformer<TEntity, TModel, TTransformer>
        where TTransformer : IEntityToViewModelListTransformer<TEntity, TModel>
    {
        #region Variables & Constants

        private TTransformer _transformer;

        #endregion

        #region Constructor

        public PaginatedResponseTransformer(
            TTransformer transformer)
        {
            _transformer = transformer;
        }

        #endregion

        #region Public Methods

        public IPaginatedResponseViewModel<TModel> Transform(IPaginationResponse<TEntity> paginationResponse)
        {
            var models = _transformer.Transform(paginationResponse.Data);

            return GenerateResponseModel(paginationResponse, models);
        }

        #endregion
    }
}
