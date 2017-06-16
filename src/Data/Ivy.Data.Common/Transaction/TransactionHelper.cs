using Ivy.Data.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace Ivy.Data.Common.Transaction
{
    public class TransactionHelper : ITransactionHelper
    {
        #region Variables & Constants

        public string ConnectionString { get; private set; }

        private readonly ITranConnGenerator _tcGenerator;
        private readonly IDatabaseKeyManager _dbKeyManager;

        #endregion

        #region Constructor

        public TransactionHelper(
            ITranConnGenerator tcGenerator,
            IDatabaseKeyManager dbKeyManager)
        {
            _tcGenerator = tcGenerator;
            _dbKeyManager = dbKeyManager;
        }

        #endregion

        #region Initialization

        public void InitializeByConnectionString(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public void InitializeByDatabaseKey(string databaseKey)
        {
            ConnectionString = _dbKeyManager.GetConnectionString(databaseKey);
        }

        #endregion

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

        public async Task WrapInTransactionAsync(Func<ITranConn, Task> tranConnFn, ITranConn tc = null)
        {
            bool myTran = false;

            if (tc == null)
            {
                tc = _tcGenerator.GenerateTranConn(ConnectionString);
                myTran = true;
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

        public async Task<T> WrapInTransactionAsync<T>(Func<ITranConn, Task<T>> tranConnFn, ITranConn tc = null)
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
    }
}
