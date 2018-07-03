using Ivy.Data.Core.Interfaces.Domain;
using Ivy.Transformer.Core.Models;

namespace Ivy.Transformer.Core.Interfaces.Entity
{
    public interface IEntityTransformer<TEntity, TViewModel, TKey> : 
        IEntityToViewModelListTransformer<TEntity, TViewModel>,
        IEntityToViewModelTransformer<TEntity, TViewModel>,
        IViewModelToEntityListTransformer<TEntity, TViewModel>,
        IViewModelToEntityTransformer<TEntity, TViewModel>
        where TEntity : IEntityWithTypedId<TKey>
        where TViewModel : BaseEntityWithTypedIdModel<TKey>
    {
    }


    public interface IEntityTransformer<TEntity, TViewModel> : 
        IEntityTransformer<TEntity, TViewModel, int>
        where TEntity : IEntity
        where TViewModel : BaseEntityModel
    {
    }
}
