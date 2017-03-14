using IBFramework.Data.MSSQL.IoC;
using IBFramework.TestHelper;
using IBFramework.TestHelper.TestValues;

namespace IBFramework.Data.MSSQL.Test
{
    public class MsSqlTestBase : TestBase
    {
        protected const string connectionString = MsSqlTestValues.TestDbConnectionString;

        protected override void InitWrapper()
        {
            Init(container => {
                container.InstallMsSql();
                TestExtensions.Init(connectionString);
            });
        }
    }
}
