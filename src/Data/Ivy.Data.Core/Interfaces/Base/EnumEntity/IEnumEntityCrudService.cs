using Ivy.Data.Core.Interfaces.Base.Entity;
using Ivy.Data.Core.Interfaces.Domain;
using System;

namespace Ivy.Data.Core.Interfaces.Base.EnumEntity
{
    public interface IEnumEntityCrudService<TEntity, TRepo, TEnum> : 
        IEnumEntityService<TEntity, TRepo, TEnum>,
        IEntityCrudService<TEntity, TRepo>

        where TEntity : class, IEnumEntity<TEnum>
        where TRepo : IEntityRepository<TEntity>
        where TEnum : struct, IComparable, IFormattable, IConvertible
    {
    }
}
