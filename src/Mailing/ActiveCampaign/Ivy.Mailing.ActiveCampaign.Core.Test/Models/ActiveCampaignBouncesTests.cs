using Ivy.Mailing.ActiveCampaign.Core.Models;
using Ivy.Mailing.ActiveCampaign.Core.Test.Base;
using Xunit;

namespace Ivy.Mailing.ActiveCampaign.Core.Test.Models
{
    public class ActiveCampaignBouncesTests : BaseActiveCampaignModelTest<ActiveCampaignBounces>
    {
        const string bounceJson = @"{
            ""mailing"": [],
            ""mailings"": 0,
            ""responder"": [],
            ""responders"": 0
        }";

        [Fact]
        public void ActiveCampaignBounces_Converts_From_Json_As_Expected()
        {
            var result = base.TestJsonConvert(bounceJson);

            Assert.Equal(0, result.mailings);
            Assert.Equal(0, result.responders);
        }
    }
}
