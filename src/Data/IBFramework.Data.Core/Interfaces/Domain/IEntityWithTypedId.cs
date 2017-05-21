namespace IBFramework.Data.Core.Interfaces.Domain
{
    public interface IEntityWithTypedId<TKey> : IEntityWithReferences
    {
        TKey Id { get; set; }
    }
}
