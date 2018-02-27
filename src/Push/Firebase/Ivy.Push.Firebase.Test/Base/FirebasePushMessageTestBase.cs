using Ivy.Push.Firebase.IoC;
using Ivy.TestHelper;

namespace Ivy.Push.Firebase.Test.Base
{
    public class FirebasePushMessageTestBase : TestBase
    {
        protected override void InitWrapper()
        {
            base.Init(
                container => {
                    container.InstallIvyFirebasePushNotifications();
                });
        }
    }
}
