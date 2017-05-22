using IBFramework.IoC.Core;
using IBFramework.Web.Client;
using IBFramework.Web.Core.Client;
using IBFramework.Web.Core.Json;
using IBFramework.Web.Json;

namespace IBFramework.Web.IoC
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
        public static IContainerGenerator InstallWeb(this IContainerGenerator containerGenerator)
        {
            new WebInstaller().Install(containerGenerator);
            return containerGenerator;
        }
    }
}
