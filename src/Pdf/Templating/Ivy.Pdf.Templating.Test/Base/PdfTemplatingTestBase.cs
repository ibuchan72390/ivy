using Ivy.TestHelper;
using Ivy.Pdf.Templating.IoC;
using Ivy.IoC.Core;

namespace Ivy.Pdf.Templating.Test.Base
{
    public class PdfTemplatingTestBase<T> : TestBase<T>
        where T: class
    {
        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            containerGen.InstallIvyPdfTemplating();
        }
    }
}
