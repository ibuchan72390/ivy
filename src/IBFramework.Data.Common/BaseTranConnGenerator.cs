using IBFramework.Core.Data;
using System.Data;

namespace IBFramework.Data.Common
{
    public abstract class BaseTranConnGenerator : ITranConnGenerator
    {
        public ITranConn GenerateTranConn(string connectionString, IsolationLevel isolation = IsolationLevel.ReadUncommitted)
        {
            IDbConnection conn = InternalGetDbConnection(connectionString);
            IDbTransaction tran = conn.BeginTransaction(isolation);
            return new TranConn(conn, tran);
        }

        protected abstract IDbConnection InternalGetDbConnection(string connectionString);
    }
}
