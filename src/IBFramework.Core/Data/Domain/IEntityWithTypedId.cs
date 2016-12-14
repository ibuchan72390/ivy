namespace IBFramework.Core.Data.Domain
{
    public interface IEntityWithTypedId<TKey>
    {
        TKey Id { get; set; }
    }
}
