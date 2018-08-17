using Ivy.Data.Core.Interfaces.Domain;
using Ivy.Data.Core.Interfaces.Init;

namespace Ivy.Data.Core.Interfaces.Base.Entity
{
    public interface IEntityService<TEntity, TKey, TRepo> : IInitialize, IConnectionString
        where TEntity : class, IEntityWithTypedId<TKey>
        where TRepo : IEntityRepository<TEntity, TKey>
    {
    }

    public interface IEntityService<TEntity, TRepo> : IEntityService<TEntity, int, TRepo>
        where TEntity : class, IEntity
        where TRepo : IEntityRepository<TEntity>
    {
    }
}
