using AmazonS3 = Amazon.S3;
using Ivy.Amazon.S3.Core.Services;
using System.Threading.Tasks;

namespace Ivy.Amazon.S3.Services
{
    public class S3FileManipulator : IS3FileManipulator
    {
        #region Variables & Constants

        private AmazonS3.IAmazonS3 _s3Client;

        #endregion

        #region Constructor

        public S3FileManipulator(
            AmazonS3.IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }

        #endregion

        #region Public Methods

        public async Task<bool> FileExistsAsync(string bucketName, string objectKey)
        {
            try
            {
                var req = new AmazonS3.Model.GetObjectMetadataRequest
                {
                    BucketName = bucketName,
                    Key = objectKey
                };

                var response = await _s3Client.GetObjectMetadataAsync(req);

                return true;
            }

            catch (AmazonS3.AmazonS3Exception ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return false;

                //status wasn't not found, so throw the exception
                throw;
            }
        }

        public async Task DeleteFileAsync(string bucketName, string originalObjectKey)
        {
            await _s3Client.DeleteObjectAsync(bucketName, originalObjectKey);
        }

        public async Task CopyFileAsync(string bucketName, string originalObjectKey, string newObjectKey)
        {
            await _s3Client.CopyObjectAsync(bucketName, originalObjectKey, bucketName, newObjectKey);
        }

        public async Task MoveFileAsync(string bucketName, string originalObjectKey, string newObjectKey)
        {
            // Copy the old to the new
            await CopyFileAsync(bucketName, originalObjectKey, newObjectKey);
            
            // Delete the old
            await DeleteFileAsync(bucketName, originalObjectKey);
        }

        #endregion
    }
}
