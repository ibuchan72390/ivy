namespace IBFramework.Core.Data
{
    public interface IEnumEntity<TKey> : IEntityWithTypedId<TKey>
    {
        string Name { get; set; }

        string FriendlyName { get; set; }

        int SortOrder { get; set; }
    }

    // Is this even necessary or is it inferred from usage????
    //public interface IEnumEntity : IEntity
    //{
    //    string Name { get; set; }

    //    string FriendlyName { get; set; }

    //    int SortOrder { get; set; }
    //}
}
