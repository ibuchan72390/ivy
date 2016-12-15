using System;
using System.Collections.Generic;
using IBFramework.Core.Data;

namespace IBFramework.Data.Common
{
    public class DatabaseKeyManager : IDatabaseKeyManager
    {
        private Dictionary<string, string> _databaseKeyDict;

        public string GetConnectionString(string databaseKey)
        {
            if (_databaseKeyDict == null)
            {
                throw new Exception("Database Key Dictionary has not been loaded!  Make sure you've initialize the DatabaseKeyManager!");
            }

            if (!_databaseKeyDict.ContainsKey(databaseKey))
            {
                throw new Exception($"No database connection string found for key - '{databaseKey}'");
            }

            return _databaseKeyDict[databaseKey];
        }

        public void Init(Dictionary<string, string> databaseKeyDict)
        {
            if (_databaseKeyDict != null)
            {
                throw new Exception("DatabaseKeyManager already initialized!");
            }

            _databaseKeyDict = databaseKeyDict;
        }
    }
}
