using Ivy.Migration.Core.Interfaces.Services;
using Ivy.Migration.MySQL.Test.Base;
using Xunit;

namespace Ivy.Migration.MySQL.Test.Services
{
    public class MigrationMySqlGeneratorTests :
        MySqlMigrationTestBase<IMigrationSqlGenerator>
    {
        #region Variables & Constants

        const string dbName = "DBName";
        const string userName = "UserName";
        const string userPass = "UserPass";

        #endregion

        #region Tests

        #region GenerateCreateDatabaseSql

        [Fact]
        public void GenerateCreateDatabaseSql_Executes_As_Expected()
        {
            var sql = Sut.GenerateCreateDatabaseSql(dbName);

            var expected = $"CREATE DATABASE {dbName};";

            Assert.Equal(sql, expected);
        }

        #endregion

        #region GenerateCreateUserSql

        [Fact]
        public void GenerateCreateUserSql_Executes_As_Expected()
        {
            var sql = Sut.GenerateCreateUserSql(userName, userPass);

            var expected = $"CREATE USER '{userName}'@'localhost' IDENTIFIED BY '{userPass}';";

            Assert.Equal(sql, expected);
        }

        #endregion

        #region GenerateDbConnectionString

        [Fact]
        public void GenerateDbConnectionString_Returns_As_Expected()
        {
            var sql = Sut.GenerateDbConnectionString(dbName, userName, userPass);

            var expected = $"Data Source=localhost;Initial Catalog={dbName};Uid={userName};Pwd={userPass};SslMode=none;";

            Assert.Equal(sql, expected);
        }

        #endregion

        #region GenerateDeleteDatabaseSql

        [Fact]
        public void GenerateDeleteDatabaseSql_Executes_As_Expected()
        {
            var sql = Sut.GenerateDeleteDatabaseSql(dbName);

            var expected = $"DROP DATABASE {dbName};";

            Assert.Equal(sql, expected);
        }

        #endregion

        #region GenerateDeleteUserSql

        [Fact]
        public void GenerateDeleteUserSql_Executes_As_Expected()
        {
            var sql = Sut.GenerateDeleteUserSql(userName);

            var expected = $"DROP USER `{userName}`@`localhost`;";

            Assert.Equal(sql, expected);
        }

        #endregion

        #region GenerateGrantPrivelegesSql

        [Fact]
        public void GenerateGrantPrivelegesSql_Executes_As_Expected()
        {
            var sql = Sut.GenerateGrantPrivelegesSql(userName, dbName);

            var expected = $"GRANT ALL PRIVILEGES ON `{dbName}`. * TO '{userName}'@'localhost';";

            Assert.Equal(sql, expected);
        }

        #endregion

        #endregion
    }
}
