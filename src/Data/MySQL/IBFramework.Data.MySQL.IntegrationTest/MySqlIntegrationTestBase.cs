using IBFramework.Data.Common.IoC;
using IBFramework.Data.Core.Interfaces;
using IBFramework.IoC;
using IBFramework.MySQL.IoC;
using IBFramework.TestHelper;
using IBFramework.TestHelper.TestValues;

namespace IBFramework.Data.MySQL.IntegrationTest
{
    public class MySqlIntegrationTestBase : TestBase
    {
        protected const string connectionString = MySqlTestValues.TestDbConnectionString;

        public MySqlIntegrationTestBase()
        {
            Init(container => {
                container.InstallCommonData();
                container.InstallMySql();

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
