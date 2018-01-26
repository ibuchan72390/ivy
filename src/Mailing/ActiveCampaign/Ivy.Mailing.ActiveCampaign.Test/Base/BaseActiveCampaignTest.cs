using Ivy.Mailing.ActiveCampaign.IoC;
using Ivy.TestHelper;
using Ivy.Web.IoC;

namespace Ivy.Mailing.ActiveCampaign.Test.Base
{
    public class BaseActiveCampaignTest : TestBase
    {
        protected override void InitWrapper()
        {
            base.Init(
                containerGen => {
                    containerGen.InstallIvyActiveCampaign();
                    containerGen.InstallIvyWeb();
                });
        }
    }
}
