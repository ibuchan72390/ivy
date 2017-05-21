using IBFramework.Data.Core.Interfaces.Domain;

namespace IBFramework.Data.Core.Interfaces.Functional.Entity
{
    public interface IEntityDelete<TEntity, TKey>
        where TEntity : class, IEntityWithTypedId<TKey>
    {
        void Delete(TEntity entity, ITranConn tc = null);

        void DeleteById(TKey id, ITranConn tc = null);
    }

    public interface IEntityDelete<TEntity> : IEntityDelete<TEntity, int>
        where TEntity : class, IEntity
    {

    }

}
