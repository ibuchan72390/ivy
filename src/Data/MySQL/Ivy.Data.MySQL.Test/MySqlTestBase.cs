using Ivy.Data.Common.IoC;
using Ivy.IoC.Core;
using Ivy.MySQL.IoC;
using Ivy.TestHelper;
using Ivy.TestHelper.TestValues;

namespace Ivy.Data.MySQL.Test
{
    public class MySqlTestBase<T> : TestBase<T>
        where T: class
    {
        #region Variables & Constants

        protected const string connectionString = MySqlTestValues.TestDbConnectionString;

        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            containerGen.InstallIvyCommonData();
            containerGen.InstallIvyMySql();

            TestExtensions.Init(connectionString);
        }

        #endregion

        #region Helper Methods

        protected string FormatSqlAttr(string item) => $"`{item}`";

        #endregion
    }
}
