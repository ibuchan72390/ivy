using Amazon.S3;
using Amazon.S3.Model;
using Ivy.Amazon.S3.Core.Services;
using System.Threading.Tasks;

namespace Ivy.Amazon.S3.Services
{
    public class S3FileValidator : IS3FileValidator
    {
        #region Variables & Constants

        private readonly IAmazonS3 _s3Client;

        #endregion

        #region Constructor

        public S3FileValidator(
            IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }

        #endregion

        #region Public Methods

        public async Task<bool> FileExistsAsync(string bucketName, string objectKey)
        {
            try
            {
                var objectMetadataRequest = new GetObjectMetadataRequest
                {
                    BucketName = bucketName,
                    Key = objectKey
                };

                await _s3Client.GetObjectMetadataAsync(objectMetadataRequest);

                return true;
            }

            catch (AmazonS3Exception ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return false;

                //status wasn't not found, so throw the exception
                throw;
            }
        }

        #endregion
    }
}
