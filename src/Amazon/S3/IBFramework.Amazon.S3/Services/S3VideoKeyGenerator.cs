using iAMGlobalAngular2.Api.Core.Enums;
using IBFramework.Amazon.Core.S3.Services;

namespace iAMGlobalAngular2.Api.Data.Services.S3
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

        public string GetS3VideoKey(string objectKey, ResolutionTypeName resolution)
        {
            //if (s3Ref.S3ContentType.GetEnumValue() != S3ContentTypeName.Video)
            //{
            //    throw new Exception($"Unable to GetS3VideoKey for non-video content! S3Content.Id: {s3Ref.Id}");
            //}

            string massagedResolution = _resolutionTranslator.GetResolutionString(resolution);

            return $"Videos/{massagedResolution}/{objectKey}";
        }

        #endregion
    }
}
