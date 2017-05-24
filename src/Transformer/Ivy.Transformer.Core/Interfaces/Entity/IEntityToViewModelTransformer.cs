namespace Ivy.Transformer.Core.Interfaces.Entity
{
    // Move to Framework
    public interface IEntityToViewModelTransformer<TEntity, TViewModel>
    {
        TViewModel Transform(TEntity entity);
    }
}
