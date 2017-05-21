using System;
using System.Data;

namespace IBFramework.Data.Core.Interfaces
{
    public interface ITranConn : IDisposable
    {
        IDbConnection Connection { get; }

        IDbTransaction Transaction { get; }
    }
}
