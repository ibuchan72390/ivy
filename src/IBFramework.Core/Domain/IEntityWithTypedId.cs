namespace IBFramework.Core.Domain
{
    public interface IEntityWithTypedId<TKey>
    {
        TKey Id { get; set; }
    }
}
