using Ivy.Migration.Core.Interfaces.Services;

namespace Ivy.Migration.MySQL.Services
{
    public class MigrationMySqlGenerator : IMigrationSqlGenerator
    {
        public string GenerateCreateDatabaseSql(string dbName)
        {
            return $"CREATE DATABASE IF NOT EXISTS {dbName};";
        }

        public string GenerateCreateUserSql(string loginUserName, string loginPassword)
        {
            return $"CREATE USER IF NOT EXISTS '{loginUserName}'@'localhost' IDENTIFIED BY '{loginPassword}';";
        }

        public string GenerateDbConnectionString(string dbName, string userName, string userPass)
        {
            return $"Data Source=localhost;Initial Catalog={dbName};Uid={userName};Pwd={userPass};SslMode=none;";
        }

        public string GenerateDeleteDatabaseSql(string dbName)
        {
            return $"DROP DATABASE IF EXISTS {dbName};";
        }

        public string GenerateDeleteUserSql(string loginUserName)
        {
            return $"DROP USER IF EXISTS `{loginUserName}`@`localhost`;";
        }

        public string GenerateGrantPrivelegesSql(string loginUserName, string dbName)
        {
            return $"GRANT ALL PRIVILEGES ON `{dbName}`. * TO '{loginUserName}'@'localhost';";
        }
    }
}
