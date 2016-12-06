namespace IBFramework.Core.Data
{
    public interface IEntityWithTypedId<TKey>
    {
        TKey Id { get; set; }
    }
}
