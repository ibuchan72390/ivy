using IBFramework.Core.Data;
using IBFramework.IoC;
using IBFramework.IoC.Installers;
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
        }

        public ITranConn TestTranConn =>
            ServiceLocator.Instance.Resolve<ITranConnGenerator>()
                .GenerateTranConn(connectionString);

    }
}
