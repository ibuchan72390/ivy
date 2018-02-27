using Ivy.Google.IoC;
using Ivy.TestHelper;

namespace Ivy.Google.Tests.Base
{
    public class GoogleTestBase : TestBase
    {
        public GoogleTestBase()
        {
            base.Init(
                containerGen => containerGen.InstallIvyGoogleCore());
        }
    }
}
