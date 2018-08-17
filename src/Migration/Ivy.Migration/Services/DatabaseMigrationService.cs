using Ivy.Data.Core.Interfaces;
using Ivy.Data.Core.Interfaces.SQL;
using Ivy.Migration.Core.Interfaces.Providers;
using Ivy.Migration.Core.Interfaces.Services;
using Ivy.Utility.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ivy.Migration.Services
{
    public class DatabaseMigrationService :
        IDatabaseMigrationService
    {
        #region Variables

        private readonly IMigrationSqlExecutor _sqlExecutor;
        private readonly IMigrationSqlGenerator _sqlGenerator;
        private readonly IDatabaseMigrationConfigurationProvider _config;
        private readonly IDatabaseKeyManager _dbKeyMgr;
        private readonly IFileAccessor _fileAccessor;

        private readonly ITransactionHelper _tranHelper;

        private readonly IRandomizationHelper _rand;

        private string InitializedConnectionString;

        #endregion

        #region Constructor

        public DatabaseMigrationService(
            IMigrationSqlExecutor sqlExecutor,
            IMigrationSqlGenerator sqlGenerator,
            IDatabaseMigrationConfigurationProvider config,
            IDatabaseKeyManager dbKeyMgr,
            IFileAccessor fileAccessor,
            ITransactionHelper tranHelper,
            IRandomizationHelper rand)
        {
            _sqlExecutor = sqlExecutor;
            _sqlGenerator = sqlGenerator;
            _config = config;
            _dbKeyMgr = dbKeyMgr;
            _fileAccessor = fileAccessor;

            _tranHelper = tranHelper;

            _rand = rand;
        }

        public void InitializeByConnectionString(string connectionString)
        {
            InitializedConnectionString = connectionString;
        }

        public void InitializeByDatabaseKey(string databaseKey)
        {
            InitializedConnectionString = _dbKeyMgr.GetConnectionString(databaseKey);
        }

        #endregion

        #region Public Methods

        public void CreateUser()
        {
            var createUserSql = _sqlGenerator.GenerateCreateUserSql(_config.LoginUserName, _config.LoginPassword);

            _sqlExecutor.ExecuteSql(createUserSql, InitializedConnectionString);
        }


        /*
         * This is a strange method that is completely unable to use a Transactional basis.
         * Essentially, we have to initialize 2 separate transactions:
         * 1) Create Database Transaction (No DB Target)
         * 2) Schema Migration Transaction (DB Target)
         */
        public string CreateDatabase(string dbName, string absoluteFilePath, 
            IComparer<string> fileSort = null)
        {
            var scripts = _fileAccessor.GetDirectoryFiles(absoluteFilePath).ToList();

            if (!scripts.Any())
            {
                throw new Exception($"Target folder does not have any migration scripts! Folder: {absoluteFilePath}");
            }

            // Optional override in case their standard script order is not viable
            if (fileSort != null)
            {
                scripts.Sort(fileSort);
            }
            else
            {
                // This should ensure alphabetical order
                // Apparently this is a huge issue going Windows -> Linux
                scripts = scripts.OrderBy(x => x).ToList();
            }

            // Create our DB
            var createDbSql = _sqlGenerator.GenerateCreateDatabaseSql(dbName);
            _sqlExecutor.ExecuteSql(createDbSql, InitializedConnectionString);

            // Grant Test User Priveleges to this Db
            var grantPrivelegesSql = _sqlGenerator.GenerateGrantPrivelegesSql(_config.LoginUserName, dbName);
            _sqlExecutor.ExecuteSql(grantPrivelegesSql, InitializedConnectionString);

            string prevString = InitializedConnectionString;
            var connString = _sqlGenerator.GenerateDbConnectionString(dbName, _config.LoginUserName, _config.LoginPassword);
            InitializeByConnectionString(connString);

            // Execute Scripts Accordingly
            foreach (var script in scripts)
            {
                var sql = _fileAccessor.GetFileText(script);
                _sqlExecutor.ExecuteSql(sql, connString);
            }

            // Reset the original string back to working
            InitializeByConnectionString(prevString);

            return connString;
        }

        public void CleanDatabase(string dbName)
        {
            var sql = _sqlGenerator.GenerateDeleteDatabaseSql(dbName);

            _sqlExecutor.ExecuteSql(sql, InitializedConnectionString);
        }

        public void CleanUser()
        {
            var sql = _sqlGenerator.GenerateDeleteUserSql(_config.LoginUserName);

            _sqlExecutor.ExecuteSql(sql, InitializedConnectionString);
        }

        #endregion
    }
}
