namespace Ivy.Captcha.Core.Models
{
    public class CaptchaResult
    {
        public string CaptchaCode { get; set; }

        public byte[] CaptchaByteData { get; set; }
    }
}