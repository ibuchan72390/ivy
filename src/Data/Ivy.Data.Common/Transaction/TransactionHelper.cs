using Ivy.Data.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace Ivy.Data.Common.Transaction
{
    public class TransactionHelper : ITransactionHelper
    {
        #region Variables & Constants

        private readonly ITranConnGenerator _tcGenerator;

        #endregion

        #region Constructor

        public TransactionHelper(
            ITranConnGenerator tcGenerator)
        {
            _tcGenerator = tcGenerator;
        }

        #endregion

        #region Public Methods

        public void WrapInTransaction(Action<ITranConn> tranConnFn, string connectionString, ITranConn tc = null)
        {
            bool myTran = tc == null;

            if (myTran)
            {
                tc = _tcGenerator.GenerateTranConn(connectionString);
            }

            try
            {
                tranConnFn(tc);

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
        }

        public T WrapInTransaction<T>(Func<ITranConn, T> tranConnFn, string connectionString, ITranConn tc = null)
        {
            bool myTran = tc == null;

            if (myTran)
            {
                tc = _tcGenerator.GenerateTranConn(connectionString);
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

        public async Task WrapInTransactionAsync(Func<ITranConn, Task> tranConnFn, string connectionString, ITranConn tc = null)
        {
            bool myTran = tc == null;

            if (myTran)
            {
                tc = await _tcGenerator.GenerateTranConnAsync(connectionString);
            }

            try
            {
                await tranConnFn(tc);

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
        }

        public async Task<T> WrapInTransactionAsync<T>(Func<ITranConn, Task<T>> tranConnFn, string connectionString, ITranConn tc = null)
        {
            bool myTran = tc == null;

            if (myTran)
            {
                tc = await _tcGenerator.GenerateTranConnAsync(connectionString);
            }

            T result;

            try
            {
                result = await tranConnFn(tc);

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

        #endregion

        #region Helper Methods

        /*
         * If we make a transaction from a connection string, then myTran == true
         * If we're simply passing a transaction that we received, then myTran == false
         */

        private void InternalWrapInTransaction(Action<ITranConn> tranConnFn, ITranConn tc, bool myTran)
        {
            try
            {
                tranConnFn(tc);

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
        }

        private T InternalWrapInTransaction<T>(Func<ITranConn, T> tranConnFn, ITranConn tc, bool myTran)
        {
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

        private async Task InternalWrapInTransactionAsync(Func<ITranConn, Task> tranConnFn, ITranConn tc, bool myTran)
        {
            try
            {
                await tranConnFn(tc);

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
        }

        private async Task<T> InternalWrapInTransactionAsync<T>(Func<ITranConn, Task<T>> tranConnFn, ITranConn tc, bool myTran)
        {
            T result;

            try
            {
                result = await tranConnFn(tc);

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

        #endregion
    }
}
