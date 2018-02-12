namespace Ivy.Captcha.Core.Interfaces.Models
{
    public interface ICaptchaResult
    {
        string CaptchaCode { get; set; }

        byte[] CaptchaByteData { get; set; }
    }
}
