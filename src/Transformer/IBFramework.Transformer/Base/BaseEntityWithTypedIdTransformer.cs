using System.Collections.Generic;
using System.Linq;
using IBFramework.Transformer.Core.Interfaces.Entity;
using IBFramework.Data.Core.Interfaces.Domain;
using IBFramework.Transformer.Core.Models;

namespace IBFramework.Transformer.Base
{
    public class BaseEntityWithTypedIdTransformer<TEntity, TModel, TKey> :
        IEntityToViewModelTransformer<TEntity, TModel>,
        IEntityToViewModelListTransformer<TEntity, TModel>,
        IViewModelToEntityTransformer<TEntity, TModel>,
        IViewModelToEntityListTransformer<TEntity, TModel>

        where TEntity : IEntityWithTypedId<TKey>, new()
        where TModel : BaseEntityWithTypedIdModel<TKey>, new()
    {
        #region Entity -> VM

        public virtual IEnumerable<TModel> Transform(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                return new List<TModel>();

            return entities.Select(Transform);
        }

        public virtual TModel Transform(TEntity entity)
        {
            if (entity == null)
                return null;

            return new TModel { Id = entity.Id };
        }

        #endregion

        #region VM -> Entity

        public virtual TEntity Transform(TModel model)
        {
            if (model == null)
                return default(TEntity);

            return new TEntity { Id = model.Id };
        }

        public virtual IEnumerable<TEntity> Transform(IEnumerable<TModel> models)
        {
            if (models == null)
                return new List<TEntity>();

            return models.Select(Transform);
        }

        #endregion
    }
}
