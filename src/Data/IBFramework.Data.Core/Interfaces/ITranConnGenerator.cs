using System.Data;

namespace IBFramework.Data.Core.Interfaces
{
    public interface ITranConnGenerator
    {
        ITranConn GenerateTranConn(string connectionString, IsolationLevel isolation = IsolationLevel.ReadUncommitted);
    }
}
