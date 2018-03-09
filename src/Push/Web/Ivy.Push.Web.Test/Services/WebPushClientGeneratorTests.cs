using Ivy.IoC;
using Ivy.Push.Web.Core.Interfaces.Clients;
using Ivy.Push.Web.Core.Interfaces.Services;
using Ivy.Push.Web.Test.Base;
using System.Collections.Generic;
using Xunit;

namespace Ivy.Push.Web.Test.Services
{
    public class WebPushClientGeneratorTests : BaseWebPushTest
    {
        #region Variables & Constants

        private readonly IWebPushClientGenerator _sut;

        #endregion

        #region SetUp & TearDown

        public WebPushClientGeneratorTests()
        {
            _sut = ServiceLocator.Instance.GetService<IWebPushClientGenerator>();
        }

        #endregion

        #region Tests

        [Fact]
        public void GenerateClient_Only_Creates_A_Single_Instance()
        {
            var created = new List<IWebPushClient> { _sut.GenerateClient() };

            for (var i = 0; i < 5; i++)
            {
                var item = _sut.GenerateClient();

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
