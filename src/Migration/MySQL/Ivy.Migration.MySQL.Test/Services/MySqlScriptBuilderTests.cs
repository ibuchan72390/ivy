using Ivy.Data.Core.Interfaces;
using Ivy.IoC.Core;
using Ivy.Migration.MySQL.Core.Providers;
using Ivy.Migration.MySQL.Core.Services;
using Ivy.Migration.MySQL.Test.Base;
using Moq;
using MySql.Data.MySqlClient;
using System.Data;
using Xunit;

namespace Ivy.Migration.MySQL.Test.Services
{
    public class MySqlScriptBuilderTests :
        MySqlMigrationTestBase<IMySqlScriptBuilder>
    {
        #region Variables & Constants

        private Mock<IMySqlMigrationConfigurationProvider> _mockConfig;

        #endregion

        #region SetUp & TearDown

        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            _mockConfig = InitializeMoq<IMySqlMigrationConfigurationProvider>(containerGen);
        }

        #endregion

        #region Tests

        #region GenerateScript

        [Fact]
        public void GenerateScript_Works_As_Expected_With_Delimiter_Config()
        {
            const string delimiter = "$$";
            const string script = "SCRIPT";

            _mockConfig.Setup(x => x.Delimiter).Returns(delimiter);

            var tcMock = new Mock<ITranConn>();
            IDbConnection mockConn = new MySqlConnection();
            tcMock.Setup(x => x.Connection).Returns(mockConn);

            var result = Sut.GenerateScript(tcMock.Object, script);

            Assert.Equal(delimiter, result.Delimiter);
            Assert.Equal(script, result.Query);
            Assert.Same(mockConn, result.Connection);
        }

        [Fact]
        public void GenerateScript_Works_As_Expected_Without_Delimiter_Config()
        {
            const string delimiter = null;
            const string script = "SCRIPT";

            _mockConfig.Setup(x => x.Delimiter).Returns(delimiter);

            var tcMock = new Mock<ITranConn>();
            IDbConnection mockConn = new MySqlConnection();
            tcMock.Setup(x => x.Connection).Returns(mockConn);

            var result = Sut.GenerateScript(tcMock.Object, script);

            Assert.Equal(";", result.Delimiter); // Seems to be the default
            Assert.Equal(script, result.Query);
            Assert.Same(mockConn, result.Connection);
        }

        #endregion

        #endregion
    }
}
