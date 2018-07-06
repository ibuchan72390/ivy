namespace Ivy.Migration.Core.Interfaces.Services
{
    public interface IMigrationSqlExecutor
    {
        void ExecuteSql(string scriptText, string connectionString);
    }
}
