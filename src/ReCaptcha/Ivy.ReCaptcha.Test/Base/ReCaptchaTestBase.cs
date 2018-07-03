using Ivy.IoC.Core;
using Ivy.ReCaptcha.IoC;
using Ivy.TestHelper;

namespace Ivy.ReCaptcha.Test.Base
{
    public class ReCaptchaTestBase<T> : TestBase<T>
        where T: class
    {
        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            containerGen.InstallIvyReCaptcha();
        }
    }
}
