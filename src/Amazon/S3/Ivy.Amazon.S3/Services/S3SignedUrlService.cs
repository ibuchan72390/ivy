using Amazon.S3;
using Amazon.S3.Model;
using Ivy.Utility.Core;
using Microsoft.Extensions.Logging;
using Ivy.Amazon.S3.Core.Services;

namespace Ivy.Amazon.S3.Services
{
    public class S3SignedUrlService : IS3SignedUrlService
    {
        #region Variables & Constants

        private readonly IAmazonS3 _s3ClientService;
        private readonly IS3UrlHelper _urlHelper;

        private readonly ILogger<IS3SignedUrlService> _logger;
        private readonly IClock _clock;

        #endregion

        #region Constructor

        public S3SignedUrlService(
            IAmazonS3 s3clientService,
            IS3UrlHelper urlHelper,
            ILogger<IS3SignedUrlService> logger,
            IClock clock)
        {
            _s3ClientService = s3clientService;
            _urlHelper = urlHelper;

            _logger = logger;
            _clock = clock;
        }

        #endregion

        #region Public Methods

        public string GetS3SignedFileDownloadUrl(string bucketName, string objectKey, string downloadFileNameOverride = null)
        {
            return PrivateGetFileUrlAsync(bucketName, objectKey, HttpVerb.GET, downloadFileNameOverride);
        }

        public string GetS3SignedFileUploadUrl(string bucketName, string objectKey)
        {
            return PrivateGetFileUrlAsync(bucketName, objectKey, HttpVerb.PUT);
        }

        #endregion

        #region Helper Methods

        private string PrivateGetFileUrlAsync(string bucketName, string fileKey, HttpVerb verb, string fileNameContentDisposition = null)
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

            if (fileNameContentDisposition != null)
            {
                request.ResponseHeaderOverrides.ContentDisposition = $"attachment: filename={fileNameContentDisposition}";
            }

            return _s3ClientService.GetPreSignedURL(request);
        }

        #endregion
    }
}
