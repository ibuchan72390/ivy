using IBFramework.Auth0.Core.Services;
using IBFramework.Auth0.Services;
using IBFramework.IoC.Core;

namespace IBFramework.Auth0.IoC
{
    public class Auth0Installer : IContainerInstaller
    {
        public void Install(IContainerGenerator containerGenerator)
        {
            containerGenerator.RegisterSingleton<IApiAuthTokenGenerator, ApiAuthTokenGenerator>();
            containerGenerator.RegisterSingleton<IAuth0ManagementRequestGenerator, Auth0ManagementRequestGenerator>();
            containerGenerator.RegisterSingleton<IAuth0ManagementService, Auth0ManagementService>();
            containerGenerator.RegisterSingleton<IAuth0ContextProcessor, Auth0ContextProcessor>();
            containerGenerator.RegisterSingleton<IAuthTokenExtractor, AuthTokenExtractor>();
            containerGenerator.RegisterSingleton<IJwtProcessor, JwtProcessor>();
            containerGenerator.RegisterSingleton<IUserProvider, UserProvider>();
        }
    }

    public static class Auth0InstallerExtension
    {
        public static IContainerGenerator InstallAuth0(this IContainerGenerator containerGenerator)
        {
            new Auth0Installer().Install(containerGenerator);
            return containerGenerator;
        }
    }
}
