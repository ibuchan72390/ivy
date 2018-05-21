using Ivy.TestHelper;
using Ivy.Pdf.Templating.IoC;

namespace Ivy.Pdf.Templating.Test.Base
{
    public class PdfTemplatingTestBase : TestBase
    {
        protected override void InitWrapper()
        {
            base.Init(containerGen => containerGen.InstallIvyPdfTempalting());
        }
    }
}
