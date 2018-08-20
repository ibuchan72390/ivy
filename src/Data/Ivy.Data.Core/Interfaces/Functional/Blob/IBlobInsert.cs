using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ivy.Data.Core.Interfaces.Base.Functional
{
    public interface IInsertBlob<TEntity>
        where TEntity : class
    {
        void Insert(TEntity entity, ITranConn tc = null);
    }

    public interface IInsertBlobAsync<TEntity>
        where TEntity : class
    {
        Task InsertAsync(TEntity entity, ITranConn tc = null);
    }

    public interface IBulkInsertBlob<TEntity>
        where TEntity : class
    {
        void BulkInsert(IEnumerable<TEntity> entities, ITranConn tc = null);
    }

    public interface IBulkInsertBlobAsync<TEntity>
        where TEntity : class
    {
        Task BulkInsertAsync(IEnumerable<TEntity> entities, ITranConn tc = null);
    }

    public interface IBlobInsert<TEntity> :
        IInsertBlob<TEntity>,
        IBulkInsertBlob<TEntity>,
        IInsertBlobAsync<TEntity>,
        IBulkInsertBlobAsync<TEntity>
        where TEntity : class
    {
    }
}
