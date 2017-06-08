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

        const string objectKey = "TEST";

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

        #region Tests

        #region GetS3VideoDownloadKey

        [Fact]
        public void GetS3VideoDownloadKey_Returns_As_Expected()
        {
            const ResolutionTypeName res = ResolutionTypeName.High;

            var resultKey = _sut.GetS3VideoDownloadKey(objectKey, res);

            Assert.Equal(resultKey, $"Videos/{_resolutionTranslator.GetResolutionString(res)}/{objectKey}");
        }

        #endregion

        #region GetS3VideoUploadKey

        [Fact]
        public void GetS3VideoUploadKey_Returns_As_Expected()
        {
            var resultKey = _sut.GetS3VideoUploadKey(objectKey);

            Assert.Equal(resultKey, $"Videos/Upload/{objectKey}");
        }

        #endregion

        #endregion
    }
}
