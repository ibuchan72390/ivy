using Ivy.Push.Core.Interfaces.Models;

namespace Ivy.Push.Core.Models.Wrappers
{
    /*
     * This class never needs to be referenced outside of the Ivy.Push projects.
     * This is just made to wrap up our PushNotification objects for submission to the API
     */
    public class PushMessageWrapper :
        IPushMessageWrapper
    {
        public IPushMessage message { get; set; }
    }
}
