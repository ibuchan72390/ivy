using Ivy.Amazon.S3.IoC;
using Ivy.IoC.Core;
using Ivy.TestHelper;
using Microsoft.Extensions.DependencyInjection;

namespace Ivy.Amazon.S3.Test.Base
{
    public class AmazonS3TestBase<T> : TestBase<T>
        where T: class
    {
        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            containerGen.InstallIvyAmazonS3();

            var svcColl = containerGen.GetServiceCollection();

            svcColl.AddLogging();
        }
    }
}
