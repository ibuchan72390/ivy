﻿using IBFramework.Data.Core.Interfaces.Domain;
using IBFramework.Data.Core.Interfaces.Functional.Entity;
using System;

namespace IBFramework.Data.Core.Interfaces.Functional.EnumEntity
{
    public interface IEnumEntityGet<TEntity, TKey, TEnum> : IEntityGet<TEntity, TKey>
        where TEntity : class, IEnumEntityWithTypedId<TKey, TEnum>
        where TEnum : struct, IComparable, IFormattable, IConvertible
    {
        TEntity GetByName(TEnum name, ITranConn tc = null);
    }

    public interface IEnumEntityGet<TEntity, TEnum> : IEnumEntityGet<TEntity, int, TEnum>
        where TEntity : class, IEnumEntity<TEnum>
        where TEnum : struct, IComparable, IFormattable, IConvertible
    {
    }
}
