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
    public class RandomGeneratorTests : TestBase
    {
        #region Variables & Cosntants

        private readonly IRandomGenerator _sut;

        private readonly Mock<IClock> _mockClock;
        private readonly DateTime _mockNow = new DateTime(2011, 10, 10);

        #endregion

        #region SetUp & TearDown

        public RandomGeneratorTests()
        {
            _mockClock = new Mock<IClock>();
            _mockClock.Setup(x => x.UtcNow).Returns(_mockNow);

            var containerGen = ServiceLocator.Instance.Resolve<IContainerGenerator>();

            base.ConfigureContainer(containerGen);

            containerGen.RegisterInstance<IClock>(_mockClock.Object);

            _sut = containerGen.GenerateContainer().Resolve<IRandomGenerator>();
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
            var rand1 = _sut.GetRandom();
            var rand2 = _sut.GetRandom();
            var rand3 = _sut.GetRandom();

            Assert.Same(rand1, rand2);
            Assert.Same(rand2, rand3);
            Assert.Same(rand1, rand3);
        }

        #endregion
    }
}
