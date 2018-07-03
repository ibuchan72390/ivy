using Ivy.IoC.Core;
using Ivy.Mailing.ActiveCampaign.IoC;
using Ivy.TestHelper;
using Ivy.Web.IoC;

namespace Ivy.Mailing.ActiveCampaign.Test.Base
{
    public class BaseActiveCampaignTest<T> : TestBase<T>
        where T: class
    {
        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            containerGen.InstallIvyActiveCampaign();
            containerGen.InstallIvyWeb();
        }
    }
}
