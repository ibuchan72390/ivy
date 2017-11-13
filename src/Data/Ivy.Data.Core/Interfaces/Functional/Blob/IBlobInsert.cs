using System.Collections.Generic;

namespace Ivy.Data.Core.Interfaces.Base.Functional
{
    public interface IInsertBlob<TEntity>
        where TEntity : class
    {
        void Insert(TEntity entity, ITranConn tc = null);
    }

    public interface IBulkInsertBlob<TEntity>
        where TEntity : class
    {
        void BulkInsert(IEnumerable<TEntity> entities, ITranConn tc = null);
    }

    public interface IBlobInsert<TEntity> :
        IInsertBlob<TEntity>,
        IBulkInsertBlob<TEntity>
        where TEntity : class
    {
    }
}
