using IBFramework.Caching.Core;
using IBFramework.IoC;
using IBFramework.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace IBFramework.Caching.Test
{
    public class ObjectCacheTests : TestBase
    {
        [Fact]
        public void ObjectCache_Initialization_Is_Pointless_After_First_Initialization()
        {
            var init1Class = new TestClass();
            var init2Class = new TestClass();

            Func<TestClass> init1 = () => init1Class;
            Func<TestClass> init2 = () => init2Class;

            var myCache = ServiceLocator.Instance.Resolve<IObjectCache<TestClass>>();
            myCache.Init(init1);

            Assert.Same(init1Class, myCache.GetCache());

            myCache.Init(init2);

            Assert.Same(init1Class, myCache.GetCache());
            Assert.NotSame(init2Class, myCache.GetCache());
        }

        [Fact]
        public void Second_Init_Doesnt_Change_Loading_Method_If_Already_Initialized_Even_If_Cache_Needs_Refresh()
        {
            var init1Class = new TestClass();
            var init2Class = new TestClass();

            Func<TestClass> init1 = () => init1Class;
            Func<TestClass> init2 = () => init2Class;

            var myCache = ServiceLocator.Instance.Resolve<IObjectCache<TestClass>>();
            myCache.Init(init1);

            Assert.Same(init1Class, myCache.GetCache());

            myCache.RefreshCache();

            myCache.Init(init2);

            Assert.Same(init1Class, myCache.GetCache());
            Assert.NotSame(init2Class, myCache.GetCache());
        }

        [Fact]
        public void ObjectCache_Can_Create_Persistent_Cache()
        {
            var myClasses = Enumerable.Range(1, 5).Select(x => new TestClass { Integer = x }).ToList();

            //var myCache = TestServiceLocator.StaticContainer.Resolve<IObjectCache<IList<TestClass>>>(typeof(IObjectCache<IList<TestClass>>));
            var myCache = ServiceLocator.Instance.Resolve<IObjectCache<IList<TestClass>>>();

            myCache.Init(() => myClasses);

            var obtainedCache = myCache.GetCache();

            foreach (var item in obtainedCache)
            {
                var matchingOrig = myClasses.First(x => x.Integer == item.Integer);
                Assert.Same(item, matchingOrig);
            }
        }

        [Fact]
        public void ObjectCache_Can_Create_Persistent_Cache_Maintained_Through_Resolutions()
        {
            var myClasses = Enumerable.Range(1, 5).Select(x => new TestClass { Integer = x }).ToList();

            var myCache = ServiceLocator.Instance.Resolve<IObjectCache<IList<TestClass>>>();

            myCache.Init(() => myClasses);

            var myResolvedCache = ServiceLocator.Instance.Resolve<IObjectCache<IList<TestClass>>>();

            var obtainedCache = myResolvedCache.GetCache();

            foreach (var item in obtainedCache)
            {
                var matchingOrig = myClasses.First(x => x.Integer == item.Integer);
                Assert.Same(item, matchingOrig);
            }
        }

        [Fact]
        public void ObjectCache_Can_Properly_Refresh_Itself()
        {
            var objectCache = ServiceLocator.Instance.Resolve<IObjectCache<TestClass>>();

            objectCache.Init(() => new TestClass());

            var cacheObj1 = objectCache.GetCache();

            var cacheObj2 = objectCache.GetCache();

            Assert.Same(cacheObj1, cacheObj2);

            objectCache.RefreshCache();

            var cacheObj3 = objectCache.GetCache();

            Assert.NotSame(cacheObj3, cacheObj1);
            Assert.NotSame(cacheObj3, cacheObj2);
        }
    }
}
