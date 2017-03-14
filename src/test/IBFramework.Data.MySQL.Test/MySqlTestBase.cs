using IBFramework.Data.Common.IoC;
using IBFramework.MySQL.IoC;
using IBFramework.TestHelper;
using IBFramework.TestHelper.TestValues;

namespace IBFramework.Data.MySQL.Test
{
    public class MySqlTestBase : TestBase
    {
        protected const string connectionString = MySqlTestValues.TestDbConnectionString;

        protected override void InitWrapper()
        {
            Init(container => {
                container.InstallCommonData();
                container.InstallMySql();

                TestExtensions.Init(connectionString);
            });
        }
    }
}
