using Ivy.Data.Core.Interfaces;
using System;

namespace Ivy.Data.Common.Transaction
{
    public class TransactionHelper : ITransactionHelper
    {
        public string ConnectionString { get; private set; }
        private readonly ITranConnGenerator _tcGenerator;

        public TransactionHelper(
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
            catch (Exception)
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

                if (myTran)
                {
                    tc.Transaction.Commit();
                }
            }
            catch (Exception)
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
