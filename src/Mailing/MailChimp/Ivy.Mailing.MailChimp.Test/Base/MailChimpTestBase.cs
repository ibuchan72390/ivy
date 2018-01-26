using Ivy.TestHelper;
using Ivy.Web.IoC;
using Ivy.Mailing.MailChimp.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Ivy.Mailing.MailChimp.Test.Base
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
