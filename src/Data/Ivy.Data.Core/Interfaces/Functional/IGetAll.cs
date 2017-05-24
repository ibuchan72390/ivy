using System.Collections.Generic;

namespace Ivy.Data.Core.Interfaces.Functional
{
    public interface IGetAll<TEntity>
        where TEntity : class
    {
        IEnumerable<TEntity> GetAll(ITranConn tc = null);
    }
}
