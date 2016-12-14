using System;
using IBFramework.Core.Data.Domain;

namespace IBFramework.Domain
{
    /*
     * Base Entity for reference types,
     * makes working with enumeration values or dropdowns on the UI very simple
     */
    public class EnumEntity<TKey> : IEntityWithTypedId<TKey>, IEnumEntity<TKey>
    {
        public TKey Id { get; set; }

        public string Name { get; set; }

        public string FriendlyName { get; set; }

        public int SortOrder { get; set; }
    }
}
