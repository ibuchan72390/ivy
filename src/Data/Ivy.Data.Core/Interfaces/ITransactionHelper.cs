using Ivy.Data.Core.Interfaces.Init;
using System;
using System.Threading.Tasks;

namespace Ivy.Data.Core.Interfaces
{
    public interface ITransactionHelper : IInitialize
    {
        // Transactional Integrity Helpers
        void WrapInTransaction(Action<ITranConn> tranConnFn, ITranConn tc = null);

        T WrapInTransaction<T>(Func<ITranConn, T> tranConnFn, ITranConn tc = null);

        // Async Support
        Task WrapInTransactionAsync(Func<ITranConn, Task> tranConnFn, ITranConn tc = null);

        Task<T> WrapInTransactionAsync<T>(Func<ITranConn, Task<T>> tranConnFn, ITranConn tc = null);
    }
}
