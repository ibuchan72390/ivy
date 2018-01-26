using Ivy.Mailing.ActiveCampaign.Core.Interfaces.Models;
using System.Collections.Generic;

namespace Ivy.Mailing.ActiveCampaign.Core.Models
{
    public class ActiveCampaignContact : IActiveCampaignModel
    {
        public ActiveCampaignContact()
        {
            lists = new Dictionary<int, ActiveCampaignList>();
            fields = new Dictionary<int, ActiveCampaignField>();
        }

        public string id { get; set; }//"id": "1",
        public string subscriberid { get; set; }//"subscriberid": "1",
        public string listid { get; set; }//"listid": "1",
        public string formid { get; set; }//"formid": "0",
        public string seriesid { get; set; }//"seriesid": "0",
        public string sdate { get; set; }//"sdate": "2018-01-23 11:22:39",
        public string udate { get; set; }//"udate": "2018-01-23 12:22:24",
        public string status { get; set; }//"status": "1",
        public string responder { get; set; }//"responder": "1",
        public string sync { get; set; }//"sync": "0",
        public string unsubreason { get; set; }//"unsubreason": "",
        public string unsubcampaignid { get; set; }//"unsubcampaignid": "0",
        public string unsubmessageid { get; set; }//"unsubmessageid": "0",
        public string first_name { get; set; }//"first_name": "Ian",
        public string last_name { get; set; }//"last_name": "Buchan",
        public string ip4_sub { get; set; }//"ip4_sub": "0",
        public string sourceid { get; set; }//"sourceid": "10",
        public string sourceid_autosync { get; set; }//"sourceid_autosync": "0",
        public string ip4_last { get; set; }//"ip4_last": "0",
        public string ip4_unsub { get; set; }//"ip4_unsub": "0",
        public string listname { get; set; }//"listname": "Development",
        public string sdate_iso { get; set; }//"sdate_iso": "2018-01-23T12:22:39-06:00",
        public string lid { get; set; }//"lid": "1",
        public string ip4 { get; set; }//"ip4": "0.0.0.0",
        public string a_unsub_time { get; set; }//"a_unsub_time": "00:00:00",
        public string a_unsub_date { get; set; }//"a_unsub_date": "0000-00-00",
        public string cdate { get; set; }//"cdate": "2018-01-23 12:22:24",
        public string email { get; set; }//"email": "ibuchan@iamglobaleducation.com",
        public string phone { get; set; }//"phone": "9209804228",
        public string orgid { get; set; }//"orgid": "1",
        public string orgname { get; set; }//"orgname": "TEST",
        public string segmentio_id { get; set; }//"segmentio_id": "",
        public string bounced_hard { get; set; }//"bounced_hard": "0",
        public string bounced_soft { get; set; }//"bounced_soft": "0",
        public string bounced_date { get; set; }//"bounced_date": null,
        public string ip { get; set; }//"ip": "0.0.0.0",
        public string ua { get; set; }//"ua": null,
        public string hash { get; set; }//"hash": "1609763baacba58adee90d012381e512",
        public string socialdata_lastcheck { get; set; }//"socialdata_lastcheck": null,
        public string email_local { get; set; }//"email_local": "",
        public string email_domain { get; set; }//"email_domain": "",
        public string sentcnt { get; set; }//"sentcnt": "0",
        public string rating { get; set; }//"rating": "0",
        public string rating_tstamp { get; set; }//"rating_tstamp": null,
        public string gravatar { get; set; }//"gravatar": "0",
        public string deleted { get; set; }//"deleted": "0",
        public string adate { get; set; }//"adate": null,
        public string edate { get; set; }//"edate": null,
        public string name { get; set; }//"name": "Ian Buchan",
        public Dictionary<int, ActiveCampaignList> lists { get; set; }
        public string listslist { get; set; } //"listslist": "1",
        public Dictionary<int, ActiveCampaignField> fields { get; set; }
        public IEnumerable<ActiveCampaignAction> actions { get; set; }
        public ActiveCampaignBounces bounces { get; set; }
        public int bouncescnt { get; set; }//"bouncescnt": 0,
        public int result_code { get; set; }
        public string result_message { get; set; }
        public string result_output { get; set; }

        /*
         * Unaccounted Items
         */
        //"automation_history": [],
        //"campaign_history": [],
        //"tags": [],
        //"geo": []
    }
}
