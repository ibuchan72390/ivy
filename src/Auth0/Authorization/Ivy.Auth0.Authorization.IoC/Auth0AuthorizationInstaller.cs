using System;
using Ivy.IoC.Core;

namespace Ivy.Auth0.Authorization.IoC
{
    public class Auth0AuthorizationInstaller : IContainerInstaller
    {
        public void Install(IContainerGenerator containerGenerator)
        {
            //containerGenerator.RegisterSingleton<IApiManagementTokenGenerator, ApiAuthTokenGenerator>();
            //containerGenerator.RegisterSingleton<IAuth0ManagementRequestGenerator, Auth0ManagementRequestGenerator>();
            //containerGenerator.RegisterSingleton<IAuth0QueryStringUriGenerator, Auth0QueryStringUriGenerator>();
            //containerGenerator.RegisterSingleton<IAuth0ContextProcessor, Auth0ContextProcessor>();
            //containerGenerator.RegisterSingleton<IAuthTokenExtractor, AuthTokenExtractor>();
            //containerGenerator.RegisterSingleton<IJwtProcessor, JwtProcessor>();
            //containerGenerator.RegisterSingleton<IUserProvider, UserProvider>();
            //containerGenerator.RegisterSingleton<IAuth0JsonManipulator, Auth0JsonManipulator>();
            //containerGenerator.RegisterSingleton<IAuth0JsonGenerator, Auth0JsonGenerator>();

            //// API Services
            //containerGenerator.RegisterSingleton<IAuth0AccountManagementService, Auth0AccountManagementService>();
            //containerGenerator.RegisterSingleton<IAuth0UserManagementService, Auth0UserManagementService>();

            //// Transformer
            //containerGenerator.RegisterSingleton<IUserPaginatedRequestTransformer, UserPaginatedRequestTransformer>();

            throw new NotImplementedException();
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
