using Ivy.IoC;
using Ivy.IoC.Core;
using Ivy.TestHelper;
using Ivy.Utility.Core;
using Ivy.Utility.Core.Helpers;
using Moq;
using System;
using Xunit;

namespace Ivy.Utility.Test.Helpers
{
    public class RandomGeneratorTests : TestBase<IRandomGenerator>
    {
        #region Variables & Cosntants

        private Mock<IClock> _mockClock;
        private readonly DateTime _mockNow = new DateTime(2011, 10, 10);

        #endregion

        #region SetUp & TearDown

        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            _mockClock = InitializeMoq<IClock>(containerGen);
            _mockClock.Setup(x => x.UtcNow).Returns(_mockNow);
        }

        #endregion

        #region Tests

        [Fact]
        public void RandomGenerator_Generates_Random_With_Clock_Now()
        {
            _mockClock.Verify(x => x.UtcNow, Times.Once);
        }

        [Fact]
        public void RandomGenerator_Returns_The_Same_Instance_Of_Random_Every_Time()
        {
            var rand1 = Sut.GetRandom();
            var rand2 = Sut.GetRandom();
            var rand3 = Sut.GetRandom();

            Assert.Same(rand1, rand2);
            Assert.Same(rand2, rand3);
            Assert.Same(rand1, rand3);
        }

        #endregion
    }
}
