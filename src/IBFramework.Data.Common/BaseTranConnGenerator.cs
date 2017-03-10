using IBFramework.Core.Data;
using System.Data;
using System;

namespace IBFramework.Data.Common
{
    public abstract class BaseTranConnGenerator : ITranConnGenerator
    {
        private string _connectionString = null;

        public ITranConn GenerateTranConn(string connectionString, IsolationLevel isolation = IsolationLevel.ReadUncommitted)
        {
            IDbConnection conn = InternalGetDbConnection(connectionString);

            //Seems this is required
            conn.Open();

            IDbTransaction tran = conn.BeginTransaction(isolation);
            return new TranConn(conn, tran);
        }

        protected abstract IDbConnection InternalGetDbConnection(string connectionString);
    }
}
