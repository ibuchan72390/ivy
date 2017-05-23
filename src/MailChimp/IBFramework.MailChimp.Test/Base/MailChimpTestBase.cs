using IBFramework.TestHelper;
using Microsoft.Extensions.DependencyInjection;

namespace IBFramework.MailChimp.Test.Base
{
    public class MailChimpTestBase : TestBase
    {
        protected override void InitWrapper()
        {
            base.Init(null, svcLocator =>
            {
                svcLocator.AddLogging();
            });
        }
    }
}
