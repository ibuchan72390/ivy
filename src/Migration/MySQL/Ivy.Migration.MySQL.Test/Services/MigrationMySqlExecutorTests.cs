using Ivy.Data.Common.IoC;
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

        private Mock<IMySqlScriptBuilder> _mockScriptBuilder;
        private Mock<IMySqlScriptExecutor> _mockScriptExecutor;

        private Mock<ITranConnGenerator> _mockTranConnGen;
        private Mock<ITranConn> _mockTranConn;

        #endregion

        #region SetUp & TearDown

        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            containerGen.InstallIvyCommonData();

            _mockScriptBuilder = InitializeMoq<IMySqlScriptBuilder>(containerGen);
            _mockScriptExecutor = InitializeMoq<IMySqlScriptExecutor>(containerGen);

            _mockTranConnGen = InitializeMoq<ITranConnGenerator>(containerGen);
            _mockTranConn = InitializeMoq<ITranConn>(containerGen);

            _mockTranConnGen
                .Setup(x => x.GenerateTranConn(It.IsAny<string>(), IsolationLevel.ReadUncommitted))
                .Returns(_mockTranConn.Object);
        }

        #endregion

        #region Tests

        [Fact]
        public void ExecuteSql_Executes_As_Expected()
        {
            // Arrange
            const string connString = "ConnectionString";
            const string script = "Script";

            var conn = new Mock<IDbConnection>();
            var tran = new Mock<IDbTransaction>();

            _mockTranConn.Setup(x => x.Connection).Returns(conn.Object);
            _mockTranConn.Setup(x => x.Transaction).Returns(tran.Object);
            _mockTranConn.Setup(x => x.Dispose());

            tran.Setup(x => x.Commit());

            var mockScript = new MySqlScript();

            _mockScriptBuilder
                .Setup(x => x.GenerateScript(_mockTranConn.Object, script))
                .Returns(mockScript);

            _mockScriptExecutor.Setup(x => x.Execute(mockScript));


            // Act
            Sut.ExecuteSql(script, connString);


            // Assert
            _mockScriptBuilder
                .Verify(x => x.GenerateScript(_mockTranConn.Object, script),
                    Times.Once);

            _mockScriptExecutor.Verify(x => x.Execute(mockScript), Times.Once);

            tran.Verify(x => x.Commit(), Times.Once);

            _mockTranConn.Verify(x => x.Dispose(), Times.Once);
        }

        #endregion
    }
}
