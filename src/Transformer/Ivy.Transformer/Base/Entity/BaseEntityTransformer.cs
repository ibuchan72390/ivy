using Ivy.Data.Core.Interfaces.Domain;
using Ivy.Transformer.Core.Models;

namespace Ivy.Transformer.Base.Entity
{
    public class BaseEntityTransformer<TEntity, TModel> : 
        BaseEntityWithTypedIdTransformer<TEntity, TModel, int>
        where TEntity : IEntity, new()
        where TModel : BaseEntityModel, new()
    {
    }
}
