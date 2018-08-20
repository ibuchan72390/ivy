using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ivy.Data.Core.Interfaces.Functional
{
    public interface IGetAllEntities<TEntity>
        where TEntity : class
    {
        IEnumerable<TEntity> GetAll(ITranConn tc = null);
    }

    public interface IGetAllEntitiesAsync<TEntity>
        where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync(ITranConn tc = null);
    }

    public interface IGetAll<TEntity> :
        IGetAllEntities<TEntity>,
        IGetAllEntitiesAsync<TEntity>
        where TEntity : class
    {
    }
}
