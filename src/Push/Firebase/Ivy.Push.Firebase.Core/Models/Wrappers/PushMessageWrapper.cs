using Ivy.Push.Core.Interfaces.Models.Messages;
using Ivy.Push.Firebase.Core.Interfaces.Models.Wrappers;

namespace Ivy.Push.Firebase.Core.Models.Wrappers
{
    /*
     * This class never needs to be referenced outside of the Ivy.Push projects.
     * This is just made to wrap up our PushNotification objects for submission to the API
     */
    public class PushMessageWrapper :
        IPushMessageWrapper
    {
        public IDataPushMessage message { get; set; }
    }
}
