using Ivy.Data.Core.Interfaces.Init;
using System.Collections.Generic;

namespace Ivy.Data.Core.Interfaces
{
    public interface IDatabaseKeyManager : IIsInitialized
    {
        string GetConnectionString(string databaseKey);

        void Init(Dictionary<string, string> databaseKeyDict);
    }
}
