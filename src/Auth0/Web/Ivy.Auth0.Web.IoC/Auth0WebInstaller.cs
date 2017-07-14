using Ivy.IoC.Core;
using Ivy.Auth0.Web.Services;
using Ivy.Auth0.Web.Core.Services;

namespace Ivy.Auth0.Web.IoC
{
    public class Auth0WebInstaller : IContainerInstaller
    {
        public void Install(IContainerGenerator containerGenerator)
        {
            containerGenerator.RegisterSingleton<IAuth0ContextProcessor, Auth0ContextProcessor>();
            containerGenerator.RegisterSingleton<IAuthTokenExtractor, AuthTokenExtractor>();
            containerGenerator.RegisterSingleton<IJwtProcessor, JwtProcessor>();
            containerGenerator.RegisterSingleton<IUserProvider, UserProvider>();
        }
    }

    public static class Auth0WebInstallerExtension
    {
        public static IContainerGenerator InstallIvyAuth0Web(this IContainerGenerator containerGen)
        {
            new Auth0WebInstaller().Install(containerGen);
            return containerGen;
        }
    }
}
