using System;
using System.Threading.Tasks;

namespace Ivy.Data.Core.Interfaces
{
    /*
     * This mechanism can't implement IInit functionality...
     * Attempted it; however, this needs to become transient due to potentially switching contexts.
     * Started running into issues where a connection string would be shared across contexts.
     * 
     * The problem lies in that we initialize this ITransactionHelper with different Connectionstrings in different contexts.
     * When we have any non-transient instance, this causes a race condition between initialization and execution.
     * By providing the connection string from the consuming environment, we should prevent context switching.
     * 
     * As long as the repositories don't switch connection strings, this implementation should work fine.
     */
    public interface ITransactionHelper
    {
        // Transactional Integrity Helpers
        void WrapInTransaction(Action<ITranConn> tranConnFn, string connectionString, ITranConn tc = null);

        T WrapInTransaction<T>(Func<ITranConn, T> tranConnFn, string connectionString, ITranConn tc = null);

        // Async Support
        Task WrapInTransactionAsync(Func<ITranConn, Task> tranConnFn, string connectionString, ITranConn tc = null);

        Task<T> WrapInTransactionAsync<T>(Func<ITranConn, Task<T>> tranConnFn, string connectionString, ITranConn tc = null);
    }
}
