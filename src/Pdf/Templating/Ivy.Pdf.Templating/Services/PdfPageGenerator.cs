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
    public class PdfPageGenerator : IPdfPageGenerator
    {
        public IList<byte[]> GeneratePageBytes(Stream pdfTemplate, Dictionary<string, string> replacementDictionary)
        {
            // Agggregate successive pages here:
            var pagesAll = new List<byte[]>();

            // Hold individual pages Here:
            byte[] pageBytes = null;

            // Read the form template for each item to be output:
            var templateReader = new PdfReader(pdfTemplate);

            using (var tempStream = new MemoryStream())
            {
                PdfStamper stamper = new PdfStamper(templateReader, tempStream);
                stamper.FormFlattening = true;
                AcroFields fields = stamper.AcroFields;
                stamper.Writer.CloseStream = false;

                foreach (string name in replacementDictionary.Keys)
                {
                    fields.SetField(name, replacementDictionary[name]);
                }

                // If we had not set the CloseStream property to false, 
                // this line would also kill our memory stream:
                stamper.Close();

                // Reset the stream position to the beginning before reading:
                tempStream.Position = 0;

                // Grab the byte array from the temp stream . . .
                pageBytes = tempStream.ToArray();

                // And add it to our array of all the pages:
                pagesAll.Add(pageBytes);
            }

            return pagesAll;
        }
    }
}
