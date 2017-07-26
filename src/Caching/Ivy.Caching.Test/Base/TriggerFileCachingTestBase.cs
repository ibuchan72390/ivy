using Ivy.Caching.IoC;
using Ivy.TestHelper;
using Microsoft.Extensions.DependencyInjection;

namespace Ivy.Caching.Test.Base
{
    public class TriggerFileCachingTestBase : TestBase
    {
        public TriggerFileCachingTestBase()
        {
            base.Init(
                containerGen => {

                    containerGen.InstallIvyTriggerFileCaching();
                },
                svcColl => {

                    svcColl.AddMemoryCache();
                }
            );
        }
    }
}
