using Ivy.Captcha.IoC;
using Ivy.TestHelper;

namespace Ivy.Captcha.Test.Base
{
    public class CaptchaTestBase : TestBase
    {
        protected override void InitWrapper()
        {
            base.Init(containerGen => containerGen.InstallIvyCaptcha());
        }
    }
}
