using IBFramework.Core.Data;
using IBFramework.IoC.Installers;
using IBFramework.TestHelper;
using IBFramework.TestUtilities;

namespace IBFramework.Data.MSSQL.IntegrationTest
{
    public class MsSqlIntegrationTestBase : TestBase
    {
        public MsSqlIntegrationTestBase()
        {
            Init(container => {
                container.InstallCommonData();
                container.InstallMsSql();
            });
        }

        public ITranConn TestTranConn =>
            TestServiceLocator.StaticContainer.Resolve<ITranConnGenerator>()
                .GenerateTranConn(TestValues.TestDbConnectionString);

    }
}
