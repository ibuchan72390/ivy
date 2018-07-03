using Ivy.Google.IoC;
using Ivy.IoC.Core;
using Ivy.TestHelper;

namespace Ivy.Google.Tests.Base
{
    public class GoogleTestBase<T> : TestBase<T>
        where T: class
    {
        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            containerGen.InstallIvyGoogleCore();
        }
    }
}
