using System.Collections.Generic;

namespace IBFramework.Data.Core.Interfaces.Functional
{
    public interface IGetAll<TEntity>
        where TEntity : class
    {
        IEnumerable<TEntity> GetAll(ITranConn tc = null);
    }
}
