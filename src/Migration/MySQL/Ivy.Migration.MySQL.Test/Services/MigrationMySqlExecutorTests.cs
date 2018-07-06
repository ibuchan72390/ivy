using Ivy.Data.Core.Interfaces;
using Ivy.IoC.Core;
using Ivy.Migration.Core.Interfaces.Services;
using Ivy.Migration.MySQL.Core.Providers;
using Ivy.Migration.MySQL.Core.Services;
using Ivy.Migration.MySQL.Providers;
using Ivy.Migration.MySQL.Test.Base;
using Moq;
using MySql.Data.MySqlClient;
using System.Data;
using Xunit;

namespace Ivy.Migration.MySQL.Test.Services
{
    public class MigrationMySqlExecutorTests :
        MySqlMigrationTestBase<IMigrationSqlExecutor>
    {
        #region Variables & Constants

        private Mock<ITranConnGenerator> _mockTcGen;
        private Mock<IMySqlScriptBuilder> _mockScriptBuilder;
        private Mock<IMySqlScriptExecutor> _mockScriptExecutor;

        #endregion

        #region SetUp & TearDown

        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            _mockTcGen = InitializeMoq<ITranConnGenerator>(containerGen);
            _mockScriptBuilder = InitializeMoq<IMySqlScriptBuilder>(containerGen);
            _mockScriptExecutor = InitializeMoq<IMySqlScriptExecutor>(containerGen);
        }

        #endregion

        #region Tests

        [Fact]
        public void ExecuteSql_Executes_As_Expected()
        {
            // Arrange
            const string connString = "ConnectionString";
            const string script = "Script";

            var tc = new Mock<ITranConn>();
            var conn = new Mock<IDbConnection>();
            tc.Setup(x => x.Connection).Returns(conn.Object);

            conn.Setup(x => x.Open());
            conn.Setup(x => x.Close());

            var mockScript = new MySqlScript();

            _mockTcGen
                .Setup(x => x.GenerateTranConn(connString, IsolationLevel.ReadUncommitted))
                .Returns(tc.Object);

            _mockScriptBuilder
                .Setup(x => x.GenerateScript(tc.Object, script))
                .Returns(mockScript);

            _mockScriptExecutor.Setup(x => x.Execute(mockScript));


            // Act
            Sut.ExecuteSql(script, connString);


            // Assert
            _mockTcGen
                .Verify(x => x.GenerateTranConn(connString, IsolationLevel.ReadUncommitted),
                    Times.Once);

            conn.Verify(x => x.Open(), Times.Once);

            _mockScriptBuilder
                .Verify(x => x.GenerateScript(tc.Object, script),
                    Times.Once);

            _mockScriptExecutor.Verify(x => x.Execute(mockScript), Times.Once);

            conn.Verify(x => x.Close(), Times.Once);
        }

        #endregion
    }
}
