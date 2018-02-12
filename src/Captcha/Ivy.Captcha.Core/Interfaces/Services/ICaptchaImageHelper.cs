using System.Drawing;

namespace Ivy.Captcha.Core.Interfaces.Services
{
    public interface ICaptchaImageHelper
    {
        int GetFontSize(int imageWidth, int captchCodeCount);

        Color GetRandomDeepColor();

        Color GetRandomLightColor();
    }
}
