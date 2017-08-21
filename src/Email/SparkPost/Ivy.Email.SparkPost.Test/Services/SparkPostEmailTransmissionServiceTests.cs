using Ivy.Email.Core.Domain;
using Ivy.Email.Core.Services;
using Ivy.Email.SparkPost.Core.Services;
using Ivy.Email.SparkPost.Test.Base;
using Ivy.IoC;
using Ivy.IoC.Core;
using Moq;
using SparkPostDotNet;
using SparkPostDotNet.Transmissions;
using System.Threading.Tasks;
using Xunit;

namespace Ivy.Email.SparkPost.Test.Services
{
    public class SparkPostEmailTransmissionServiceTests : SparkPostTestBase
    {
        #region Variables & Constants

        private readonly IEmailTransmissionService _sut;

        private readonly Mock<ISparkPostTransmissionGenerator> _mockGenerator;
        private readonly Mock<SparkPostClient> _mockClient;

        #endregion

        #region SetUp & TearDown

        public SparkPostEmailTransmissionServiceTests()
        {
            var containerGen = ServiceLocator.Instance.Resolve<IContainerGenerator>();

            base.ConfigureContainer(containerGen);

            _mockClient = new Mock<SparkPostClient>();
            containerGen.RegisterInstance<SparkPostClient>(_mockClient.Object);

            _mockGenerator = new Mock<ISparkPostTransmissionGenerator>();
            containerGen.RegisterInstance<ISparkPostTransmissionGenerator>(_mockGenerator.Object);

            _sut = containerGen.GenerateContainer().Resolve<IEmailTransmissionService>();
        }

        #endregion

        #region Tests

        [Fact]
        public async void SendEmailAsync_Sends_As_Expected()
        {
            var model = new SendEmailModel();
            var transmission = new Transmission();

            _mockGenerator.Setup(x => x.GenerateTransmission(model)).Returns(transmission);

            _mockClient.Setup(x => x.CreateTransmission(transmission)).Returns(Task.FromResult(0));

            await _sut.SendEmailAsync(model);

            _mockGenerator.Verify(x => x.GenerateTransmission(model), Times.Once);

            _mockClient.Verify(x => x.CreateTransmission(transmission), Times.Once);
        }

        #endregion
    }
}
