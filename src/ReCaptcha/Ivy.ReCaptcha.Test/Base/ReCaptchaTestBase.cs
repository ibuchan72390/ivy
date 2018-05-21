using Ivy.ReCaptcha.IoC;
using Ivy.TestHelper;

namespace Ivy.ReCaptcha.Test.Base
{
    public class ReCaptchaTestBase : TestBase
    {
        protected override void InitWrapper()
        {
            base.Init(
                containerGen => {
                    containerGen.InstallIvyReCaptcha();
                }
            );
        }
    }
}
