using Ivy.IoC.Core;
using Ivy.Transformer.Base;
using Ivy.Transformer.Base.Entity;
using Ivy.Transformer.Core.Interfaces.Entity;
using Ivy.Transformer.Core.Interfaces.EnumEntity;

/*
 * We should be able to make it so we can request a single base transformer if necessary
 * Seems that this pattern was pretty effective when working with Enum models.
 */

namespace Ivy.Transformer.IoC
{
    public class TransformerInstaller : IContainerInstaller
    {
        public void Install(IContainerGenerator container)
        {
            // Entity Transformers
            container.RegisterSingleton(typeof(IViewModelToEntityTransformer<,>), typeof(BaseEntityTransformer<,>));
            container.RegisterSingleton(typeof(IViewModelToEntityListTransformer<,>), typeof(BaseEntityTransformer<,>));
            container.RegisterSingleton(typeof(IEntityToViewModelTransformer<,>), typeof(BaseEntityTransformer<,>));
            container.RegisterSingleton(typeof(IEntityToViewModelListTransformer<,>), typeof(BaseEntityTransformer<,>));

            container.RegisterSingleton(typeof(IEntityTransformer<,>), typeof(BaseEntityTransformer<,>));

            // Enum Entity Transformers
            container.RegisterSingleton(typeof(IEnumEntityToViewModelTransformer<,>), typeof(BaseEnumEntityTransformer<,>));
            container.RegisterSingleton(typeof(IEnumEntityToViewModelListTransformer<,>), typeof(BaseEnumEntityTransformer<,>));

            container.RegisterSingleton(typeof(IEnumEntityTransformer<,>), typeof(BaseEnumEntityTransformer<,>));
        }
    }

    public static class TransformerInstallerExtension
    {
        public static IContainerGenerator InstallIvyTransformer(this IContainerGenerator containerGenerator)
        {
            new TransformerInstaller().Install(containerGenerator);
            return containerGenerator;
        }
    }
}
