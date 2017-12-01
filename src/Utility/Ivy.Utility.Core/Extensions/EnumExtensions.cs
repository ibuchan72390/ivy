using Ivy.Data.Core.Interfaces.Domain;
using System;

namespace Ivy.Utility.Core.Extensions
{
    public static class EnumExtensions
    {
        public static TEnumEntity ToEnumEntityWithTypedId<TEnum, TEnumEntity, TId>(this TEnum enumVal)
            where TEnum : struct, IComparable, IFormattable, IConvertible
            where TEnumEntity : IEnumEntityWithTypedId<TId, TEnum>, new()
        {
            return new TEnumEntity
            {
                Name = enumVal.ToString()
            };
        }

        public static TEnumEntity ToEnumEntity<TEnum, TEnumEntity>(this TEnum enumVal)
            where TEnum : struct, IComparable, IFormattable, IConvertible
            where TEnumEntity : IEnumEntity<TEnum>, new()
        {
            return enumVal.ToEnumEntityWithTypedId<TEnum, TEnumEntity, int>();
        }
    }
}
