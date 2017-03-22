using IBFramework.Core.Data.Domain;

namespace IBFramework.Core.Domain
{
    /*
     * Base Entity for reference types,
     * makes working with enumeration values or dropdowns on the UI very simple
     */
    public class EnumEntity<TKey> : EntityWithTypedId<TKey>, IEnumEntity<TKey>
    {
        public string Name { get; set; }

        public string FriendlyName { get; set; }

        public int SortOrder { get; set; }
    }
}
