using Ivy.Data.Core.Interfaces.Domain;
using Ivy.Data.Core.Interfaces.Init;

namespace Ivy.Data.Core.Interfaces.Base.Entity
{
    public interface IEntityService<TEntity, TRepo> : IInitialize
        where TEntity : class, IEntity
        where TRepo : IEntityRepository<TEntity>
    {
    }
}
