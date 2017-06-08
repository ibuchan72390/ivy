using Ivy.Amazon.S3.Core.Enums;
using Ivy.Amazon.S3.Core.Services;

namespace Ivy.Amazon.S3.Services
{
    public class S3VideoKeyGenerator : IS3VideoKeyGenerator
    {
        #region Variables & Constants

        private readonly IS3ResolutionTranslator _resolutionTranslator;

        #endregion

        #region Constructor

        public S3VideoKeyGenerator(IS3ResolutionTranslator resolutionTranslator)
        {
            _resolutionTranslator = resolutionTranslator;
        }

        #endregion

        #region Public Methods

        public string GetS3VideoDownloadKey(string objectKey, ResolutionTypeName resolution)
        {
            string massagedResolution = _resolutionTranslator.GetResolutionString(resolution);

            return $"Videos/{massagedResolution}/{objectKey}";
        }

        public string GetS3VideoUploadKey(string objectKey)
        {
            return $"Videos/Upload/{objectKey}";
        }

        #endregion
    }
}
