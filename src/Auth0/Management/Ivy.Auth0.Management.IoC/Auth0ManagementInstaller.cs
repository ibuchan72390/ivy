using Ivy.IoC.Core;
using Ivy.Auth0.Management.Transformers;
using Ivy.Auth0.Management.Core.Transformers;
using Ivy.Auth0.Management.Services;
using Ivy.Auth0.Management.Core.Services;

namespace Ivy.Auth0.Management.IoC
{
    public class Auth0ManagementInstaller : IContainerInstaller
    {
        public void Install(IContainerGenerator containerGenerator)
        {
            containerGenerator.RegisterSingleton<IManagementApiTokenGenerator, ApiManagementTokenGenerator>();
            containerGenerator.RegisterSingleton<IAuth0ManagementRequestGenerator, Auth0ManagementRequestGenerator>();
            containerGenerator.RegisterSingleton<IAuth0QueryStringUriGenerator, Auth0QueryStringUriGenerator>();
            containerGenerator.RegisterSingleton<IAuth0JsonManipulator, Auth0JsonManipulator>();
            containerGenerator.RegisterSingleton<IAuth0JsonGenerator, Auth0JsonGenerator>();

            // API Services
            containerGenerator.RegisterSingleton<IAuth0AccountManagementService, Auth0AccountManagementService>();
            containerGenerator.RegisterSingleton<IAuth0UserManagementService, Auth0UserManagementService>();

            // Transformer
            containerGenerator.RegisterSingleton<IUserPaginatedRequestTransformer, UserPaginatedRequestTransformer>();
        }
    }

    public static class Auth0ManagementInstallerExtension
    {
        public static IContainerGenerator InstallIvyAuth0Management(this IContainerGenerator containerGen)
        {
            new Auth0ManagementInstaller().Install(containerGen);
            return containerGen;
        }
    }
}
