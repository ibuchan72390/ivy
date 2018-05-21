using Ivy.IoC;
using Ivy.Pdf.Templating.Core.Interfaces.Services;
using Ivy.Pdf.Templating.Test.Base;
using Moq;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Ivy.Pdf.Templating.Test.Services
{
    public class PdfTemplatingServiceTests : PdfTemplatingTestBase
    {
        #region Variables & Constants

        private readonly IPdfTemplatingService sut;

        private readonly Mock<IPdfPageGenerator> _mockPageGen;
        private readonly Mock<IPdfByteWriter> _mockByteWriter;

        #endregion

        #region Setup & Teardown

        public PdfTemplatingServiceTests()
        {
            var containerGen = new ContainerGenerator();

            base.ConfigureContainer(containerGen);

            _mockPageGen = new Mock<IPdfPageGenerator>();
            containerGen.RegisterInstance<IPdfPageGenerator>(_mockPageGen.Object);

            _mockByteWriter = new Mock<IPdfByteWriter>();
            containerGen.RegisterInstance<IPdfByteWriter>(_mockByteWriter.Object);

            sut = containerGen.GenerateContainer().GetService<IPdfTemplatingService>();
        }

        #endregion

        #region Tests

        [Fact]
        public void TemplatePdf_Executes_As_Expected()
        {
            var replacementDict = new Dictionary<string, string>();

            var resultBytes = new List<byte[]>
            {
                new byte[0]
            };

            using (var resultStream = new MemoryStream())
            {
                using (var fileStream = new MemoryStream())
                {
                    _mockPageGen.Setup(x => x.GeneratePageBytes(fileStream, replacementDict))
                        .Returns(resultBytes);

                    _mockByteWriter.Setup(x => x.WriteBytesToPdf(resultStream, resultBytes));


                    // Act
                    sut.TemplatePdf(fileStream, replacementDict, resultStream);


                    // Assert
                    _mockPageGen.Verify(x => x.GeneratePageBytes(fileStream, replacementDict), Times.Once);

                    _mockByteWriter.Verify(x => x.WriteBytesToPdf(resultStream, resultBytes), Times.Once);
                }
            }

        }

        #endregion
    }
}
