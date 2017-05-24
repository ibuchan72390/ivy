﻿using Ivy.Data.Core.Interfaces.Init;
using System;

namespace Ivy.Data.Core.Interfaces
{
    public interface ITransactionHelper : IInitializeByConnectionString
    {
        void WrapInTransaction(Action<ITranConn> tranConnFn, ITranConn tc = null);

        T WrapInTransaction<T>(Func<ITranConn, T> tranConnFn, ITranConn tc = null);
    }
}
