using Ivy.Data.Core.Interfaces;
using Ivy.TestHelper.TestValues;
using System;
using Xunit;

namespace Ivy.Data.MySQL.Test
{
    public class MySqlTranConnGeneratorTests : 
        MySqlTestBase<ITranConnGenerator>
    {
        [Fact]
        public void TranConnGenerator_Throws_Initialized_Exception_If_Connection_String_Not_Set()
        {
            var e = Assert.Throws<Exception>(() => Sut.GenerateTranConn(null));

            Assert.Equal("Your service has not been initialized! The connection is going to fail!", e.Message);
        }

        [Fact]
        public void TranConnGenerator_Generates_MySql_Connection_With_Open_Transaction()
        {
            var tc = Sut.GenerateTranConn(MySqlTestValues.TestDbConnectionString);

            Assert.NotNull(tc.Connection);
            Assert.NotNull(tc.Transaction);
            Assert.True(tc.Connection.State == System.Data.ConnectionState.Open);
            Assert.True(tc.Transaction.IsolationLevel == System.Data.IsolationLevel.ReadUncommitted);
        }

        [Fact]
        public void TranConnGenerator_Adjusts_Transaction_To_Provided_Type()
        {
            System.Data.IsolationLevel targetType = System.Data.IsolationLevel.ReadCommitted;

            var tc = Sut.GenerateTranConn(MySqlTestValues.TestDbConnectionString, targetType);

            Assert.NotNull(tc.Connection);
            Assert.NotNull(tc.Transaction);
            Assert.True(tc.Connection.State == System.Data.ConnectionState.Open);
            Assert.True(tc.Transaction.IsolationLevel == targetType);
        }
    }
}
