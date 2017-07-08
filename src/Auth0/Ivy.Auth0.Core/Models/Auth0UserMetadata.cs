namespace Ivy.Auth0.Core.Models
{
    public class Auth0UserMetadata
    {
        #region Public Vars

        public string name { get; set; }
        public string nickname { get; set; }
        public string lang { get; set; }
        public Auth0GeoIp geoip { get; set; }

        #endregion
    }
}
