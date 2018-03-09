using Ivy.Push.Web.Clients;
using Ivy.Push.Web.Core.Interfaces.Clients;
using Ivy.Push.Web.Core.Interfaces.Services;

/*
 * This will live in a singleton context so that we only ever generate one of these
 * Regardless, I'd rather nobody be hitting this thing if possible, they should be using
 * the WebPushClientService over everything else.
 * 
 * If we were really smart and fancy, we'd create an interface of our own for this project
 * that is basically satisfied by a single class inheriting from WebPushClient and implementing
 * our fancy new wrapper interface IWebPushClient that this project is seemingly missing
 */
namespace Ivy.Push.Web.Services
{
    public class WebPushClientGenerator : IWebPushClientGenerator
    {
        private readonly CustomWebPushClient _client = new CustomWebPushClient();

        public IWebPushClient GenerateClient()
        {
            return _client;
        }
    }
}
