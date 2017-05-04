using IBFramework.Core.Data.Domain;
using IBFramework.Core.Data.Functional;
using IBFramework.Core.Data.Functional.Entity;

namespace IBFramework.Core.Data.Base.Entity
{
    public interface IEntityCrudService<TEntity, TRepo> : 
        IEntityService<TEntity, TRepo>,
        IGetAllDeleteAll<TEntity>,
        IEntityDelete<TEntity>,
        IEntityGet<TEntity>,
        IEntitySaveOrUpdate<TEntity>
        
        where TEntity : class, IEntity
        where TRepo : IEntityRepository<TEntity>
    {
    }
}
