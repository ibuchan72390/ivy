using IBFramework.Data.Core.Interfaces.Domain;
using IBFramework.Data.Core.Interfaces.Functional;
using IBFramework.Data.Core.Interfaces.Functional.Entity;

namespace IBFramework.Data.Core.Interfaces.Base.Entity
{
    public interface IEntityCrudService<TEntity, TRepo> : 
        IEntityService<TEntity, TRepo>,
        IGetAll<TEntity>,
        IDeleteAll<TEntity>,
        IEntityDelete<TEntity>,
        IEntityGet<TEntity>,
        IEntitySaveOrUpdate<TEntity>
        
        where TEntity : class, IEntity
        where TRepo : IEntityRepository<TEntity>
    {
    }
}
