using IBFramework.Core.Data.Init;
using System.Collections.Generic;

namespace IBFramework.Core.Data
{
    public interface IDatabaseKeyManager : IIsInitialized
    {
        string GetConnectionString(string databaseKey);

        void Init(Dictionary<string, string> databaseKeyDict);
    }
}
