using System.Collections.Generic;

namespace IBFramework.Core.Data
{
    public interface IDatabaseKeyManager
    {
        string GetConnectionString(string databaseKey);

        void Init(Dictionary<string, string> databaseKeyDict);
    }
}
