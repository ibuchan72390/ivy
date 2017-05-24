using Ivy.Data.MSSQL.IoC;
using Ivy.TestHelper;
using Ivy.TestHelper.TestValues;

namespace Ivy.Data.MSSQL.Test
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
