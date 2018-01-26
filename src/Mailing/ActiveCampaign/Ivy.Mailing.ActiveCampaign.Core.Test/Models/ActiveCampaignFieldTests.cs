using Ivy.Mailing.ActiveCampaign.Core.Models;
using Ivy.Mailing.ActiveCampaign.Core.Test.Base;
using Xunit;

namespace Ivy.Mailing.ActiveCampaign.Core.Test.Models
{
    public class ActiveCampaignFieldTests : BaseActiveCampaignModelTest<ActiveCampaignField>
    {
        const string fieldJson = @"{
            ""id"": ""2"",
            ""title"": ""Country"",
            ""descript"": """",
            ""type"": ""text"",
            ""isrequired"": ""0"",
            ""perstag"": ""COUNTRY"",
            ""defval"": """",
            ""show_in_list"": ""0"",
            ""rows"": ""0"",
            ""cols"": ""0"",
            ""visible"": ""1"",
            ""service"": """",
            ""ordernum"": ""2"",
            ""cdate"": ""2018-01-23 12:16:23"",
            ""udate"": ""2018-01-23 12:16:23"",
            ""val"": ""Uganda"",
            ""relid"": ""0"",
            ""dataid"": ""5"",
            ""element"": ""text"",
            ""tag"": ""%COUNTRY%""
        }";

        [Fact]
        public void ActiveCampaignField_Converts_From_Json_As_Expected()
        {
            var result = base.TestJsonConvert(fieldJson);

            Assert.Equal("2", result.id);
            Assert.Equal("Country", result.title);
            Assert.Equal("", result.descript);
            Assert.Equal("text", result.type);
            Assert.Equal("0", result.isrequired);
            Assert.Equal("COUNTRY", result.perstag);
            Assert.Equal("", result.defval);
            Assert.Equal("0", result.show_in_list);
            Assert.Equal("0", result.rows);
            Assert.Equal("0", result.cols);
            Assert.Equal("1", result.visible);
            Assert.Equal("", result.service);
            Assert.Equal("2", result.ordernum);
            Assert.Equal("2018-01-23 12:16:23", result.cdate);
            Assert.Equal("2018-01-23 12:16:23", result.udate);
            Assert.Equal("Uganda", result.val);
            Assert.Equal("0", result.relid);
            Assert.Equal("5", result.dataid);
            Assert.Equal("text", result.element);
            Assert.Equal("%COUNTRY%", result.tag);
        }
    }
}
