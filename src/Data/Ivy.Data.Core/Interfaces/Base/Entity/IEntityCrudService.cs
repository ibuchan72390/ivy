using Ivy.Data.Core.Interfaces.Domain;
using Ivy.Data.Core.Interfaces.Functional;
using Ivy.Data.Core.Interfaces.Functional.Entity;

namespace Ivy.Data.Core.Interfaces.Base.Entity
{
    public interface IEntityCrudService<TEntity, TRepo> : 
        IEntityService<TEntity, TRepo>,
        IGetAll<TEntity>,
        IPaginatedGetAll<TEntity>,
        IDeleteAll<TEntity>,
        IEntityDelete<TEntity>,
        IEntityGet<TEntity>,
        IEntitySaveOrUpdate<TEntity>
        
        where TEntity : class, IEntity
        where TRepo : IEntityRepository<TEntity>
    {
    }
}
