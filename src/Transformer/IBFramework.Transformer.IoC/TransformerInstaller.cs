using IBFramework.IoC.Core;

/*
 * We should be able to make it so we can request a single base transformer if necessary
 * Seems that this pattern was pretty effective when working with Enum models.
 */

namespace IBFramework.Transformer.IoC
{
    public class TransformerInstaller : IContainerInstaller
    {
        public void Install(IContainerGenerator container)
        {
            
        }
    }

    public static class TransformerInstallerExtension
    {
        public static IContainerGenerator InstallTransformer(this IContainerGenerator containerGenerator)
        {
            new TransformerInstaller().Install(containerGenerator);
            return containerGenerator;
        }
    }
}
