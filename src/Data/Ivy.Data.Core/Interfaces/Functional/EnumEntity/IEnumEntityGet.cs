using Ivy.Data.Core.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ivy.Data.Core.Interfaces.Functional.EnumEntity
{
    public interface IGetEnumEntityByName<TEntity, TKey, TEnum>
        where TEntity : class, IEnumEntityWithTypedId<TKey, TEnum>
        where TEnum : struct, IComparable, IFormattable, IConvertible
    {
        TEntity GetByName(TEnum name, ITranConn tc = null);
    }

    public interface IGetEnumEntityByNameAsync<TEntity, TKey, TEnum>
        where TEntity : class, IEnumEntityWithTypedId<TKey, TEnum>
        where TEnum : struct, IComparable, IFormattable, IConvertible
    {
        Task<TEntity> GetByNameAsync(TEnum name, ITranConn tc = null);
    }

    public interface IGetEnumEntitiesByNames<TEntity, TKey, TEnum>
        where TEntity : class, IEnumEntityWithTypedId<TKey, TEnum>
        where TEnum : struct, IComparable, IFormattable, IConvertible
    {
        IEnumerable<TEntity> GetByNames(IEnumerable<TEnum> enumVals, ITranConn tc = null);
    }

    public interface IGetEnumEntitiesByNamesAsync<TEntity, TKey, TEnum>
        where TEntity : class, IEnumEntityWithTypedId<TKey, TEnum>
        where TEnum : struct, IComparable, IFormattable, IConvertible
    {
        Task<IEnumerable<TEntity>> GetByNamesAsync(IEnumerable<TEnum> enumVals, ITranConn tc = null);
    }

    public interface IEnumEntityGet<TEntity, TKey, TEnum> :
        IGetEnumEntityByName<TEntity, TKey, TEnum>,
        IGetEnumEntityByNameAsync<TEntity, TKey, TEnum>,
        IGetEnumEntitiesByNames<TEntity, TKey, TEnum>,
        IGetEnumEntitiesByNamesAsync<TEntity, TKey, TEnum>
        where TEntity : class, IEnumEntityWithTypedId<TKey, TEnum>
        where TEnum : struct, IComparable, IFormattable, IConvertible
    {
    }

    public interface IEnumEntityGet<TEntity, TEnum> : IEnumEntityGet<TEntity, int, TEnum>
        where TEntity : class, IEnumEntity<TEnum>
        where TEnum : struct, IComparable, IFormattable, IConvertible
    {
    }
}
