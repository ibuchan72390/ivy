using Amazon.S3;
using Amazon.S3.Model;
using Ivy.Utility.Core;
using Microsoft.Extensions.Logging;
using Ivy.Amazon.S3.Core.Services;
using Ivy.Amazon.S3.Core.Enums;

namespace Ivy.Amazon.S3.Services
{
    public class S3FileService : IS3FileService
    {
        #region Variables & Constants

        private readonly IAmazonS3 _s3ClientService;
        private readonly IS3UrlHelper _urlHelper;
        private readonly IS3VideoKeyGenerator _videoKeyGen;

        private readonly ILogger<IS3FileService> _logger;
        private readonly IClock _clock;

        #endregion

        #region Constructor

        public S3FileService(
            IAmazonS3 s3clientService,
            IS3UrlHelper urlHelper,
            IS3VideoKeyGenerator videoKeyGen,
            ILogger<IS3FileService> logger,
            IClock clock)
        {
            _s3ClientService = s3clientService;
            _urlHelper = urlHelper;
            _videoKeyGen = videoKeyGen;

            _logger = logger;
            _clock = clock;
        }

        #endregion

        #region Public Methods

        public string GetCloudFrontSignedFileDownloadUrl(string bucketName, string objectKey)
        {
            return PrivateGetFileUrlAsync(bucketName, objectKey, HttpVerb.GET);
        }

        public string GetCloudFrontSignedVideoDownloadUrl(string bucketName, string objectKey, ResolutionTypeName resolution)
        {
            var videoKey = _videoKeyGen.GetS3VideoDownloadKey(objectKey, resolution);

            return PrivateGetFileUrlAsync(bucketName, videoKey, HttpVerb.GET);
        }

        public string GetCloudFrontSignedFileUploadUrl(string bucketName, string objectKey)
        {
            return PrivateGetFileUrlAsync(bucketName, objectKey, HttpVerb.PUT);
        }

        public string GetCloudFrontSignedVideoUploadUrl(string bucketName, string objectKey)
        {
            var videoKey = _videoKeyGen.GetS3VideoUploadKey(objectKey);

            return PrivateGetFileUrlAsync(bucketName, videoKey, HttpVerb.PUT);
        }

        #endregion

        #region Helper Methods

        private string PrivateGetFileUrlAsync(string bucketName, string fileKey, HttpVerb verb)
        {
            GetPreSignedUrlRequest request = new GetPreSignedUrlRequest
            {
                BucketName = bucketName,
                Key = fileKey,
                Verb = verb,

                // I think this will do the trick?
                // It seems to indicate that once the connection is established, the download should complete.
                // The user will only run into issues if the download fails and they attempt to re-initiate
                // If they really run past 1 minutes, we have much greater issues.
                Expires = _clock.Now.AddMinutes(1)
            };

            return _s3ClientService.GetPreSignedURL(request);
        }

        #endregion
    }
}
