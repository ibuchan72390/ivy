using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ivy.Data.Core.Interfaces;
using Ivy.Data.Core.Interfaces.SQL;
using Ivy.IoC.Core;
using Ivy.Migration.Core.Interfaces.Providers;
using Ivy.Migration.Core.Interfaces.Services;
using Ivy.Migration.Providers;
using Ivy.Migration.Test.Base;
using Ivy.Utility.Core;
using Moq;
using Xunit;

namespace Ivy.Migration.Test.Services
{
    public class DatabaseMigrationServiceTests :
        MigrationTestBase<IDatabaseMigrationService>
    {
        #region Variables & Constants

        private Mock<IMigrationSqlExecutor> _mockExecutor;
        private Mock<IMigrationSqlGenerator> _mockSqlGen;
        private Mock<IRandomizationHelper> _mockRandomHelper;
        private Mock<IFileAccessor> _mockFileAccessor;
        private Mock<IDatabaseKeyManager> _mockKeyManager;

        private readonly IDatabaseMigrationConfigurationProvider _config =
            new DefaultDatabaseMigrationConfigurationProvider();

        private const string connString = "ConnectionString";

        #endregion

        #region SetUp & TearDown

        public DatabaseMigrationServiceTests()
        {
            Sut.InitializeByConnectionString(connString);
        }

        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            _mockExecutor = InitializeMoq<IMigrationSqlExecutor>(containerGen);
            _mockSqlGen = InitializeMoq<IMigrationSqlGenerator>(containerGen);
            _mockRandomHelper = InitializeMoq<IRandomizationHelper>(containerGen);
            _mockFileAccessor = InitializeMoq<IFileAccessor>(containerGen);
            _mockKeyManager = InitializeMoq<IDatabaseKeyManager>(containerGen);

            containerGen.RegisterSingleton<ITransactionHelper, TestTransactionHelper>();
        }

        #endregion

        #region Tests

        #region CreateUser

        [Fact]
        public void CreateUser_Executes_As_Expected()
        {
            const string sql = "SQL";

            _mockSqlGen
                .Setup(x => x.GenerateCreateUserSql(_config.LoginUserName, _config.LoginPassword))
                .Returns(sql);

            _mockExecutor
                .Setup(x => x.ExecuteSql(sql, connString));


            Sut.CreateUser();

            _mockSqlGen
                .Verify(x => x.GenerateCreateUserSql(_config.LoginUserName, _config.LoginPassword),
                    Times.Once);

            _mockExecutor
                .Verify(x => x.ExecuteSql(sql, connString),
                    Times.Once);
        }

        #endregion

        #region CreateDatabase

        [Fact]
        public void CreateDatabase_Returns_Immediately_If_No_Scripts()
        {
            const string dbName = "TestDb";
            const string path = "C:\\Test";

            _mockFileAccessor
                .Setup(x => x.GetDirectoryFiles(path))
                .Returns(new List<string>());

            var e = Assert.Throws<Exception>(() => Sut.CreateDatabase(dbName, path, null));

            var err = $"Target folder does not have any migration scripts! Folder: {path}";

            Assert.Equal(err, e.Message);
        }

        [Fact]
        public void CreateDatabase_Executes_As_Expected_Without_Random_Key()
        {
            // Arrange
            const string dbName = "TestDb";
            const string path = "C:\\Test";

            var fileNames = Enumerable.Range(0, 3)
                .Select(x => $"{path}\\Test{x + 1}.txt")
                .ToList();

            _mockFileAccessor
                .Setup(x => x.GetDirectoryFiles(path))
                .Returns(fileNames);

            const string createDbSql = "Create DB";
            _mockSqlGen
                .Setup(x => x.GenerateCreateDatabaseSql(dbName))
                .Returns(createDbSql);

            _mockExecutor.Setup(x => x.ExecuteSql(createDbSql, connString));

            const string grantUserSql = "Grant User";
            _mockSqlGen
                .Setup(x => x.GenerateGrantPrivelegesSql(_config.LoginUserName, dbName))
                .Returns(grantUserSql);

            _mockExecutor.Setup(x => x.ExecuteSql(grantUserSql, connString));

            const string resultConn = "ResultConnectionString";
            _mockSqlGen
                .Setup(x => x.GenerateDbConnectionString(dbName, _config.LoginUserName, _config.LoginPassword))
                .Returns(resultConn);

            const string scriptText = "Script";
            fileNames.Select(x => _mockFileAccessor.Setup(y => y.GetFileText(x)).Returns(scriptText)).ToList();
            _mockExecutor.Setup(x => x.ExecuteSql(scriptText, resultConn));


            // Act
            var result = Sut.CreateDatabase(dbName, path, null);


            // Assert
            Assert.Equal(resultConn, result);

            _mockFileAccessor
                .Verify(x => x.GetDirectoryFiles(path), Times.Once);

            _mockSqlGen
                .Verify(x => x.GenerateCreateDatabaseSql(dbName), Times.Once);

            _mockExecutor.Verify(x => x.ExecuteSql(createDbSql, connString), Times.Once);

            _mockSqlGen
                .Verify(x => x.GenerateGrantPrivelegesSql(_config.LoginUserName, dbName), Times.Once);

            _mockExecutor.Verify(x => x.ExecuteSql(grantUserSql, connString), Times.Once);

            foreach (var file in fileNames)
            {
                _mockFileAccessor.Verify(y => y.GetFileText(file), Times.Once);
            }

            _mockExecutor.Verify(x => x.ExecuteSql(scriptText, resultConn), Times.Exactly(3));

            _mockSqlGen
                .Verify(x => x.GenerateDbConnectionString(dbName, _config.LoginUserName, _config.LoginPassword),
                    Times.Once);
        }

        [Fact]
        public void CreateDatabase_Executes_As_Expected_With_Sorting_Functionality()
        {
            // Arrange
            const string dbName = "TestDb";
            const string path = "C:\\Test";
            const string randKey = "RANDOM";

            _mockRandomHelper.Setup(x => x.RandomString(10)).Returns(randKey);

            var fileNames = Enumerable.Range(0, 3)
                .Select(x => $"{path}\\Test{x + 1}.txt")
                .ToList();

            _mockFileAccessor
                .Setup(x => x.GetDirectoryFiles(path))
                .Returns(fileNames);

            const string createDbSql = "Create DB";
            _mockSqlGen
                .Setup(x => x.GenerateCreateDatabaseSql(dbName))
                .Returns(createDbSql);

            _mockExecutor.Setup(x => x.ExecuteSql(createDbSql, connString));

            const string grantUserSql = "Grant User";
            _mockSqlGen
                .Setup(x => x.GenerateGrantPrivelegesSql(_config.LoginUserName, dbName))
                .Returns(grantUserSql);
            _mockExecutor.Setup(x => x.ExecuteSql(grantUserSql, connString));

            const string resultConn = "ResultConnectionString";
            _mockSqlGen
                .Setup(x => x.GenerateDbConnectionString(dbName, _config.LoginUserName, _config.LoginPassword))
                .Returns(resultConn);

            const string scriptText = "Script";
            fileNames.Select(x => _mockFileAccessor.Setup(y => y.GetFileText(x)).Returns(scriptText)).ToList();
            _mockExecutor.Setup(x => x.ExecuteSql(scriptText, resultConn));

            var comparer = new TestComparer();


            // Act
            var result = Sut.CreateDatabase(dbName, path, comparer);


            // Assert
            Assert.Equal(resultConn, result);

            _mockFileAccessor
                .Verify(x => x.GetDirectoryFiles(path), Times.Once);

            _mockSqlGen
                .Verify(x => x.GenerateCreateDatabaseSql(dbName), Times.Once);

            _mockExecutor.Verify(x => x.ExecuteSql(createDbSql, connString), Times.Once);

            _mockSqlGen
                .Verify(x => x.GenerateGrantPrivelegesSql(_config.LoginUserName, dbName), Times.Once);

            _mockExecutor.Verify(x => x.ExecuteSql(grantUserSql, connString), Times.Once);

            _mockSqlGen
                .Verify(x => x.GenerateDbConnectionString(dbName, _config.LoginUserName, _config.LoginPassword),
                    Times.Once);

            foreach (var file in fileNames)
            {
                _mockFileAccessor.Verify(y => y.GetFileText(file), Times.Once);
            }

            _mockExecutor.Verify(x => x.ExecuteSql(scriptText, resultConn), Times.Exactly(3));

            Assert.True(comparer.Executed);
        }

        #endregion

        #region CleanDatabase

        [Fact]
        public void CleanDatabase_Executes_As_Expected()
        {
            const string dbName = "DBName";
            const string sql = "SQL";

            _mockSqlGen
                .Setup(x => x.GenerateDeleteDatabaseSql(dbName))
                .Returns(sql);

            _mockExecutor
                .Setup(x => x.ExecuteSql(sql, connString));


            Sut.CleanDatabase(dbName);

            _mockSqlGen
                .Verify(x => x.GenerateDeleteDatabaseSql(dbName),
                    Times.Once);

            _mockExecutor
                .Verify(x => x.ExecuteSql(sql, connString),
                    Times.Once);
        }

        #endregion

        #region CleanUser

        [Fact]
        public void CleanUser_Executes_As_Expected()
        {
            const string sql = "SQL";

            _mockSqlGen
                .Setup(x => x.GenerateDeleteUserSql(_config.LoginUserName))
                .Returns(sql);

            _mockExecutor
                .Setup(x => x.ExecuteSql(sql, connString));


            Sut.CleanUser();

            _mockSqlGen
                .Verify(x => x.GenerateDeleteUserSql(_config.LoginUserName),
                    Times.Once);

            _mockExecutor
                .Verify(x => x.ExecuteSql(sql, connString),
                    Times.Once);
        }

        #endregion

        #endregion
    }

    /// <summary>
    /// I don't want to add a dependency on a given integration provider
    /// That would force this test context to have a dependency on a given SQL provider.
    /// This will allow us to properly test with the TransactionHelper without a SQL dependency.
    /// </summary>
    public class TestTransactionHelper : ITransactionHelper
    {
        public string ConnectionString { get; private set; }

        public void InitializeByConnectionString(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public void InitializeByDatabaseKey(string databaseKey)
        {
            throw new NotImplementedException();
        }

        public void WrapInTransaction(Action<ITranConn> tranConnFn, string connectionString, ITranConn tc = null)
        {
            tranConnFn(tc);
        }

        public T WrapInTransaction<T>(Func<ITranConn, T> tranConnFn, string connectionString, ITranConn tc = null)
        {
            return tranConnFn(tc);
        }

        public async Task WrapInTransactionAsync(Func<ITranConn, Task> tranConnFn, string connectionString, ITranConn tc = null)
        {
            await tranConnFn(tc);
        }

        public async Task<T> WrapInTransactionAsync<T>(Func<ITranConn, Task<T>> tranConnFn, string connectionString, ITranConn tc = null)
        {
            return await tranConnFn(tc);
        }
    }

    public class TestComparer : IComparer<string>
    {
        public bool Executed { get; set; }

        public int Compare(string x, string y)
        {
            Executed = true;

            return 0;
        }
    }
}
