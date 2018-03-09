using Ivy.TestHelper;
using Ivy.Push.Web.IoC;
using Ivy.Web.IoC;

namespace Ivy.Push.Web.Test.Base
{
    public class BaseWebPushTest : TestBase
    {
        public BaseWebPushTest()
        {
            base.Init(
                container =>
                {
                    container.InstallIvyWeb();
                    container.InstallIvyWebPushNotifications();
                },
                svcColl =>
                {

                });
        }
    }
}
