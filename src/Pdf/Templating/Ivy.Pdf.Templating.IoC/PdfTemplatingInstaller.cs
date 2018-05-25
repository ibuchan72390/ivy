using Ivy.IoC.Core;
using Ivy.Pdf.Templating.Core.Interfaces.Services;
using Ivy.Pdf.Templating.Services;

namespace Ivy.Pdf.Templating.IoC
{
    public class PdfTemplatingInstaller : IContainerInstaller
    {
        public void Install(IContainerGenerator containerGenerator)
        {
            containerGenerator.RegisterSingleton<IPdfTemplatingService, PdfTemplatingService>();
            containerGenerator.RegisterSingleton<IPdfPageGenerator, PdfPageGenerator>();
            containerGenerator.RegisterSingleton<IPdfByteWriter, PdfByteWriter>();
        }
    }

    public static class PdfTemplatingInstallerExtension
    {
        public static IContainerGenerator InstallIvyPdfTemplating(this IContainerGenerator containerGenerator)
        {
            new PdfTemplatingInstaller().Install(containerGenerator);
            return containerGenerator;
        }
    }
}
