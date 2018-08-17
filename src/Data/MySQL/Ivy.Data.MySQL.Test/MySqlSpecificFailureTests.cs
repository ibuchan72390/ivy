using Ivy.Data.Core.Domain;
using Ivy.Data.Core.Interfaces.SQL;
using System.Collections.Generic;
using Xunit;

namespace Ivy.Data.MySQL.Test
{
    public class MySqlSpecificFailureTests : 
        MySqlTestBase<ISqlGenerator<MySqlSpecificFailureTests.DatabaseKey, int>>
    {
        #region Database Key

        public class DatabaseKey : Entity
        {
            public string Key { get; set; }

            public string ConnectionString { get; set; }
        }

        [Fact]
        public void DatabaseKey_Replace_Query_Formats_As_Expected()
        {
            var dbKey = new DatabaseKey { Key = "Test Key", ConnectionString = "Test ConnString" };

            var parms = new Dictionary<string, object>();

            var result = Sut.GenerateSaveOrUpdateQuery(dbKey, parms);

            const string expected = "INSERT INTO databasekey (`Key`, `ConnectionString`) VALUES (@Key0, @ConnectionString0);SELECT LAST_INSERT_ID();";

            Assert.Equal(expected, result.Sql);
        }

        #endregion

    }
}
