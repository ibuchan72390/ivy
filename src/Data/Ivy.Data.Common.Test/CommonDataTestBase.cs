using Ivy.Data.Common.IoC;
using Ivy.TestHelper;

namespace Ivy.Data.Common.Test
{
    public class CommonDataTestBase : TestBase
    {
        protected override void InitWrapper()
        {
            Init(container => container.InstallIvyCommonData());
        }
    }
}
