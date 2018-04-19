using Ivy.Captcha.IoC;
using Ivy.Captcha.Magick.IoC;
using Ivy.TestHelper;

namespace Ivy.Captcha.Magick.Test.Base
{
    public class CaptchaMagickTestBase : TestBase
    {
        protected override void InitWrapper()
        {
            base.Init(containerGen =>
            {
                containerGen.InstallIvyCaptcha();
                containerGen.InstallIvyCaptchaMagick();
            });
        }
    }
}
