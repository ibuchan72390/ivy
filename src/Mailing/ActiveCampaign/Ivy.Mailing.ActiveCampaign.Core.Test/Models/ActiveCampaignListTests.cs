using Ivy.Mailing.ActiveCampaign.Core.Models;
using Ivy.Mailing.ActiveCampaign.Core.Test.Base;
using Xunit;

namespace Ivy.Mailing.ActiveCampaign.Core.Test.Models
{
    public class ActiveCampaignListTests : BaseActiveCampaignModelTest<ActiveCampaignList>
    {
        const string listJson = @"{
            ""id"": ""1"",
            ""subscriberid"": ""1"",
            ""listid"": ""1"",
            ""formid"": ""0"",
            ""seriesid"": ""0"",
            ""sdate"": ""2018-01-23 11:22:39"",
            ""udate"": ""0000-00-00 00:00:00"",
            ""status"": ""1"",
            ""responder"": ""1"",
            ""sync"": ""0"",
            ""unsubreason"": """",
            ""unsubcampaignid"": ""0"",
            ""unsubmessageid"": ""0"",
            ""first_name"": ""Ian"",
            ""last_name"": ""Buchan"",
            ""ip4_sub"": ""0"",
            ""sourceid"": ""10"",
            ""sourceid_autosync"": ""0"",
            ""ip4_last"": ""0"",
            ""ip4_unsub"": ""0"",
            ""listname"": ""Development"",
            ""sdate_iso"": ""2018-01-23T12:22:39-06:00""
        }";


        [Fact]
        public void ActiveCampaignList_Converts_From_Json_As_Expected()
        {
            var result = base.TestJsonConvert(listJson);

            Assert.Equal("1", result.id);
            Assert.Equal("1", result.subscriberid);
            Assert.Equal("1", result.listid);
            Assert.Equal("0", result.formid);
            Assert.Equal("0", result.seriesid);
            Assert.Equal("2018-01-23 11:22:39", result.sdate);
            Assert.Equal("0000-00-00 00:00:00", result.udate);
            Assert.Equal("1", result.status);
            Assert.Equal("1", result.responder);
            Assert.Equal("0", result.sync);
            Assert.Equal("", result.unsubreason);
            Assert.Equal("0", result.unsubcampaignid);
            Assert.Equal("0", result.unsubmessageid);
            Assert.Equal("Ian", result.first_name);
            Assert.Equal("Buchan", result.last_name);
            Assert.Equal("0", result.ip4_sub);
            Assert.Equal("10", result.sourceid);
            Assert.Equal("0", result.sourceid_autosync);
            Assert.Equal("0", result.ip4_last);
            Assert.Equal("0", result.ip4_unsub);
            Assert.Equal("Development", result.listname);
            Assert.Equal("2018-01-23T12:22:39-06:00", result.sdate_iso);
        }
    }
}
