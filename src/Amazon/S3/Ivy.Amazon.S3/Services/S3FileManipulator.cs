using Amazon.S3;
using Amazon.S3.Model;
using Ivy.Amazon.S3.Core.Services;
using System;
using System.Threading.Tasks;

namespace Ivy.Amazon.S3.Services
{
    public class S3FileManipulator : IS3FileManipulator
    {
        #region Variables & Constants

        private IAmazonS3 _s3Client;

        #endregion

        #region Constructor

        public S3FileManipulator(
            IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }

        #endregion

        #region Public Methods

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
