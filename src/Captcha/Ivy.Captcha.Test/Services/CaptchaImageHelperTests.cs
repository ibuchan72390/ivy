using Ivy.Captcha.Core.Interfaces.Services;
using Ivy.Captcha.Test.Base;
using Ivy.IoC;
using Ivy.IoC.Core;
using Ivy.Utility.Core;
using Moq;
using System;
using Xunit;

namespace Ivy.Captcha.Test.Services
{
    public class CaptchaImageHelperTests : CaptchaTestBase
    {
        #region Variables & Constants

        private readonly ICaptchaImageHelper _sut;

        private readonly Mock<IRandomizationHelper> _mockRand;

        #endregion

        #region SetUp & TearDown

        public CaptchaImageHelperTests()
        {
            var containerGen = ServiceLocator.Instance.GetService<IContainerGenerator>();

            base.ConfigureContainer(containerGen);

            _mockRand = new Mock<IRandomizationHelper>();
            containerGen.RegisterInstance<IRandomizationHelper>(_mockRand.Object);

            _sut = containerGen.GenerateContainer().GetService<ICaptchaImageHelper>();
        }

        #endregion

        #region Tests

        #region GetFontSize
        
        [Fact]
        public void GetFontSize_Executes_As_Expected()
        {
            // Arrange
            const int width = 500;
            const int codeCount = 20;

            var expected = Convert.ToInt32(width / codeCount);

            // Act
            var result = _sut.GetFontSize(width, codeCount);

            // Assert
            Assert.Equal(expected, result);
        }

        #endregion

        #region GetRandomDeepColor

        [Fact]
        public void GetRandomDeepColor_Executes_As_Expected()
        {
            // Arrange
            const int randLow = 0;
            const int randHigh = 160;
            const int randResult = 2;

            _mockRand.Setup(x => x.RandomInt(randLow, randHigh)).
                Returns(randResult);

            // Act
            var result = _sut.GetRandomDeepColor();

            // Assert
            _mockRand.Verify(x => x.RandomInt(randLow, randHigh),
                Times.Exactly(3));

            Assert.Equal(randResult, result.R);
            Assert.Equal(randResult, result.B);
            Assert.Equal(randResult, result.G);
        }

        #endregion

        #region GetRandomLightColor

        [Fact]
        public void GetRandomLightColor_Executes_As_Expected()
        {
            // Arrange
            const int randLow = 180;
            const int randHigh = 255;
            const int randResult = 195;

            _mockRand.Setup(x => x.RandomInt(randLow, randHigh)).
                Returns(randResult);

            // Act
            var result = _sut.GetRandomLightColor();

            // Assert
            _mockRand.Verify(x => x.RandomInt(randLow, randHigh),
                Times.Exactly(3));

            Assert.Equal(randResult, result.R);
            Assert.Equal(randResult, result.B);
            Assert.Equal(randResult, result.G);
        }

        #endregion

        #endregion
    }
}
