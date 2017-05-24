using Ivy.Data.Common.IoC;
using Ivy.MySQL.IoC;
using Ivy.TestHelper;
using Ivy.TestHelper.TestValues;

namespace Ivy.Data.MySQL.Test
{
    public class MySqlTestBase : TestBase
    {
        protected const string connectionString = MySqlTestValues.TestDbConnectionString;

        protected override void InitWrapper()
        {
            Init(container => {
                container.InstallIvyCommonData();
                container.InstallIvyMySql();

                TestExtensions.Init(connectionString);
            });
        }


        #region Helper Methods

        protected string FormatSqlAttr(string item) => $"`{item}`";

        #endregion
    }
}
