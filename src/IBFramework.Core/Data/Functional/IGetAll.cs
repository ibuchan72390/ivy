using System.Collections.Generic;

namespace IBFramework.Core.Data.Functional
{
    public interface IGetAll<TEntity>
        where TEntity : class
    {
        IEnumerable<TEntity> GetAll(ITranConn tc = null);
    }
}
