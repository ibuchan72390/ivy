using Ivy.Mailing.ActiveCampaign.Core.Interfaces.Services;
using Ivy.Mailing.ActiveCampaign.Test.Base;
using Ivy.IoC;
using Xunit;
using Ivy.Web.Core.Json;

namespace Ivy.Mailing.ActiveCampaign.Test.Services
{
    public class ActiveCampaignContactListDeserializerTests : 
        BaseActiveCampaignTest<IActiveCampaignContactListDeserializer>
    {
        string contactListJson = @"{
            ""0"": {
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
            },

            ""result_code"": 1,
            ""result_message"": ""Success: Something is returned"",
            ""result_output"": ""json""
        }";

        /*
         * result_code, result_message, and result_output are problematic here
         * They don't directly cast to a Dictionary object and this moronic company insists on their dynamic model structure
         * Because fuck strongly typed languages that do things correctly, amirite?
         */

        [Fact]
        public void ActiveCampaignContactList_Converts_From_Json_As_Expected()
        {
            var result = Sut.Deserialize(contactListJson);

            Assert.Single(result);

            Assert.Equal(1, result.result_code);
            Assert.Equal("Success: Something is returned", result.result_message);
            Assert.Equal("json", result.result_output);
        }
    }
}
