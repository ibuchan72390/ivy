using IBFramework.Core.Data;
using IBFramework.Data.Common.IoC;
using IBFramework.Data.MSSQL.IoC;
using IBFramework.IoC;
using IBFramework.TestHelper;
using IBFramework.TestHelper.TestValues;

namespace IBFramework.Data.MSSQL.IntegrationTest
{
    public class MsSqlIntegrationTestBase : TestBase
    {
        public MsSqlIntegrationTestBase()
        {
            Init(container => {
                container.InstallCommonData();
                container.InstallMsSql();

                TestExtensions.Init(MsSqlTestValues.TestDbConnectionString);
            });
        }

        public ITranConn TestTranConn =>
            ServiceLocator.Instance.Resolve<ITranConnGenerator>()
                .GenerateTranConn(MsSqlTestValues.TestDbConnectionString);

    }
}
