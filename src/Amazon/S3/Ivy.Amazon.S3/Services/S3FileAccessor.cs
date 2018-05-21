using Amazon.S3;
using Ivy.Amazon.S3.Core.Services;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Ivy.Amazon.S3.Services
{
    public class S3FileAccessor : IS3FileAccessor
    {
        #region Variables & Constants

        private readonly IAmazonS3 _s3Svc;

        private readonly IDictionary<string, object> additionalProps = 
            new Dictionary<string, object>();

        #endregion

        #region Constructor

        public S3FileAccessor(
            IAmazonS3 s3Svc)
        {
            _s3Svc = s3Svc;
        }

        #endregion

        #region Public Methods

        public async Task<Stream> GetFileStreamFromS3Async(string bucketName, string objectKey)
        {
            return await _s3Svc.GetObjectStreamAsync(bucketName, objectKey, additionalProps);
        }

        public async Task SaveFileStreamToS3Async(string bucketName, string objectKey, Stream fileStream)
        {
            await _s3Svc.UploadObjectFromStreamAsync(bucketName, objectKey, fileStream, additionalProps);
        }

        #endregion
    }
}
