using Ivy.Caching.IoC;
using Ivy.TestHelper;
using Microsoft.Extensions.DependencyInjection;

namespace Ivy.Caching.Test.Base
{
    public class BasicCachingTestBase : TestBase
    {
        public BasicCachingTestBase()
        {
            base.Init(
                containerGen => {

                    containerGen.InstallIvyBasicCaching();
                },
                svcColl => {

                    svcColl.AddMemoryCache();
                }    
            );
        }
    }
}
