using System.Collections.Generic;

namespace Ivy.Transformer.Core.Interfaces.Entity
{
    // Move to Framework
    public interface IEntityToViewModelListTransformer<TEntity, TViewModel>
    {
        IEnumerable<TViewModel> Transform(IEnumerable<TEntity> entities);
    }
}
