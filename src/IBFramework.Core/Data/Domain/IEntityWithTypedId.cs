namespace IBFramework.Core.Data.Domain
{
    public interface IEntityWithTypedId<TKey> : IEntityWithReferences
    {
        TKey Id { get; set; }
    }
}
