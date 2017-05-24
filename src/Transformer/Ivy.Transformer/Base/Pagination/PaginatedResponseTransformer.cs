using Ivy.Data.Common.Pagination;
using Ivy.Transformer.Core.Interfaces.Pagination;
using Ivy.Transformer.Core.Models.Pagination;
using Ivy.Transformer.Core.Interfaces.Entity;

namespace Ivy.Transformer.Base.Pagination
{
    public class PaginatedResponseTransformer<TEntity, TModel, TTransformer> :
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

        public PaginatedResponseViewModel<TModel> Transform(PaginationResponse<TEntity> paginationResponse)
        {
            var models = _transformer.Transform(paginationResponse.Data);

            return new PaginatedResponseViewModel<TModel>
            {
                Data = models,
                TotalCount = paginationResponse.TotalCount
            };
        }

        #endregion
    }
}
