using System;
using System.Data;

namespace IBFramework.Core.Data
{
    public interface ITranConn : IDisposable
    {
        IDbConnection Connection { get; }

        IDbTransaction Transaction { get; }
    }
}
