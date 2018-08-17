using System.Collections.Generic;

namespace Ivy.Data.Core.Interfaces.SQL
{
    public interface ISqlExecutor
    {
        void ExecuteNonQuery(string sql, string connectionString, ITranConn tc = null, object parms = null);

        IEnumerable<T> ExecuteTypedQuery<T>(string sql, string connectionString, ITranConn tc = null, object parms = null);
    }
}
