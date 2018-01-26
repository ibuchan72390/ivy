using Ivy.Mailing.ActiveCampaign.Core.Models;
using Ivy.Mailing.ActiveCampaign.Core.Test.Base;
using Xunit;

namespace Ivy.Mailing.ActiveCampaign.Core.Test.Models
{
    public class ActiveCampaignErrorTests : BaseActiveCampaignModelTest<ActiveCampaignError>
    {
        const string errJson = @"{
            ""result_code"": 0,
            ""result_message"": ""Failed: Nothing is returned"",
            ""result_output"": ""json""
        }";


        [Fact]
        public void ActiveCampaignError_Converts_From_Json_As_Expected()
        {
            var result = base.TestJsonConvert(errJson);

            Assert.Equal(0, result.result_code);
            Assert.Equal("Failed: Nothing is returned", result.result_message);
            Assert.Equal("json", result.result_output);
        }
    }
}
