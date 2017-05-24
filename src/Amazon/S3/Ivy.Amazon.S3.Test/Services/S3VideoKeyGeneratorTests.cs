using Ivy.Amazon.S3.Core.Enums;
using Ivy.Amazon.S3.Core.Services;
using Ivy.Amazon.S3.Test.Base;
using Ivy.IoC;
using Xunit;

namespace Ivy.Amazon.S3.Test.Services
{
    public class S3VideoKeyGeneratorTests : AmazonS3TestBase
    {
        #region Variables & Constants

        private readonly IS3VideoKeyGenerator _sut;

        private readonly IS3ResolutionTranslator _resolutionTranslator;

        #endregion

        #region Constructor

        public S3VideoKeyGeneratorTests()
        {
            _sut = ServiceLocator.Instance.Resolve<IS3VideoKeyGenerator>();

            _resolutionTranslator = ServiceLocator.Instance.Resolve<IS3ResolutionTranslator>();
        }

        #endregion

        #region Public Methods

        [Fact]
        public void GetS3VideoKey_Returns_As_Expected()
        {
            const ResolutionTypeName res = ResolutionTypeName.High;
            const string objectKey = "TEST";

            var resultKey = _sut.GetS3VideoKey(objectKey, res);

            Assert.Equal(resultKey, $"Videos/{_resolutionTranslator.GetResolutionString(res)}/{objectKey}");
        }

        #endregion
    }
}
