using Ivy.ReCaptcha.Core.Models.Base;
using Newtonsoft.Json;

namespace Ivy.ReCaptcha.Core.Models
{
    public class ReCaptchaResponse :
        BaseReCaptchaResponse
    {
        [JsonProperty(PropertyName = "hostname")]
        public string Hostname { get; set; }
    }
}
