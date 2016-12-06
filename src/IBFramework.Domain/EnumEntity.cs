using IBFramework.Core.Domain;

namespace IBFramework.Domain
{
    public class EnumEntity<TKey> : EntityWithTypedId<TKey>, IEnumEntity<TKey>
    {
        public string Name { get; set; }

        public string FriendlyName { get; set; }

        public int SortOrder { get; set; }
    }
}
