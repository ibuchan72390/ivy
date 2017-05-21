using IBFramework.Data.Core.Interfaces.Domain;
using IBFramework.Data.Core.Interfaces.Init;
using System;

namespace IBFramework.Data.Core.Interfaces.Base.EnumEntity
{
    public interface IEnumEntityService<TEntity, TRepo, TEnum> : IInitialize
        where TEntity : class, IEnumEntity<TEnum>
        where TRepo : IEntityRepository<TEntity>
        where TEnum : struct, IComparable, IFormattable, IConvertible
    {
    }
}
