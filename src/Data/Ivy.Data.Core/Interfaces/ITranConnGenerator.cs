using System.Data;

namespace Ivy.Data.Core.Interfaces
{
    public interface ITranConnGenerator
    {
        ITranConn GenerateTranConn(string connectionString, IsolationLevel isolation = IsolationLevel.ReadUncommitted);
    }
}
