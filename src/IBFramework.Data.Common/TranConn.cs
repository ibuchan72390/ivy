using IBFramework.Core.Data;
using System.Data;
using System;

namespace IBFramework.Data.Common
{
    public class TranConn : ITranConn
    {
        #region Variables & Constants

        public IDbConnection Connection { get; private set; }

        public IDbTransaction Transaction { get; private set; }

        #endregion

        #region Constructor

        public TranConn(IDbConnection connection, IDbTransaction transaction)
        {
            Connection = connection;
            Transaction = transaction;
        }

        #endregion

        #region Public Methods

        public void Dispose()
        {
            Transaction.Dispose();
            Connection.Dispose();
        }

        #endregion
    }
}
