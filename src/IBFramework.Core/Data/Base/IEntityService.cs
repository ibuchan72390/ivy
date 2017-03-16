using IBFramework.Core.Data.Domain;
using IBFramework.Core.Data.Init;

namespace IBFramework.Core.Data.Base
{
    public interface IEntityService<TEntity, TRepo> : IInitialize
        where TEntity : class, IEntity
        where TRepo : IEntityRepository<TEntity>
    {
    }
}
