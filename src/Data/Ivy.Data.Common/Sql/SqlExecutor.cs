using Dapper;
using Ivy.Data.Core.Interfaces;
using Ivy.Data.Core.Interfaces.SQL;
using System.Collections.Generic;
using System.Threading.Tasks;

/*
 * This is an extremely dangerous class
 * In all honesty, end users should ALWAYS be referencing repositories;
 * However, this provides a convenient means of building utility functionality.
 */
namespace Ivy.Data.Common.Sql
{
    public class SqlExecutor : ISqlExecutor
    {
        #region Variables

        private readonly ITransactionHelper _tranHelper;

        #endregion

        #region Constructor

        public SqlExecutor(ITransactionHelper tranHelper)
        {
            _tranHelper = tranHelper;
        }

        #endregion

        #region Public Methods

        public void ExecuteNonQuery(string sql, string connectionString, ITranConn tc = null, object parms = null)
        {
            _tranHelper.WrapInTransaction(
                tran => tran.Connection.Execute(sql, parms, tran.Transaction), connectionString, tc);
        }

        public async Task ExecuteNonQueryAsync(string sql, string connectionString, ITranConn tc = null, object parms = null)
        {
            await _tranHelper.WrapInTransactionAsync(
                async tran => await tran.Connection.ExecuteAsync(sql, parms, tran.Transaction), connectionString, tc);
        }

        public IEnumerable<T> ExecuteTypedQuery<T>(string sql, string connectionString, ITranConn tc = null, object parms = null)
        {
            return _tranHelper.WrapInTransaction(
                tran => tran.Connection.Query<T>(sql, parms, tran.Transaction), connectionString, tc);
        }

        public async Task<IEnumerable<T>> ExecuteTypedQueryAsync<T>(string sql, string connectionString, ITranConn tc = null, object parms = null)
        {
            return await _tranHelper.WrapInTransactionAsync(
                async tran => await tran.Connection.QueryAsync<T>(sql, parms, tran.Transaction), connectionString, tc);
        }

        #endregion
    }
}
