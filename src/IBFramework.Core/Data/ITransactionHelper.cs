using System;

namespace IBFramework.Core.Data
{
    public interface ITransactionHelper : IInitializeByConnectionString
    {
        void WrapInTransaction(Action<ITranConn> tranConnFn, ITranConn tc = null);

        T WrapInTransaction<T>(Func<ITranConn, T> tranConnFn, ITranConn tc = null);
    }
}
