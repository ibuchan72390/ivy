using Ivy.Captcha.Core.Interfaces.Services;
using Ivy.Captcha.Test.Base;
using Ivy.IoC;
using Ivy.IoC.Core;
using Ivy.Utility.Core;
using Moq;
using System.Linq;
using Xunit;

namespace Ivy.Captcha.Test.Services
{
    public class CaptchaCodeGeneratorTests : CaptchaTestBase
    {
        #region Variables & Constants

        private readonly ICaptchaCodeGenerator _sut;

        private readonly Mock<IRandomizationHelper> _mockRand;
        
        const string Letters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        #endregion

        #region SetUp & TearDown

        public CaptchaCodeGeneratorTests()
        {
            var containerGen = ServiceLocator.Instance.GetService<IContainerGenerator>();

            base.ConfigureContainer(containerGen);

            _mockRand = new Mock<IRandomizationHelper>();
            containerGen.RegisterInstance<IRandomizationHelper>(_mockRand.Object);

            _sut = containerGen.GenerateContainer().GetService<ICaptchaCodeGenerator>();
        }

        #endregion

        #region Tests

        #region GenerateCaptchaCode

        [Fact]
        public void GenerateCaptchaCode_Generates_As_Expected()
        {
            var maxRand = Letters.Length - 1;
            const int randResult = 0;
            const int captchaLength = 5;

            _mockRand.Setup(x => x.RandomInt(0, maxRand)).
                Returns(randResult);

            var result = _sut.GenerateCaptchaCode(captchaLength);

            var expectedString = Enumerable.Range(0, captchaLength).
                Select(x => Letters[randResult]).
                Select(x => x.ToString()).
                Aggregate((prev, curr) => prev += curr);

            Assert.Equal(captchaLength, result.Length);
            Assert.Equal(expectedString, result);
        }

        #endregion

        #endregion
    }
}
