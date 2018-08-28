using Ivy.Data.Core.Interfaces;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Ivy.Data.Common.Transaction
{
    public abstract class BaseTranConnGenerator : ITranConnGenerator
    {
        #region Variables & Constants

        protected abstract IDbConnection InternalGetOpenDbConnection(string connectionString);

        protected abstract Task<IDbConnection> InternalGetOpenDbConnectionAsync(string connectionString);

        #endregion

        #region Public Methods

        public ITranConn GenerateTranConn(string connectionString, IsolationLevel isolation = IsolationLevel.ReadUncommitted)
        {
            CheckConnectionStringInitialized(connectionString);

            IDbConnection conn = InternalGetOpenDbConnection(connectionString);
            IDbTransaction tran = conn.BeginTransaction(isolation);

            return new TranConn(conn, tran);
        }

        public async Task<ITranConn> GenerateTranConnAsync(string connectionString, IsolationLevel isolation = IsolationLevel.ReadUncommitted)
        {
            CheckConnectionStringInitialized(connectionString);

            IDbConnection conn = await InternalGetOpenDbConnectionAsync(connectionString);
            IDbTransaction tran = conn.BeginTransaction(isolation);

            return new TranConn(conn, tran);
        }

        #endregion

        #region Helper Methods

        protected void CheckConnectionStringInitialized(string connectionString)
        {
            if (connectionString == null)
            {
                throw new Exception("Your service has not been initialized! The connection is going to fail!");
            }
        }

        #endregion
    }
}
