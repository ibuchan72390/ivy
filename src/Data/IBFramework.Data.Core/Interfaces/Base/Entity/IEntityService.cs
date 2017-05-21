using IBFramework.Data.Core.Interfaces.Domain;
using IBFramework.Data.Core.Interfaces.Init;

namespace IBFramework.Data.Core.Interfaces.Base.Entity
{
    public interface IEntityService<TEntity, TRepo> : IInitialize
        where TEntity : class, IEntity
        where TRepo : IEntityRepository<TEntity>
    {
    }
}
