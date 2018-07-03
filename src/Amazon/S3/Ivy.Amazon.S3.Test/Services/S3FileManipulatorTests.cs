using Amazon.S3;
using Amazon.S3.Model;
using Ivy.Amazon.S3.Core.Services;
using Ivy.Amazon.S3.Test.Base;
using Ivy.IoC.Core;
using Moq;
using System;
using System.Threading;
using Xunit;

namespace Ivy.Amazon.S3.Test.Services
{
    public class S3FileManipulatorTests : 
        AmazonS3TestBase<IS3FileManipulator>
    {
        #region Variables & Constants

        private Mock<IAmazonS3> _mockS3Client;

        const string bucketName = "TESTBucket";
        const string objectKey = "TestKey";
        const string newKey = "NewKey";

        #endregion

        #region SetUp & TearDown

        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            _mockS3Client = InitializeMoq<IAmazonS3>(containerGen);
        }

        #endregion

        #region Tests

        #region FileExistsAsync

        [Fact]
        public async void FileExistsAsync_Passes_As_Expected_For_True()
        {
            var response = new GetObjectMetadataResponse();

            _mockS3Client.Setup(x => 
                x.GetObjectMetadataAsync(It.Is<GetObjectMetadataRequest>(y => y.BucketName == bucketName &&
                                                                              y.Key == objectKey), default(CancellationToken))).
                    ReturnsAsync(response);

            Assert.True(await Sut.FileExistsAsync(bucketName, objectKey));

            _mockS3Client.Verify(x =>
                x.GetObjectMetadataAsync(It.Is<GetObjectMetadataRequest>(y => y.BucketName == bucketName &&
                                                                              y.Key == objectKey), default(CancellationToken)),
                    Times.Once);
        }

        [Fact]
        public async void FileExistsAsync_Passes_As_Expected_For_False()
        {
            var err = new AmazonS3Exception("TEST");
            err.StatusCode = System.Net.HttpStatusCode.NotFound;

            _mockS3Client.Setup(x =>
                x.GetObjectMetadataAsync(It.Is<GetObjectMetadataRequest>(y => y.BucketName == bucketName &&
                                                                              y.Key == objectKey), default(CancellationToken))).
                    Throws(err);

            Assert.False(await Sut.FileExistsAsync(bucketName, objectKey));

            _mockS3Client.Verify(x =>
                x.GetObjectMetadataAsync(It.Is<GetObjectMetadataRequest>(y => y.BucketName == bucketName &&
                                                                              y.Key == objectKey), default(CancellationToken)),
                    Times.Once);
        }

        [Fact]
        public async void FileExistsAsync_Throws_Error_As_Expected()
        {
            var err = new Exception();

            _mockS3Client.Setup(x =>
                x.GetObjectMetadataAsync(It.Is<GetObjectMetadataRequest>(y => y.BucketName == bucketName &&
                                                                              y.Key == objectKey), default(CancellationToken))).
                    Throws(err);

            var e = await Assert.ThrowsAsync<Exception>(async () => await Sut.FileExistsAsync(bucketName, objectKey));

            Assert.Same(err, e);

            _mockS3Client.Verify(x =>
                x.GetObjectMetadataAsync(It.Is<GetObjectMetadataRequest>(y => y.BucketName == bucketName &&
                                                                              y.Key == objectKey), default(CancellationToken)),
                    Times.Once);
        }

        #endregion

        #region DeleteFileAsync

        [Fact]
        public async void DeleteFileAsync_Passes_Command_As_Expected()
        {
            _mockS3Client.Setup(x => x.DeleteObjectAsync(bucketName, objectKey, default(CancellationToken)))
                .ReturnsAsync(new DeleteObjectResponse());

            await Sut.DeleteFileAsync(bucketName, objectKey);

            _mockS3Client.Verify(x => x.DeleteObjectAsync(bucketName, objectKey, default(CancellationToken)), 
                Times.Once);
        }

        #endregion

        #region CopyFileAsync

        [Fact]
        public async void CopyFileAsync_Passes_Command_As_Expected()
        {
            _mockS3Client.Setup(x => x.CopyObjectAsync(bucketName, objectKey, bucketName, newKey, default(CancellationToken)))
                .ReturnsAsync(new CopyObjectResponse());

            await Sut.CopyFileAsync(bucketName, objectKey, newKey);

            _mockS3Client.Verify(x => x.CopyObjectAsync(bucketName, objectKey, bucketName, newKey, default(CancellationToken)),
                Times.Once);

        }

        #endregion

        #region MoveFileAsync

        [Fact]
        public async void MoveFileAsync_Passes_Command_As_Expected()
        {
            _mockS3Client.Setup(x => x.DeleteObjectAsync(bucketName, objectKey, default(CancellationToken)))
                .ReturnsAsync(new DeleteObjectResponse());

            _mockS3Client.Setup(x => x.CopyObjectAsync(bucketName, objectKey, bucketName, newKey, default(CancellationToken)))
                .ReturnsAsync(new CopyObjectResponse());

            await Sut.MoveFileAsync(bucketName, objectKey, newKey);

            _mockS3Client.Verify(x => x.DeleteObjectAsync(bucketName, objectKey, default(CancellationToken)), 
                Times.Once);

            _mockS3Client.Verify(x => x.CopyObjectAsync(bucketName, objectKey, bucketName, newKey, default(CancellationToken)),
                Times.Once);
        }

        #endregion

        #endregion
    }
}
