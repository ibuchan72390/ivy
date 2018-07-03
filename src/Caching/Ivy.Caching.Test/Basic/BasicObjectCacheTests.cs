using Ivy.Caching.Core;
using Ivy.Caching.Test.Base;
using Ivy.IoC;
using Ivy.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Ivy.Caching.Test.Basic
{
    public class BasicObjectCacheTests : 
        BasicCachingTestBase<IObjectCache<TestClass>>
    {
        [Fact]
        public void ObjectCache_Initialization_Is_Pointless_After_First_Initialization()
        {
            var init1Class = new TestClass();
            var init2Class = new TestClass();

            Func<TestClass> init1 = () => init1Class;
            Func<TestClass> init2 = () => init2Class;

            Sut.Init(init1);

            Assert.Same(init1Class, Sut.GetCache());

            Sut.Init(init2);

            Assert.Same(init1Class, Sut.GetCache());
            Assert.NotSame(init2Class, Sut.GetCache());
        }

        [Fact]
        public void Second_Init_Doesnt_Change_Loading_Method_If_Already_Initialized_Even_If_Cache_Needs_Refresh()
        {
            var init1Class = new TestClass();
            var init2Class = new TestClass();

            Func<TestClass> init1 = () => init1Class;
            Func<TestClass> init2 = () => init2Class;

            Sut.Init(init1);

            Assert.Same(init1Class, Sut.GetCache());

            Sut.RefreshCache();

            Sut.Init(init2);

            Assert.Same(init1Class, Sut.GetCache());
            Assert.NotSame(init2Class, Sut.GetCache());
        }

        [Fact]
        public void Parallel_Caches_Dont_Affect_Each_Other()
        {
            var testClassCache = new TestClass();
            var testClassCollectionCache = Enumerable.Range(0, 4).Select(x => new TestClass());

            Sut.Init(() => testClassCache);

            var collectionCache = TestContainer.GetService<IObjectCache<IEnumerable<TestClass>>>();
            collectionCache.Init(() => testClassCollectionCache);

            var resultEntityCache = Sut.GetCache();
            Assert.Same(testClassCache, resultEntityCache);

            var resultCollectionCache = collectionCache.GetCache();
            Assert.Same(testClassCollectionCache, resultCollectionCache);
        }

        [Fact]
        public void ObjectCache_Can_Create_Persistent_Cache()
        {
            var myClasses = Enumerable.Range(1, 5).Select(x => new TestClass { Integer = x }).ToList();

            //var myCache = TestServiceLocator.StaticContainer.GetService<IObjectCache<IList<TestClass>>>(typeof(IObjectCache<IList<TestClass>>));
            var myCache = TestContainer.GetService<IObjectCache<IList<TestClass>>>();

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

            var myCache = TestContainer.GetService<IObjectCache<IList<TestClass>>>();

            myCache.Init(() => myClasses);

            var myResolvedCache = TestContainer.GetService<IObjectCache<IList<TestClass>>>();

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
            Sut.Init(() => new TestClass());

            var cacheObj1 = Sut.GetCache();

            var cacheObj2 = Sut.GetCache();

            Assert.Same(cacheObj1, cacheObj2);

            Sut.RefreshCache();

            var cacheObj3 = Sut.GetCache();

            Assert.NotSame(cacheObj3, cacheObj1);
            Assert.NotSame(cacheObj3, cacheObj2);
        }
    }
}