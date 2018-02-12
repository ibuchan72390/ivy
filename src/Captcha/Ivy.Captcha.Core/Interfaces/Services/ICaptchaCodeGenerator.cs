namespace Ivy.Captcha.Core.Interfaces.Services
{
    public interface ICaptchaCodeGenerator
    {
        string GenerateCaptchaCode(int charLength);
    }
}
