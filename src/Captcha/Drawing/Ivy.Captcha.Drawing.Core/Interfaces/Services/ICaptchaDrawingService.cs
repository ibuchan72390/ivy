using System.Drawing;

namespace Ivy.Captcha.Drawing.Core.Interfaces.Services
{
    public interface ICaptchaDrawingService
    {
        void DrawCaptchaCode(int width, int height, string captchaCode, Graphics graph);

        void DrawDisorderLine(int width, int height, Graphics graph);

        void UnsafeAdjustRippleEffect(Bitmap baseMap);
    }
}
