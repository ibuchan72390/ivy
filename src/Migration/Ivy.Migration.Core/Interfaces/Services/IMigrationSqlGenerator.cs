namespace Ivy.Migration.Core.Interfaces.Services
{
    public interface IMigrationSqlGenerator
    {
        string GenerateDeleteDatabaseSql(string dbName);
        string GenerateCreateDatabaseSql(string dbName);
        string GenerateCreateUserSql(string loginUserName, string loginPassword);
        string GenerateGrantPrivelegesSql(string loginUserName, string dbName);
        string GenerateDeleteUserSql(string loginUserName);
        string GenerateDbConnectionString(string dbName, string userName, string userPass);
    }
}
