using Ivy.IoC;
using Ivy.TestHelper;
using Ivy.Utility.Core;
using System;
using Xunit;

namespace Ivy.Utility.Test.Helpers
{
    public class ClockTests : TestBase<IClock>
    {
        #region Variables & Constants

        private TimeSpan delta = new TimeSpan(0,0,1);

        #endregion

        #region Tests

        [Fact]
        public void Now()
        {
            AssertClose(DateTime.Now, Sut.Now, delta);
        }

        [Fact]
        public void NowWithOffset()
        {
            AssertClose(DateTimeOffset.Now, Sut.NowWithOffset, delta);
        }

        [Fact]
        public void UtcNow()
        {
            AssertClose(DateTime.UtcNow, Sut.UtcNow, delta);
        }

        [Fact]
        public void UtcNowWithOffset()
        {
            AssertClose(DateTimeOffset.UtcNow, Sut.UtcNowWithOffset, delta);
        }

        #endregion

        #region Helper Methods

        private void AssertClose(DateTimeOffset expected, DateTimeOffset received, TimeSpan dif)
        {
            var low = expected.Add(-dif);
            var high = expected.Add(dif);

            Assert.True(low < received);
            Assert.True(received < high);
        }

        #endregion
    }
}
