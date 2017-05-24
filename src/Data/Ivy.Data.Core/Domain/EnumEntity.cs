using Ivy.Data.Core.Interfaces.Domain;
using System;

namespace Ivy.Data.Core.Domain
{
    /*
     * Base Entity for reference types,
     * makes working with enumeration values or dropdowns on the UI very simple
     */

    public class EnumEntityWithTypedId<TKey> : EntityWithTypedId<TKey>, IEnumEntityWithTypedId<TKey>
    {
        public string Name { get; set; }

        public string FriendlyName { get; set; }

        public int SortOrder { get; set; }
    }

    public class EnumEntityWithTypedId<TKey, TEnum> : EnumEntityWithTypedId<TKey>, IEnumEntityWithTypedId<TKey, TEnum>
         where TEnum : struct, IComparable, IFormattable, IConvertible
    {
        public TEnum GetEnumValue()
        {
            TEnum result;

            var success = System.Enum.TryParse<TEnum>(Name, out result);

            if (!success)
            {
                throw new Exception("Working with an EnumEntity whose DB values do not match the Entity values! " + 
                    $"Enum Type: {typeof(TEnum).Name} / Db Name: {Name}");
            }

            return result;
        }
    }

    public class EnumEntity<TEnum> : EnumEntityWithTypedId<int, TEnum>, IEnumEntity<TEnum>
        where TEnum : struct, IComparable, IFormattable, IConvertible
    {
    }

}
