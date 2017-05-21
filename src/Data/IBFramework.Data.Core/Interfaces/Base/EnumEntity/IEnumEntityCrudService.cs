using IBFramework.Data.Core.Interfaces.Domain;
using IBFramework.Data.Core.Interfaces.Functional.Entity;
using IBFramework.Data.Core.Interfaces.Functional.EnumEntity;
using System;

namespace IBFramework.Data.Core.Interfaces.Base.EnumEntity
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
