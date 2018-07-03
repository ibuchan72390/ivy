using Ivy.IoC.Core;
using Ivy.Push.Firebase.IoC;
using Ivy.TestHelper;

namespace Ivy.Push.Firebase.Test.Base
{
    public class FirebasePushMessageTestBase<T> : TestBase<T>
        where T: class
    {
        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            containerGen.InstallIvyFirebasePushNotifications();
        }
    }
}
