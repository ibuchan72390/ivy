namespace Ivy.Auth0.Core.Models
{
    public class Auth0GeoIp
    {
        #region Public Vars

        public string country_code { get; set; }
        public string country_code3 { get; set; }
        public string country_name { get; set; }
        public string city_name { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string time_zone { get; set; }
        public string continent_code { get; set; }

        #endregion
    }
}
