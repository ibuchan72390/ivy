using System;

namespace IBFramework.Core.Data.Domain
{
    public interface IEnumEntityWithTypedId<TKey, TEnum> : IEntityWithTypedId<TKey>
        where TEnum: struct, IComparable, IFormattable, IConvertible
    {
        string Name { get; set; }

        string FriendlyName { get; set; }

        int SortOrder { get; set; }

        TEnum GetEnumValue();
    }

    // Is this even necessary or is it inferred from usage????
    public interface IEnumEntity<TEnum> : IEnumEntityWithTypedId<int, TEnum>, IEntity
        where TEnum : struct, IComparable, IFormattable, IConvertible
    {
    }
}
