using Amazon.S3;
using Amazon.S3.Model;
using iAMGlobalAngular2.Api.Core.Enums;
using IBFramework.Amazon.Core.S3.Services;
using IBFramework.Utility.Core;
using System;
using Microsoft.Extensions.Logging;

namespace iAMGlobalAngular2.Api.Data.Services.S3
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


        public string GetCloudFrontSignedFileUrl(string bucketName, string objectKey)
        {
            return PrivateGetFileAsync(bucketName, objectKey);
        }

        public string GetCloudFrontSignedVideoUrl(string bucketName, string objectKey, string resolution)
        {
            ResolutionTypeName resolutionEnum;
            bool success = Enum.TryParse(resolution, out resolutionEnum);

            if (!success)
            {
                throw new Exception($"Unable to parse the received resolution to a ResolutionTypeName.  Received: {resolution}");
            }

            var videoKey = _videoKeyGen.GetS3VideoKey(objectKey, resolutionEnum);

            return PrivateGetFileAsync(bucketName, videoKey);
        }

        #endregion

        #region Helper Methods

        private string PrivateGetFileAsync(string bucketName, string fileKey)
        {
            GetPreSignedUrlRequest request = new GetPreSignedUrlRequest
            {
                BucketName = bucketName,
                Key = fileKey,

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
