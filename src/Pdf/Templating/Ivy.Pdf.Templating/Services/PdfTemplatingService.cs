using System.Collections.Generic;
using System.IO;
using Ivy.Pdf.Templating.Core.Interfaces.Services;


/*
 * This entire class has specifically been taken from an online code sample for the old ITextSharp version
 * The sample can be seen here:
 * https://www.codeproject.com/Articles/691723/Csharp-Generate-and-Deliver-PDF-Files-On-Demand-fr
 * 
 * All of this stuff is kind of really hard to test because ITextSharp seemingly loves to new up objects all over the place
 * There's got to be a way I can better break this down into services that can be better wrapped in viable tests
 */
namespace Ivy.Pdf.Templating.Services
{
    public class PdfTemplatingService : IPdfTemplatingService
    {
        #region Variables & Constants

        private readonly IPdfPageGenerator _pageGenerator;
        private readonly IPdfByteWriter _byteWriter;

        #endregion

        #region Constructor

        public PdfTemplatingService(
            IPdfPageGenerator pageGenerator,
            IPdfByteWriter byteWriter)
        {
            _pageGenerator = pageGenerator;
            _byteWriter = byteWriter;
        }

        #endregion

        #region Public Methods

        public void TemplatePdf(Stream pdfTemplate, Dictionary<string, string> replacementDictionary, Stream resultStream)
        {
            var pagesAll = _pageGenerator.GeneratePageBytes(pdfTemplate, replacementDictionary);

            _byteWriter.WriteBytesToPdf(resultStream, pagesAll);
        }

        #endregion
    }
}
