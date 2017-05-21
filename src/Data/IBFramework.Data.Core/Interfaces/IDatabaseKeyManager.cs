using IBFramework.Data.Core.Interfaces.Init;
using System.Collections.Generic;

namespace IBFramework.Data.Core.Interfaces
{
    public interface IDatabaseKeyManager : IIsInitialized
    {
        string GetConnectionString(string databaseKey);

        void Init(Dictionary<string, string> databaseKeyDict);
    }
}
