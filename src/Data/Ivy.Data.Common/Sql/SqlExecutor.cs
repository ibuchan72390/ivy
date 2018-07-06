using Dapper;
using Ivy.Data.Core.Interfaces;
using Ivy.Data.Core.Interfaces.SQL;
using System.Collections.Generic;

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

        public void InitializeByConnectionString(string connectionString)
        {
            _tranHelper.InitializeByConnectionString(connectionString);
        }

        public void InitializeByDatabaseKey(string databaseKey)
        {
            _tranHelper.InitializeByDatabaseKey(databaseKey);
        }

        #endregion

        #region Public Methods

        public void ExecuteNonQuery(string sql, object parms = null, ITranConn tc = null)
        {
            _tranHelper.WrapInTransaction(
                tran => tran.Connection.Execute(sql, parms, tran.Transaction), tc);
        }

        public IEnumerable<T> ExecuteTypedQuery<T>(string sql, object parms = null, ITranConn tc = null)
        {
            return _tranHelper.WrapInTransaction(
                tran => tran.Connection.Query<T>(sql, parms, tran.Transaction), tc);
        }

        #endregion
    }
}
