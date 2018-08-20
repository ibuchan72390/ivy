using System.Threading.Tasks;

namespace Ivy.Data.Core.Interfaces.Functional
{
    public interface IDeleteAllEntities<TEntity>
        where TEntity : class
    {
        void DeleteAll(ITranConn tc = null);
    }

    public interface IDeleteAllEntitiesAsync<TEntity>
        where TEntity : class
    {
        Task DeleteAllAsync(ITranConn tc = null);
    }

    public interface IDeleteAll<TEntity> :
        IDeleteAllEntities<TEntity>,
        IDeleteAllEntitiesAsync<TEntity>
        where TEntity : class
    {
    }
}
