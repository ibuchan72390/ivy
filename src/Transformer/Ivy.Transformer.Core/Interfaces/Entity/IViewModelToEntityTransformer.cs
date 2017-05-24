namespace Ivy.Transformer.Core.Interfaces.Entity
{
    // Move to Framework
    public interface IViewModelToEntityTransformer<TEntity, TViewModel>
    {
        TEntity Transform(TViewModel model);
    }
}
