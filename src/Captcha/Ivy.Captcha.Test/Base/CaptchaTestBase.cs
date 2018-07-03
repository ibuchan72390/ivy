using Ivy.Captcha.IoC;
using Ivy.IoC.Core;
using Ivy.TestHelper;

namespace Ivy.Captcha.Test.Base
{
    public class CaptchaTestBase<T> : TestBase<T>
        where T: class
    {
        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            containerGen.InstallIvyCaptcha();
        }
    }
}
