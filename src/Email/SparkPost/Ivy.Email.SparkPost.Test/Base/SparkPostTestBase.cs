using Ivy.Email.Common.IoC;
using Ivy.Email.SparkPost.IoC;
using Ivy.TestHelper;

namespace Ivy.Email.SparkPost.Test.Base
{
    public class SparkPostTestBase : TestBase
    {
        protected override void InitWrapper()
        {
            base.Init(
                containerGen => {
                    containerGen.InstallIvyCommonEmail();
                    containerGen.InstallIvySparkPostEmail();
                }
            );
        }
    }
}
