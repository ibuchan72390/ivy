using Ivy.IoC;
using Ivy.IoC.Core;
using Ivy.Pdf.Templating.Core.Interfaces.Services;
using Ivy.Pdf.Templating.Test.Base;
using Moq;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Ivy.Pdf.Templating.Test.Services
{
    public class PdfTemplatingServiceTests : 
        PdfTemplatingTestBase<IPdfTemplatingService>
    {
        #region Variables & Constants

        private Mock<IPdfPageGenerator> _mockPageGen;
        private Mock<IPdfByteWriter> _mockByteWriter;

        #endregion

        #region Setup & Teardown

        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            _mockPageGen = InitializeMoq<IPdfPageGenerator>(containerGen);
            _mockByteWriter = InitializeMoq<IPdfByteWriter>(containerGen);
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
                    Sut.TemplatePdf(fileStream, replacementDict, resultStream);


                    // Assert
                    _mockPageGen.Verify(x => x.GeneratePageBytes(fileStream, replacementDict), Times.Once);

                    _mockByteWriter.Verify(x => x.WriteBytesToPdf(resultStream, resultBytes), Times.Once);
                }
            }

        }

        #endregion
    }
}
