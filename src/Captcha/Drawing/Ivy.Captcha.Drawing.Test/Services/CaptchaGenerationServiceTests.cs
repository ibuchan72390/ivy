using Ivy.Captcha.Core.Interfaces.Services;
using Ivy.Captcha.Drawing.Core.Interfaces.Services;
using Ivy.Captcha.Drawing.Test.Base;
using Ivy.IoC;
using Ivy.IoC.Core;
using Ivy.Utility.Core;
using Moq;
using System;
using System.Drawing;
using Xunit;

namespace Ivy.Captcha.Drawing.Test.Services
{
    public class CaptchaGenerationServiceTests : CaptchaDrawingTestBase
    {
        #region Variables & Constants

        private readonly ICaptchaGenerationService _sut;

        private readonly Mock<IRandomizationHelper> _mockCodeGen;
        private readonly Mock<ICaptchaImageHelper> _mockImageHelper;
        private readonly Mock<ICaptchaDrawingService> _mockDrawingService;
        
        #endregion

        #region SetUp & TearDown

        public CaptchaGenerationServiceTests()
        {
            var containerGen = ServiceLocator.Instance.GetService<IContainerGenerator>();

            base.ConfigureContainer(containerGen);

            _mockCodeGen = new Mock<IRandomizationHelper>();
            containerGen.RegisterInstance<IRandomizationHelper>(_mockCodeGen.Object);

            _mockImageHelper = new Mock<ICaptchaImageHelper>();
            containerGen.RegisterInstance<ICaptchaImageHelper>(_mockImageHelper.Object);

            _mockDrawingService = new Mock<ICaptchaDrawingService>();
            containerGen.RegisterInstance<ICaptchaDrawingService>(_mockDrawingService.Object);

            _sut = containerGen.GenerateContainer().GetService<ICaptchaGenerationService>();
        }

        #endregion

        #region Tests
        
        [Fact]
        public void GenerateCaptchaImage_Throws_Exception_For_Invalid_Height()
        {
            const string err = "Both height and width need to be greater than 0!";
            var e = Assert.Throws<Exception>(() => _sut.GenerateCaptchaImage(5, 5, 0));
            Assert.Equal(err, e.Message);
        }

        [Fact]
        public void GenerateCaptchaImage_Throws_Exception_For_Invalid_Width()
        {
            const string err = "Both height and width need to be greater than 0!";
            var e = Assert.Throws<Exception>(() => _sut.GenerateCaptchaImage(5, 0, 5));
            Assert.Equal(err, e.Message);
        }

        [Fact]
        public void GenerateCaptchaImage_Executes_As_Expected()
        {
            // Arrange
            const int charLength = 5;
            const int width = 1;
            const int height = 1;
            const string code = "TEST";

            _mockCodeGen.Setup(x => x.RandomString(charLength)).
                Returns(code);

            _mockDrawingService.Setup(x => x.DrawCaptchaCode(width, height, code, It.IsAny<Graphics>()));
            _mockDrawingService.Setup(x => x.DrawDisorderLine(width, height, It.IsAny<Graphics>()));
            _mockDrawingService.Setup(x => x.UnsafeAdjustRippleEffect(It.IsAny<Bitmap>()));

            // Act
            var result = _sut.GenerateCaptchaImage(charLength, width, height);

            // Assert
            _mockCodeGen.Verify(x => x.RandomString(charLength), Times.Once);
            _mockDrawingService.Verify(x => x.DrawCaptchaCode(width, height, code, It.IsAny<Graphics>()), Times.Once);
            _mockDrawingService.Verify(x => x.DrawDisorderLine(width, height, It.IsAny<Graphics>()), Times.Once);
            _mockDrawingService.Verify(x => x.UnsafeAdjustRippleEffect(It.IsAny<Bitmap>()), Times.Once);

            Assert.Equal(code, result.CaptchaCode);
            Assert.Equal(120, result.CaptchaByteData.Length);
        }
        
        #endregion
    }
}
