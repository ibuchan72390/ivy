using IBFramework.IoC.Installers;
using IBFramework.TestHelper;

namespace IBFramework.Data.Common.Test
{
    public class CommonDataTestBase : TestBase
    {
        protected override void InitWrapper()
        {
            Init(container => container.InstallCommonData());
        }
    }
}
