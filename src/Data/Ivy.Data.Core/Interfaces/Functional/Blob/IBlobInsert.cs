using System.Collections.Generic;

namespace Ivy.Data.Core.Interfaces.Base.Functional
{
    public interface IBlobInsert<TEntity>
        where TEntity : class
    {
        void Insert(TEntity entity, ITranConn tc = null);

        void BulkInsert(IEnumerable<TEntity> entities, ITranConn tc = null);
    }
}
