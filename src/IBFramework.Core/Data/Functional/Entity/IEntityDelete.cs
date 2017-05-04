using IBFramework.Core.Data.Domain;

namespace IBFramework.Core.Data.Functional.Entity
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
