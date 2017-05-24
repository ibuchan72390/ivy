using Ivy.TestHelper;
using Ivy.Web.IoC;

namespace Ivy.Web.Test.Base
{
    public class WebTestBase : TestBase
    {
        protected override void InitWrapper()
        {
            base.Init(containerGen => containerGen.InstallIvyWeb());
        }
    }
}
