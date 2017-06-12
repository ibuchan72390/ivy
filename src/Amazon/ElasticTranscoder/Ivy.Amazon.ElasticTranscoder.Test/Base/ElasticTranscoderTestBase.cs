using Ivy.Amazon.ElasticTranscoder.IoC;
using Ivy.TestHelper;

namespace Ivy.Amazon.ElasticTranscoder.Test.Base
{
    public abstract class ElasticTranscoderTestBase : TestBase
    {
        protected override void InitWrapper()
        {
            base.Init(
                containerGen => containerGen.InstallIvyAmazonElasticTranscoder()
            );
        }
    }
}
