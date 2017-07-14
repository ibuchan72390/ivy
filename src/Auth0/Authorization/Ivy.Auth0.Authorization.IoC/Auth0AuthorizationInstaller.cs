using System;
using Ivy.IoC.Core;
using Ivy.Auth0.Authorization.Services;
using Ivy.Auth0.Authorization.Core.Services;

namespace Ivy.Auth0.Authorization.IoC
{
    public class Auth0AuthorizationInstaller : IContainerInstaller
    {
        public void Install(IContainerGenerator containerGenerator)
        {
            containerGenerator.RegisterSingleton<IAuth0AuthorizationRequestGenerator, Auth0AuthorizationRequestGenerator>();
            containerGenerator.RegisterSingleton<IAuth0AuthorizationService, Auth0AuthorizationService>();
            containerGenerator.RegisterSingleton<IAuthorizationApiTokenGenerator, AuthorizationApiTokenGenerator>();
        }
    }

    public static class Auth0AuthorizationInstallerExtension
    {
        public static IContainerGenerator InstallIvyAuth0Authorization(this IContainerGenerator containerGen)
        {
            new Auth0AuthorizationInstaller().Install(containerGen);
            return containerGen;
        }
    }
}
