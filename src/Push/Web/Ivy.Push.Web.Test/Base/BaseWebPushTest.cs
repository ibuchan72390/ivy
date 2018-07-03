using Ivy.TestHelper;
using Ivy.Push.Web.IoC;
using Ivy.Web.IoC;
using Ivy.IoC.Core;

namespace Ivy.Push.Web.Test.Base
{
    public class BaseWebPushTest<T> : TestBase<T>
        where T: class
    {
        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            containerGen.InstallIvyWeb();
            containerGen.InstallIvyWebPushNotifications();
        }
    }
}
