using IBFramework.Core.Data;
using IBFramework.IoC;
using IBFramework.TestHelper.TestValues;
using Xunit;

namespace IBFramework.Data.MySQL.Test
{
    public class MySqlTranConnGeneratorTests : MySqlTestBase
    {
        ITranConnGenerator _sut;

        public MySqlTranConnGeneratorTests()
        {
            _sut = ServiceLocator.Instance.Resolve<ITranConnGenerator>();
        }

        [Fact]
        public void TranConnGenerator_Generates_MySql_Connection_With_Open_Transaction()
        {
            var tc = _sut.GenerateTranConn(MySqlTestValues.TestDbConnectionString);

            Assert.NotNull(tc.Connection);
            Assert.NotNull(tc.Transaction);
            Assert.True(tc.Connection.State == System.Data.ConnectionState.Open);
            Assert.True(tc.Transaction.IsolationLevel == System.Data.IsolationLevel.ReadUncommitted);
        }

        [Fact]
        public void TranConnGenerator_Adjusts_Transaction_To_Provided_Type()
        {
            System.Data.IsolationLevel targetType = System.Data.IsolationLevel.ReadCommitted;

            var tc = _sut.GenerateTranConn(MySqlTestValues.TestDbConnectionString, targetType);

            Assert.NotNull(tc.Connection);
            Assert.NotNull(tc.Transaction);
            Assert.True(tc.Connection.State == System.Data.ConnectionState.Open);
            Assert.True(tc.Transaction.IsolationLevel == targetType);
        }
    }
}
