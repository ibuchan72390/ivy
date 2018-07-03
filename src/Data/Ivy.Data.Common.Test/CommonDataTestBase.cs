using Ivy.Data.Common.IoC;
using Ivy.IoC.Core;
using Ivy.TestHelper;

namespace Ivy.Data.Common.Test
{
    public class CommonDataTestBase<T> : TestBase<T>
        where T: class
    {
        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            containerGen.InstallIvyCommonData();
        }
    }
}
