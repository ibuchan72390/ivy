using IBFramework.IoC.Installers;
using IBFramework.TestHelper;

namespace IBFramework.Data.MSSQL.Test
{
    public class MsSqlTestBase : TestBase
    {
        protected override void InitWrapper()
        {
            Init(container => container.InstallMsSql());
        }
    }
}
