using Ivy.Mailing.ActiveCampaign.Core.Models;
using Ivy.Mailing.ActiveCampaign.Core.Test.Base;
using Xunit;

namespace Ivy.Mailing.ActiveCampaign.Core.Test.Models
{
    public class ActiveCampaignContactTests : BaseActiveCampaignModelTest<ActiveCampaignContact>
    {
        const string contactJson = @"{
            ""id"": ""2"",
            ""subscriberid"": ""2"",
            ""listid"": ""1"",
            ""formid"": ""0"",
            ""seriesid"": ""0"",
            ""sdate"": ""2018-01-23 12:17:50"",
            ""udate"": ""2018-01-23 13:17:39"",
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
            ""sdate_iso"": ""2018-01-23T13:17:50-06:00"",
            ""lid"": ""1"",
            ""ip4"": ""0.0.0.0"",
            ""a_unsub_time"": ""00:00:00"",
            ""a_unsub_date"": ""0000-00-00"",
            ""cdate"": ""2018-01-23 13:17:39"",
            ""email"": ""ibuchan@iamglobaleducation.com"",
            ""phone"": ""9209804228"",
            ""orgid"": ""1"",
            ""orgname"": ""TEST"",
            ""segmentio_id"": """",
            ""bounced_hard"": ""0"",
            ""bounced_soft"": ""0"",
            ""bounced_date"": null,
            ""ip"": ""0.0.0.0"",
            ""ua"": null,
            ""hash"": ""9fab74dd482f9397b58f80ec1cc39c50"",
            ""socialdata_lastcheck"": null,
            ""email_local"": """",
            ""email_domain"": """",
            ""sentcnt"": ""0"",
            ""rating"": ""0"",
            ""rating_tstamp"": null,
            ""gravatar"": ""0"",
            ""deleted"": ""0"",
            ""adate"": null,
            ""edate"": null,
            ""name"": ""Ian Buchan"",
            ""lists"": {
                ""1"": {
                    ""id"": ""2"",
                    ""subscriberid"": ""2"",
                    ""listid"": ""1"",
                    ""formid"": ""0"",
                    ""seriesid"": ""0"",
                    ""sdate"": ""2018-01-23 12:17:50"",
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
                    ""sdate_iso"": ""2018-01-23T13:17:50-06:00""
                }
            },
            ""listslist"": ""1"",
            ""fields"": {
                ""1"": {
                    ""id"": ""1"",
                    ""title"": ""Role"",
                    ""descript"": """",
                    ""type"": ""radio"",
                    ""isrequired"": ""0"",
                    ""perstag"": ""ROLE"",
                    ""defval"": """",
                    ""show_in_list"": ""0"",
                    ""rows"": ""0"",
                    ""cols"": ""0"",
                    ""visible"": ""1"",
                    ""service"": """",
                    ""ordernum"": ""1"",
                    ""cdate"": ""2018-01-23 12:05:44"",
                    ""udate"": ""2018-01-23 12:05:44"",
                    ""val"": ""Student"",
                    ""relid"": ""0"",
                    ""dataid"": ""6"",
                    ""element"": ""radio"",
                    ""options"": [
                        {
                            ""name"": ""Student"",
                            ""value"": ""Student"",
                            ""isdefault"": ""0""
                        },
                        {
                            ""name"": ""Parent"",
                            ""value"": ""Parent"",
                            ""isdefault"": ""0""
                        },
                        {
                            ""name"": ""Professional"",
                            ""value"": ""Professional"",
                            ""isdefault"": ""0""
                        }
                    ],
                    ""selected"": ""Student"",
                    ""tag"": ""%ROLE%""
                },
                ""2"": {
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
                },
                ""3"": {
                    ""id"": ""3"",
                    ""title"": ""More"",
                    ""descript"": """",
                    ""type"": ""text"",
                    ""isrequired"": ""0"",
                    ""perstag"": ""MORE"",
                    ""defval"": """",
                    ""show_in_list"": ""0"",
                    ""rows"": ""0"",
                    ""cols"": ""0"",
                    ""visible"": ""1"",
                    ""service"": """",
                    ""ordernum"": ""3"",
                    ""cdate"": ""2018-01-23 12:16:37"",
                    ""udate"": ""2018-01-23 12:16:37"",
                    ""val"": ""TEST"",
                    ""relid"": ""0"",
                    ""dataid"": ""4"",
                    ""element"": ""text"",
                    ""tag"": ""%MORE%""
                }
            },
            ""actions"": [
                {
                    ""text"": ""Subscribed to list - Development"",
                    ""type"": ""subscribe"",
                    ""tstamp"": ""2018-01-23T13:17:50-06:00""
                }
            ],
            ""automation_history"": [],
            ""campaign_history"": [],
            ""bounces"": {
                ""mailing"": [],
                ""mailings"": 0,
                ""responder"": [],
                ""responders"": 0
            },
            ""bouncescnt"": 0,
            ""tags"": [],
            ""geo"": []
        }";


        [Fact]
        public void ActiveCampaignContact_Converts_From_Json_As_Expected()
        {
            var result = base.TestJsonConvert(contactJson);

            Assert.Equal("2", result.id);
            Assert.Equal("2", result.subscriberid);
            Assert.Equal("1", result.listid);
            Assert.Equal("0", result.formid);
            Assert.Equal("2018-01-23 12:17:50", result.sdate);
            Assert.Equal("2018-01-23 13:17:39", result.udate);
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
            Assert.Equal("2018-01-23T13:17:50-06:00", result.sdate_iso);
            Assert.Equal("1", result.lid);
            Assert.Equal("0.0.0.0", result.ip4);
            Assert.Equal("00:00:00", result.a_unsub_time);
            Assert.Equal("0000-00-00", result.a_unsub_date);
            Assert.Equal("2018-01-23 13:17:39", result.cdate);
            Assert.Equal("ibuchan@iamglobaleducation.com", result.email);
            Assert.Equal("9209804228", result.phone);
            Assert.Equal("1", result.orgid);
            Assert.Equal("TEST", result.orgname);
            Assert.Equal("", result.segmentio_id);
            Assert.Equal("0", result.bounced_hard);
            Assert.Equal("0", result.bounced_soft);
            Assert.Null(result.bounced_date);
            Assert.Equal("0.0.0.0", result.ip);
            Assert.Null(result.ua);
            Assert.Equal("9fab74dd482f9397b58f80ec1cc39c50", result.hash);
            Assert.Null(result.socialdata_lastcheck);
            Assert.Equal("", result.email_local);
            Assert.Equal("", result.email_domain);
            Assert.Equal("0", result.sentcnt);
            Assert.Equal("0", result.rating);
            Assert.Null(result.rating_tstamp);
            Assert.Equal("0", result.gravatar);
            Assert.Equal("0", result.deleted);
            Assert.Null(result.adate);
            Assert.Null(result.edate);
            Assert.Equal("Ian Buchan", result.name);

            Assert.Single(result.lists);
            Assert.Equal("1", result.listslist);

            Assert.Equal(3, result.fields.Count);

            Assert.Single(result.actions);

            Assert.NotNull(result.bounces);
        }
    }
}
