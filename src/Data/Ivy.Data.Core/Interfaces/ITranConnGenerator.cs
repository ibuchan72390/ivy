using System.Data;
using System.Threading.Tasks;

namespace Ivy.Data.Core.Interfaces
{
    public interface ITranConnGenerator
    {
        ITranConn GenerateTranConn(string connectionString, IsolationLevel isolation = IsolationLevel.ReadUncommitted);

        Task<ITranConn> GenerateTranConnAsync(string connectionString, IsolationLevel isolation = IsolationLevel.ReadUncommitted);
    }
}
