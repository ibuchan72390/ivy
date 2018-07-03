using Ivy.Data.Common.IoC;
using Ivy.Data.Core.Interfaces;
using Ivy.IoC;
using Ivy.IoC.Core;
using Ivy.MySQL.IoC;
using Ivy.TestHelper;
using Ivy.TestHelper.TestValues;

namespace Ivy.Data.MySQL.IntegrationTest
{
    public class MySqlIntegrationTestBase<T> : TestBase<T>
        where T: class
    {
        protected const string connectionString = MySqlTestValues.TestDbConnectionString;

        public MySqlIntegrationTestBase()
        {
            // Clean Database
            TestCleaner.CleanDatabase();
        }

        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            containerGen.InstallIvyCommonData();
            containerGen.InstallIvyMySql();

            TestExtensions.Init(connectionString);
        }

        public ITranConn TestTranConn =>
            ServiceLocator.Instance.GetService<ITranConnGenerator>()
                .GenerateTranConn(connectionString);
    }
}
