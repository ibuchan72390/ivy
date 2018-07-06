using Ivy.Data.Core.Interfaces.Init;
using System.Collections.Generic;

namespace Ivy.Data.Core.Interfaces.SQL
{
    public interface ISqlExecutor : IInitialize
    {
        void ExecuteNonQuery(string sql, object parms = null, ITranConn tc = null);

        IEnumerable<T> ExecuteTypedQuery<T>(string sql, object parms = null, ITranConn tc = null);
    }
}
