using IBFramework.Core.Data.Domain;
using IBFramework.Core.Data.Functional.Entity;
using IBFramework.Core.Data.Functional.EnumEntity;
using System;

namespace IBFramework.Core.Data.Base.EnumEntity
{
    public interface IEnumEntityCrudService<TEntity, TRepo, TEnum> : 
        IEnumEntityService<TEntity, TRepo, TEnum>,
        IEntityDelete<TEntity>,
        IEntitySaveOrUpdate<TEntity>,
        IEnumEntityGet<TEntity, TEnum>
        
        where TEntity : class, IEnumEntity<TEnum>
        where TRepo : IEntityRepository<TEntity>
        where TEnum : struct, IComparable, IFormattable, IConvertible
    {
    }
}
