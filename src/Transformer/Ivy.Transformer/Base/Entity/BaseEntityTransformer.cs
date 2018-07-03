using Ivy.Data.Core.Interfaces.Domain;
using Ivy.Transformer.Core.Interfaces.Entity;
using Ivy.Transformer.Core.Models;

namespace Ivy.Transformer.Base.Entity
{
    public class BaseEntityTransformer<TEntity, TModel> : 
        BaseEntityWithTypedIdTransformer<TEntity, TModel, int>,
        IEntityTransformer<TEntity, TModel>
        where TEntity : IEntity, new()
        where TModel : BaseEntityModel, new()
    {
    }
}
