using IBFramework.Data.Core.Interfaces.Domain;
using IBFramework.Transformer.Core.Models;

namespace IBFramework.Transformer.Base
{
    public class BaseEntityTransformer<TEntity, TModel> : 
        BaseEntityWithTypedIdTransformer<TEntity, TModel, int>
        where TEntity : IEntity, new()
        where TModel : BaseEntityModel, new()
    {
    }
}
