using Ivy.IoC;
using Ivy.Push.Web.Core.Interfaces.Clients;
using Ivy.Push.Web.Core.Interfaces.Services;
using Ivy.Push.Web.Test.Base;
using System.Collections.Generic;
using Xunit;

namespace Ivy.Push.Web.Test.Services
{
    public class WebPushClientGeneratorTests : 
        BaseWebPushTest<IWebPushClientGenerator>
    {
        #region Tests

        [Fact]
        public void GenerateClient_Only_Creates_A_Single_Instance()
        {
            var created = new List<IWebPushClient> { Sut.GenerateClient() };

            for (var i = 0; i < 5; i++)
            {
                var item = Sut.GenerateClient();

                foreach (var validationItem in created)
                {
                    Assert.Same(item, validationItem);
                }

                created.Add(item);
            }
        }

        #endregion
    }
}
