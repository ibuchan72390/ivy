using IBFramework.Core.Data.Domain;
using IBFramework.Core.Data.Init;
using System;

namespace IBFramework.Core.Data.Base.EnumEntity
{
    public interface IEnumEntityService<TEntity, TRepo, TEnum> : IInitialize
        where TEntity : class, IEnumEntity<TEnum>
        where TRepo : IEntityRepository<TEntity>
        where TEnum : struct, IComparable, IFormattable, IConvertible
    {
    }
}
