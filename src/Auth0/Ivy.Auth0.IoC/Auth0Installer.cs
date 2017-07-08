using Ivy.Auth0.Core.Services;
using Ivy.Auth0.Services;
using Ivy.IoC.Core;

namespace Ivy.Auth0.IoC
{
    public class Auth0Installer : IContainerInstaller
    {
        public void Install(IContainerGenerator containerGenerator)
        {
            containerGenerator.RegisterSingleton<IApiAuthTokenGenerator, ApiAuthTokenGenerator>();
            containerGenerator.RegisterSingleton<IAuth0ManagementRequestGenerator, Auth0ManagementRequestGenerator>();
            containerGenerator.RegisterSingleton<IAuth0QueryStringUriGenerator, Auth0QueryStringUriGenerator>();
            containerGenerator.RegisterSingleton<IAuth0ManagementService, Auth0ManagementService>();
            containerGenerator.RegisterSingleton<IAuth0ContextProcessor, Auth0ContextProcessor>();
            containerGenerator.RegisterSingleton<IAuthTokenExtractor, AuthTokenExtractor>();
            containerGenerator.RegisterSingleton<IJwtProcessor, JwtProcessor>();
            containerGenerator.RegisterSingleton<IUserProvider, UserProvider>();
        }
    }

    public static class Auth0InstallerExtension
    {
        public static IContainerGenerator InstallIvyAuth0(this IContainerGenerator containerGenerator)
        {
            new Auth0Installer().Install(containerGenerator);
            return containerGenerator;
        }
    }
}
