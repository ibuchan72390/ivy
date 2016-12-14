using IBFramework.Core.Utility;
using IBFramework.TestHelper;
using System;
using Xunit;

namespace IBFramework.Utility.Test
{
    public class ClockTests : TestBase
    {
        private IClock _sut;

        public ClockTests()
        {
            _sut = TestServiceLocator.StaticContainer.Resolve<IClock>();
        }

        [Fact]
        public void Now()
        {
            Assert.Same(DateTime.Now, _sut.Now);
        }

        [Fact]
        public void NowWithOffset()
        {
            Assert.Same(DateTimeOffset.Now, _sut.NowWithOffset);
        }

        [Fact]
        public void UtcNow()
        {
            Assert.Same(DateTime.UtcNow, _sut.UtcNow);
        }

        [Fact]
        public void UtcNowWithOffset()
        {
            Assert.Same(DateTimeOffset.UtcNow, _sut.UtcNowWithOffset);
        }
    }
}
