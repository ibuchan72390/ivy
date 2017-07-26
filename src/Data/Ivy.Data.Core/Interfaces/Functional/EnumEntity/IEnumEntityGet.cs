﻿using Ivy.Data.Core.Interfaces.Domain;
using System;
using System.Collections.Generic;

namespace Ivy.Data.Core.Interfaces.Functional.EnumEntity
{
    public interface IEnumEntityGet<TEntity, TKey, TEnum>
        where TEntity : class, IEnumEntityWithTypedId<TKey, TEnum>
        where TEnum : struct, IComparable, IFormattable, IConvertible
    {
        TEntity GetByName(TEnum name, ITranConn tc = null);

        IEnumerable<TEntity> GetByNames(IEnumerable<TEnum> enumVals, ITranConn tc = null);
    }

    public interface IEnumEntityGet<TEntity, TEnum> : IEnumEntityGet<TEntity, int, TEnum>
        where TEntity : class, IEnumEntity<TEnum>
        where TEnum : struct, IComparable, IFormattable, IConvertible
    {
    }
}
