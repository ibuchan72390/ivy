using Ivy.Amazon.ElasticTranscoder.IoC;
using Ivy.IoC.Core;
using Ivy.TestHelper;

namespace Ivy.Amazon.ElasticTranscoder.Test.Base
{
    public abstract class ElasticTranscoderTestBase<T> : TestBase<T>
        where T: class
    {
        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            containerGen.InstallIvyAmazonElasticTranscoder();
        }
    }
}
