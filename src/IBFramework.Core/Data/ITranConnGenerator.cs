using System.Data;

namespace IBFramework.Core.Data
{
    public interface ITranConnGenerator
    {
        ITranConn GenerateTranConn(string connectionString, IsolationLevel isolation = IsolationLevel.ReadUncommitted);
    }
}
