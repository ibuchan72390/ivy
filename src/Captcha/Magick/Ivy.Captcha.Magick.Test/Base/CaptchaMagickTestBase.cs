using Ivy.Captcha.IoC;
using Ivy.Captcha.Magick.IoC;
using Ivy.IoC.Core;
using Ivy.TestHelper;

namespace Ivy.Captcha.Magick.Test.Base
{
    public class CaptchaMagickTestBase<T> : TestBase<T>
        where T: class
    {
        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            containerGen.InstallIvyCaptcha();
            containerGen.InstallIvyCaptchaMagick();
        }
    }
}
