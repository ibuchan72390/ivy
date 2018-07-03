using Ivy.IoC;
using Ivy.Push.Web.Core.Models;
using Ivy.Push.Web.Test.Base;
using Ivy.Web.Core.Json;
using Xunit;

namespace Ivy.Push.Web.Test.Models
{
    public class WebPushSubscriptionTests : 
        BaseWebPushTest<IJsonSerializationService>
    {
        #region Tests

        [Fact]
        public void WebPushSubscription_Converts_As_Expected()
        {
            // Arrange

            // Sample WebPushSubscription taken from Google documentation at the following link:
            // https://developers.google.com/web/fundamentals/push-notifications/subscribing-a-user#what_is_a_pushsubscription
            const string json = 
                @"{
                    ""endpoint"": ""https://some.pushservice.com/something-unique"",
                    ""keys"": {
                        ""p256dh"": ""BIPUL12DLfytvTajnryr2PRdAgXS3HGKiLqndGcJGabyhHheJYlNGCeXl1dn18gSJ1WAkAPIxr4gK0_dQds4yiI="",
                        ""auth"":""FPssNDTKnInHVndSTdbKFw==""
                    }
                }";


            // Act
            var result = Sut.Deserialize<WebPushSubscription>(json);


            // Assert
            Assert.NotNull(result);
        }

        #endregion
    }
}
