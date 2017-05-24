using Ivy.Amazon.S3.IoC;
using Ivy.TestHelper;
using Microsoft.Extensions.DependencyInjection;

namespace Ivy.Amazon.S3.Test.Base
{
    public class AmazonS3TestBase : TestBase
    {
        protected override void InitWrapper()
        {
            base.Init(
                containerGen => containerGen.InstallIvyAmazonS3(),
                svcColl => svcColl.AddLogging());
        }
    }
}
