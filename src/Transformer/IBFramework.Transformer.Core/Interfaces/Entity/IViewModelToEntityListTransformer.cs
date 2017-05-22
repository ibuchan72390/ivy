using System.Collections.Generic;

namespace IBFramework.Transformer.Core.Interfaces.Entity
{
    // Move to Framework
    public interface IViewModelToEntityListTransformer<TEntity, TViewModel>
    {
        IEnumerable<TEntity> Transform(IEnumerable<TViewModel> models);
    }
}
