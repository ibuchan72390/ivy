using iTextSharp.text;
using iTextSharp.text.pdf;
using Ivy.Pdf.Templating.Core.Interfaces.Services;
using System.Collections.Generic;
using System.IO;

/*
 * I have absolutely no idea how to test this with all these fucking new-ed up
 * custom classes from ITextSharp. 
 * 
 * Why they refuse to move to a service-oriented strategy, I have no idea, but this
 * is the bullshit that I'm left to deal with.
 */
namespace Ivy.Pdf.Templating.Services
{
    public class PdfByteWriter : IPdfByteWriter
    {
        public void WriteBytesToPdf(Stream resultStream, IList<byte[]> pageBytes)
        {
            // Create a document container to assemble our pieces in:
            Document mainDocument = new Document(PageSize.A4);

            // Copy the contents of our document to our output stream:
            var pdfCopier = new PdfSmartCopy(mainDocument, resultStream);

            // Once again, don't close the stream when we close the document:
            pdfCopier.CloseStream = false;

            mainDocument.Open();
            foreach (var pageByteArray in pageBytes)
            {
                // Copy each page into the document:
                mainDocument.NewPage();
                pdfCopier.AddPage(pdfCopier.GetImportedPage(new PdfReader(pageByteArray), 1));
            }
            pdfCopier.Close();

            // Set stream position to the beginning before returning:
            resultStream.Position = 0;
        }
    }
}
