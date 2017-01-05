using System;
using IBFramework.Core.Data;

namespace IBFramework.Data.MSSQL
{
    public class MsSqlTransactionHelper : ITransactionHelper
    {
        public string ConnectionString { get; private set; }
        private readonly ITranConnGenerator _tcGenerator;

        public MsSqlTransactionHelper(
            ITranConnGenerator tcGenerator)
        {
            _tcGenerator = tcGenerator;
        }

        public void InitializeByConnectionString(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public void WrapInTransaction(Action<ITranConn> tranConnFn, ITranConn tc = null)
        {
            bool myTran = false;

            if (tc == null)
            {
                tc = _tcGenerator.GenerateTranConn(ConnectionString);
                myTran = true;
            }

            try
            {
                tranConnFn(tc);
            }
            catch (Exception e)
            {
                if (myTran)
                {
                    tc.Transaction.Rollback();
                }

                throw;
            }
            finally
            {
                if (myTran)
                {
                    tc.Dispose();
                }
            }
        }

        public T WrapInTransaction<T>(Func<ITranConn, T> tranConnFn, ITranConn tc = null)
        {
            bool myTran = false;

            if (tc == null)
            {
                tc = _tcGenerator.GenerateTranConn(ConnectionString);
                myTran = true;
            }

            T result;

            try
            {
                result = tranConnFn(tc);
            }
            catch (Exception e)
            {
                if (myTran)
                {
                    tc.Transaction.Rollback();
                }

                throw;
            }
            finally
            {
                if (myTran)
                {
                    tc.Dispose();
                }
            }

            return result;
        }
    }
}
