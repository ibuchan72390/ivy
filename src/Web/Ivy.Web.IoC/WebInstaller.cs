using Ivy.IoC.Core;
using Ivy.Web.Client;
using Ivy.Web.Core.Client;
using Ivy.Web.Core.Json;
using Ivy.Web.Json;

namespace Ivy.Web.IoC
{
    public class WebInstaller : IContainerInstaller
    {
        public void Install(IContainerGenerator container)
        {
            // Client
            container.RegisterSingleton<IHttpClientHelper, HttpClientHelper>();
            container.RegisterSingleton<IApiHelper, ApiHelper>();

            // Json
            container.RegisterSingleton<IJsonSerializationService, JsonSerializationService>();
        }
    }

    public static class WebInstallerExtension
    {
        public static IContainerGenerator InstallIvyWeb(this IContainerGenerator containerGenerator)
        {
            new WebInstaller().Install(containerGenerator);
            return containerGenerator;
        }
    }
}
