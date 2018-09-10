using Ivy.Data.Common.IoC;
using Ivy.IoC.Core;
using Ivy.Migration.Core.Interfaces.Services;
using Ivy.Migration.MySQL.Core.Services;
using Ivy.Migration.MySQL.Test.Base;
using Ivy.TestHelper.TestValues;
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

        #endregion

        #region SetUp & TearDown

        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            containerGen.InstallIvyCommonData();

            _mockScriptBuilder = InitializeMoq<IMySqlScriptBuilder>(containerGen);
            _mockScriptExecutor = InitializeMoq<IMySqlScriptExecutor>(containerGen);
        }

        #endregion

        #region Tests

        [Fact]
        public void ExecuteSql_Executes_As_Expected()
        {
            // Arrange
            const string connString = MySqlTestValues.TestDataDbConnectionString;
            const string script = "Script";

            var conn = new Mock<IDbConnection>();

            var mockScript = new MySqlScript();

            _mockScriptBuilder
                //.Setup(x => x.GenerateScript(It.Is<MySqlConnection>(y => y.State == ConnectionState.Open), script))
                .Setup(x => x.GenerateScript(It.IsAny<MySqlConnection>(), script))
                .Returns(mockScript);

            _mockScriptExecutor.Setup(x => x.Execute(mockScript));


            // Act
            Sut.ExecuteSql(script, connString);


            // Assert
            _mockScriptBuilder
                //.Verify(x => x.GenerateScript(It.Is<MySqlConnection>(y => y.State == ConnectionState.Open), script),
                .Verify(x => x.GenerateScript(It.IsAny<MySqlConnection>(), script),
                    Times.Once);

            _mockScriptExecutor.Verify(x => x.Execute(mockScript), Times.Once);
        }

        #endregion
    }
}
