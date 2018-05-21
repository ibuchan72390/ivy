using Ivy.ReCaptcha.Core.Models.Base;
using Newtonsoft.Json;

namespace Ivy.ReCaptcha.Core.Models
{
    public class AndroidReCaptchaResponse :
        BaseReCaptchaResponse
    {
        [JsonProperty(PropertyName = "apk_package_name")]
        public string ApkPackageName { get; set; }
    }
}
