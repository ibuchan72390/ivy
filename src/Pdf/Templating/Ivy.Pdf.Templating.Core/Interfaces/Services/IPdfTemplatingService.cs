using System.Collections.Generic;
using System.IO;

namespace Ivy.Pdf.Templating.Core.Interfaces.Services
{
    public interface IPdfTemplatingService
    {
        // We probably won't pass a PDF Template string in here, but a stream from S3
        void TemplatePdf(Stream pdfTemplate, Dictionary<string,string> replacementDictionary, Stream resultStream);
    }
}