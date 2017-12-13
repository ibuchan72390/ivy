﻿using Ivy.IoC;
using Ivy.TestHelper;
using Ivy.Utility.Core;
using System;
using Xunit;

namespace Ivy.Utility.Test.Helpers
{
    public class ClockTests : TestBase
    {
        private IClock _sut;
        private TimeSpan delta = new TimeSpan(0,0,1);

        public ClockTests()
        {
            _sut = ServiceLocator.Instance.GetService<IClock>();
        }

        [Fact]
        public void Now()
        {
            AssertClose(DateTime.Now, _sut.Now, delta);
        }

        [Fact]
        public void NowWithOffset()
        {
            AssertClose(DateTimeOffset.Now, _sut.NowWithOffset, delta);
        }

        [Fact]
        public void UtcNow()
        {
            AssertClose(DateTime.UtcNow, _sut.UtcNow, delta);
        }

        [Fact]
        public void UtcNowWithOffset()
        {
            AssertClose(DateTimeOffset.UtcNow, _sut.UtcNowWithOffset, delta);
        }

        private void AssertClose(DateTimeOffset expected, DateTimeOffset received, TimeSpan dif)
        {
            var low = expected.Add(-dif);
            var high = expected.Add(dif);

            Assert.True(low < received);
            Assert.True(received < high);
        }
    }
}
