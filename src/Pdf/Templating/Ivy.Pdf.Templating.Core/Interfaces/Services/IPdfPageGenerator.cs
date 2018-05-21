using System.Collections.Generic;
using System.IO;

namespace Ivy.Pdf.Templating.Core.Interfaces.Services
{
    public interface IPdfPageGenerator
    {
        IList<byte[]> GeneratePageBytes(Stream pdfTemplate, Dictionary<string, string> replacementDictionary);
    }
}
