using Ivy.Captcha.Core.Interfaces.Models;

namespace Ivy.Captcha.Core.Models
{
    public class CaptchaResult : ICaptchaResult
    {
        public string CaptchaCode { get; set; }

        public byte[] CaptchaByteData { get; set; }
    }
}