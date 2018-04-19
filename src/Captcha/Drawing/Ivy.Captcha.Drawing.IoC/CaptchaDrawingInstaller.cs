using Ivy.Captcha.Core.Interfaces.Services;
using Ivy.Captcha.Drawing.Core.Interfaces.Services;
using Ivy.Captcha.Services;
using Ivy.IoC.Core;

namespace Ivy.Captcha.Drawing.IoC
{
    public class CaptchaInstaller : IContainerInstaller
    {
        public void Install(IContainerGenerator containerGenerator)
        {
            containerGenerator.RegisterSingleton<ICaptchaDrawingService, CaptchaDrawingService>();
            containerGenerator.RegisterSingleton<ICaptchaGenerationService, DrawingCaptchaGenerationService>();
        }
    }

    public static class CaptchaInstallerExtension
    {
        public static IContainerGenerator InstallIvyCaptchaDrawing(this IContainerGenerator containerGenerator)
        {
            new CaptchaInstaller().Install(containerGenerator);
            return containerGenerator;
        }
    }
}
