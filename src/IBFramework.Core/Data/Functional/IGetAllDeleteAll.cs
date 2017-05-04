using System.Collections.Generic;

namespace IBFramework.Core.Data.Functional
{
    public interface IGetAllDeleteAll<TEntity>
        where TEntity : class
    {
        IEnumerable<TEntity> GetAll(ITranConn tc = null);

        void DeleteAll(ITranConn tc = null);
    }
}
