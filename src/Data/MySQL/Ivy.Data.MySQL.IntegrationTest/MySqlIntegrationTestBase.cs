using Ivy.Data.Common.IoC;
using Ivy.Data.Core.Interfaces;
using Ivy.IoC;
using Ivy.MySQL.IoC;
using Ivy.TestHelper;
using Ivy.TestHelper.TestValues;

namespace Ivy.Data.MySQL.IntegrationTest
{
    public class MySqlIntegrationTestBase : TestBase
    {
        protected const string connectionString = MySqlTestValues.TestDbConnectionString;

        public MySqlIntegrationTestBase()
        {
            Init(container => {
                container.InstallIvyCommonData();
                container.InstallIvyMySql();

                TestExtensions.Init(connectionString);
            });

            
            // Clean Database
            TestCleaner.CleanDatabase();
        }

        public ITranConn TestTranConn =>
            ServiceLocator.Instance.Resolve<ITranConnGenerator>()
                .GenerateTranConn(connectionString);

    }
}
