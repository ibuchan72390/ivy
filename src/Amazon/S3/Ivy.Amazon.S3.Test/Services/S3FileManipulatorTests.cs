using Amazon.S3;
using Amazon.S3.Model;
using Ivy.Amazon.S3.Core.Services;
using Ivy.Amazon.S3.Test.Base;
using Ivy.IoC;
using Ivy.IoC.Core;
using Moq;
using System.Threading;
using Xunit;

namespace Ivy.Amazon.S3.Test.Services
{
    public class S3FileManipulatorTests : AmazonS3TestBase
    {
        #region Variables & Constants

        private IS3FileManipulator _sut;

        private Mock<IAmazonS3> _mockS3Client;

        const string bucketName = "TESTBucket";
        const string objectKey = "TestKey";
        const string newKey = "NewKey";

        #endregion

        #region SetUp & TearDown

        public S3FileManipulatorTests()
        {
            var containerGen = ServiceLocator.Instance.Resolve<IContainerGenerator>();

            base.ConfigureContainer(containerGen);

            _mockS3Client = new Mock<IAmazonS3>();
            containerGen.RegisterInstance<IAmazonS3>(_mockS3Client.Object);

            _sut = containerGen.GenerateContainer().Resolve<IS3FileManipulator>();
        }

        #endregion

        #region Tests

        #region DeleteFileAsync

        [Fact]
        public async void DeleteFileAsync_Passes_Command_As_Expected()
        {
            _mockS3Client.Setup(x => x.DeleteObjectAsync(bucketName, objectKey, default(CancellationToken)))
                .ReturnsAsync(new DeleteObjectResponse());

            await _sut.DeleteFileAsync(bucketName, objectKey);

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

            await _sut.CopyFileAsync(bucketName, objectKey, newKey);

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

            await _sut.MoveFileAsync(bucketName, objectKey, newKey);

            _mockS3Client.Verify(x => x.DeleteObjectAsync(bucketName, objectKey, default(CancellationToken)), 
                Times.Once);

            _mockS3Client.Verify(x => x.CopyObjectAsync(bucketName, objectKey, bucketName, newKey, default(CancellationToken)),
                Times.Once);
        }

        #endregion

        #endregion
    }
}
