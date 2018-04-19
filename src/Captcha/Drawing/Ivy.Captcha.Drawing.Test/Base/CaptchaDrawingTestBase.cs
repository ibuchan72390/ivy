using Ivy.Captcha.IoC;
using Ivy.Captcha.Drawing.IoC;
using Ivy.TestHelper;

namespace Ivy.Captcha.Drawing.Test.Base
{
    public class CaptchaDrawingTestBase : TestBase
    {
        protected override void InitWrapper()
        {
            base.Init(containerGen => {
                containerGen.InstallIvyCaptcha();
                containerGen.InstallIvyCaptchaDrawing();
            });
        }
    }
}
