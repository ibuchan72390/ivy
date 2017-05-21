using IBFramework.Data.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace IBFramework.Data.Common
{
    public class DatabaseKeyManager : IDatabaseKeyManager
    {
        #region Variables & Constants

        private Dictionary<string, string> _databaseKeyDict = null;

        public bool IsInitialized => _databaseKeyDict != null;

        #endregion

        #region Public Methods

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

        #endregion
    }
}
