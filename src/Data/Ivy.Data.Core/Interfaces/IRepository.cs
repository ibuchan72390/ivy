using Ivy.Data.Core.Interfaces.Base.Functional;
using Ivy.Data.Core.Interfaces.Domain;
using Ivy.Data.Core.Interfaces.Functional;
using Ivy.Data.Core.Interfaces.Functional.Entity;
using Ivy.Data.Core.Interfaces.Functional.EnumEntity;
using Ivy.Data.Core.Interfaces.Init;
using System;

namespace Ivy.Data.Core.Interfaces
{
    // Base
    public interface IBaseRepository<TObject> : 
        IInitialize, 
        IDeleteAll<TObject>, 
        IGetAll<TObject>, 
        IPaginatedGetAll<TObject>,
        IGetCount,
        IConnectionString

        where TObject : class
    {
    }


    // Blob
    public interface IBlobRepository<TEntity> : 
        IBaseRepository<TEntity>,
        IBlobInsert<TEntity>

        where TEntity : class
    {
    }


    // Entity
    public interface IEntityRepository<TEntity, TKey> : 
        IBaseRepository<TEntity>,
        IEntityDelete<TEntity, TKey>,
        IEntityGet<TEntity, TKey>,
        IEntitySaveOrUpdate<TEntity, TKey>

        where TEntity : class, IEntityWithTypedId<TKey>
    {
    }

    public interface IEntityRepository<TEntity> : 
        IEntityRepository<TEntity, int>

        where TEntity : class, IEntity
    {
    }

    // EnumEntity
    public interface IEnumEntityRepository<TEntity, TKey, TEnum> : 
        IEntityRepository<TEntity, TKey>, 
        IEnumEntityGet<TEntity, TKey, TEnum>

        where TEntity : class, IEnumEntityWithTypedId<TKey, TEnum>
        where TEnum : struct, IComparable, IFormattable, IConvertible
    {
    }

    public interface IEnumEntityRepository<TEntity, TEnum> : 
        IEntityRepository<TEntity>, 
        IEnumEntityGet<TEntity, TEnum>
        
        where TEntity : class, IEnumEntity<TEnum>
        where TEnum : struct, IComparable, IFormattable, IConvertible
    {
    }

}
