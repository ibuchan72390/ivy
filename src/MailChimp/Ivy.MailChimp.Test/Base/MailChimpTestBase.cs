using Ivy.TestHelper;
using Ivy.Web.IoC;
using Ivy.MailChimp.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Ivy.MailChimp.Test.Base
{
    public class MailChimpTestBase : TestBase
    {
        protected override void InitWrapper()
        {
            base.Init(
                containerGen => {
                    containerGen.InstallIvyWeb();
                    containerGen.InstallIvyMailChimp();
                },
                svcLocator => svcLocator.AddLogging());
        }
    }
}
