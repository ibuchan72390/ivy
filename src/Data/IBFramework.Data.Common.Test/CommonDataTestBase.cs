using IBFramework.Data.Common.IoC;
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
