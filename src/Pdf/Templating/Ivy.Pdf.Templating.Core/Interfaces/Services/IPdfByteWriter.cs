using System.Collections.Generic;
using System.IO;

namespace Ivy.Pdf.Templating.Core.Interfaces.Services
{
    public interface IPdfByteWriter
    {
        void WriteBytesToPdf(Stream resultStream, IList<byte[]> pageBytes);
    }
}
