using Ivy.Amazon.S3.Core.Enums;
using Ivy.Amazon.S3.Core.Services;
using Ivy.Amazon.S3.Test.Base;
using Ivy.IoC;
using Xunit;

namespace Ivy.Amazon.S3.Test.Services
{
    public class S3ResolutionTranslatorTests : AmazonS3TestBase
    {
        #region Variables & Constants

        private readonly IS3ResolutionTranslator _sut;

        #endregion

        #region SetUp & TearDown

        public S3ResolutionTranslatorTests()
        {
            _sut = ServiceLocator.Instance.Resolve<IS3ResolutionTranslator>();
        }

        #endregion

        #region Tests

        [Fact]
        public void S3ResolutionTranslator_Works_As_Expected_For_High_Resolution()
        {
            TestTranslation(ResolutionTypeName.High, "1080");
        }

        [Fact]
        public void S3ResolutionTranslator_Works_As_Expected_For_Medium_Resolution()
        {
            TestTranslation(ResolutionTypeName.Medium, "720");
        }

        [Fact]
        public void S3ResolutionTranslator_Works_As_Expected_For_Low_Resolution()
        {
            TestTranslation(ResolutionTypeName.Low, "480");
        }

        #endregion

        #region Helper Method

        private void TestTranslation(ResolutionTypeName resolution, string result)
        {
            Assert.Equal(result, _sut.GetResolutionString(resolution));
        }

        #endregion
    }
}
