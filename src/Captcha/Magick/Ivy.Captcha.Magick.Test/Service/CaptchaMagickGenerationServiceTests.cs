using Ivy.Captcha.Core.Interfaces.Services;
using Ivy.Captcha.Magick.Test.Base;
using Ivy.IoC;
using System;
using Xunit;

namespace Ivy.Captcha.Magick.Test.Service
{
    public class CaptchaMagickGenerationServiceTests : 
        CaptchaMagickTestBase<ICaptchaGenerationService>
    {
        #region Tests

        [Fact]
        public void Generate_Works()
        {
            var result = Sut.GenerateCaptchaImage(5, 512, 256);

            Console.WriteLine("Code is: " + result.CaptchaCode);
            Console.WriteLine("Byte Array Is: " + result.CaptchaByteData);
        }

        #endregion
    }
}
