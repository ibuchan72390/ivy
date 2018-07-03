using Ivy.Captcha.IoC;
using Ivy.Captcha.Drawing.IoC;
using Ivy.TestHelper;
using Ivy.IoC.Core;

namespace Ivy.Captcha.Drawing.Test.Base
{
    public class CaptchaDrawingTestBase<T> : TestBase<T>
        where T: class
    {
        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            containerGen.InstallIvyCaptcha();
            containerGen.InstallIvyCaptchaDrawing();
        }
    }
}
