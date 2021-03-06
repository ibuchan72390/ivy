﻿using Ivy.Captcha.Core.Interfaces.Services;
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
    public class CaptchaGenerationServiceTests : 
        CaptchaDrawingTestBase<ICaptchaGenerationService>
    {
        #region Variables & Constants

        private Mock<IRandomizationHelper> _mockCodeGen;
        private Mock<ICaptchaImageHelper> _mockImageHelper;
        private Mock<ICaptchaDrawingService> _mockDrawingService;
        
        #endregion

        #region SetUp & TearDown

        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            _mockCodeGen = InitializeMoq<IRandomizationHelper>(containerGen);
            _mockImageHelper = InitializeMoq<ICaptchaImageHelper>(containerGen);
            _mockDrawingService = InitializeMoq<ICaptchaDrawingService>(containerGen);
        }

        #endregion

        #region Tests

        [Fact]
        public void GenerateCaptchaImage_Throws_Exception_For_Invalid_Height()
        {
            const string err = "Both height and width need to be greater than 0!";
            var e = Assert.Throws<Exception>(() => Sut.GenerateCaptchaImage(5, 5, 0));
            Assert.Equal(err, e.Message);
        }

        [Fact]
        public void GenerateCaptchaImage_Throws_Exception_For_Invalid_Width()
        {
            const string err = "Both height and width need to be greater than 0!";
            var e = Assert.Throws<Exception>(() => Sut.GenerateCaptchaImage(5, 0, 5));
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
            var result = Sut.GenerateCaptchaImage(charLength, width, height);

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
