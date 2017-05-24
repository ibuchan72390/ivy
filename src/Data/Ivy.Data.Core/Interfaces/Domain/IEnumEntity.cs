using System;

namespace Ivy.Data.Core.Interfaces.Domain
{
    public interface IEnumEntityProperties
    {
        string Name { get; set; }

        string FriendlyName { get; set; }

        int SortOrder { get; set; }
    }

    public interface IEnumEntityWithTypedId<TKey> : IEntityWithTypedId<TKey>, IEnumEntityProperties
    {
    }

    public interface IEnumEntityWithTypedId<TKey, TEnum> : IEnumEntityWithTypedId<TKey>
        where TEnum: struct, IComparable, IFormattable, IConvertible
    {
        TEnum GetEnumValue();
    }

    // Is this even necessary or is it inferred from usage????
    public interface IEnumEntity<TEnum> : IEnumEntityWithTypedId<int, TEnum>, IEntity
        where TEnum : struct, IComparable, IFormattable, IConvertible
    {
    }
}
