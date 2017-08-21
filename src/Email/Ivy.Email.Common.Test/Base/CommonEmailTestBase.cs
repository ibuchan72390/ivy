using Ivy.Email.Common.IoC;
using Ivy.TestHelper;

namespace Ivy.Email.Common.Test.Base
{
    public class CommonEmailTestBase : TestBase
    {
        protected override void InitWrapper()
        {
            base.Init(
                containerGen => {
                    containerGen.InstallIvyCommonEmail();
                }
            );
        }
    }
}
