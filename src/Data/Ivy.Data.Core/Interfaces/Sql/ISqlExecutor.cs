using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ivy.Data.Core.Interfaces.SQL
{
    public interface ISqlExecutor
    {
        void ExecuteNonQuery(string sql, string connectionString, ITranConn tc = null, object parms = null);

        Task ExecuteNonQueryAsync(string sql, string connectionString, ITranConn tc = null, object parms = null);

        IEnumerable<T> ExecuteTypedQuery<T>(string sql, string connectionString, ITranConn tc = null, object parms = null);

        Task<IEnumerable<T>> ExecuteTypedQueryAsync<T>(string sql, string connectionString, ITranConn tc = null, object parms = null);
    }
}
