using Ivy.TestHelper;
using Ivy.Web.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Ivy.Web.Test.Base
{
    public class WebTestBase : TestBase
    {
        protected override void InitWrapper()
        {
            base.Init(
                containerGen => containerGen.InstallIvyWeb(),
                svcColl => svcColl.AddLogging());
        }
    }
}
