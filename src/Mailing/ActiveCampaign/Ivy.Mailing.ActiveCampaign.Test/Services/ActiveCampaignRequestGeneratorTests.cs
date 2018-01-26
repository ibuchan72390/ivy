using Ivy.Mailing.ActiveCampaign.Core.Interfaces.Providers;
using Ivy.Mailing.ActiveCampaign.Test.Base;
using Ivy.IoC;
using Ivy.IoC.Core;
using Ivy.Mailing.Core.Interfaces.Services;
using Ivy.Mailing.Core.Models;
using Moq;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Ivy.Mailing.ActiveCampaign.Test.Services
{
    public class ActiveCampaignRequestGeneratorTests : BaseActiveCampaignTest
    {
        #region Variables & Constants

        private readonly IMailingRequestFactory _sut;

        private readonly Mock<IActiveCampaignConfigurationProvider> _mockConfigProvider;

        private const string apiUrl = "http://api-url.com/";
        private const string apiKey = "test-api-key";
        private const string listId = "test-list-id";

        private const string outputType = "json";
        private const string email = "test@gmail.com";

        #endregion

        #region SetUp & TearDown

        public ActiveCampaignRequestGeneratorTests()
        {
            var containerGen = ServiceLocator.Instance.GetService<IContainerGenerator>();

            base.ConfigureContainer(containerGen);

            _mockConfigProvider = new Mock<IActiveCampaignConfigurationProvider>();
            _mockConfigProvider.Setup(x => x.ApiUrl).Returns(apiUrl);
            _mockConfigProvider.Setup(x => x.ApiKey).Returns(apiKey);
            _mockConfigProvider.Setup(x => x.ListId).Returns(listId);

            containerGen.RegisterInstance<IActiveCampaignConfigurationProvider>(_mockConfigProvider.Object);

            _sut = containerGen.GenerateContainer().GetService<IMailingRequestFactory>();
        }

        #endregion

        #region Tests

        #region GenerateGetMemberRequest

        [Fact]
        public void GenerateGetMemberRequest_Generates_Request_As_Expected()
        {
            // Act
            var result = _sut.GenerateGetMemberRequest(email);

            // Assert
            Assert.Equal(HttpMethod.Get, result.Method);
            Assert.Equal(result.RequestUri.ToString(), $"{apiUrl}/admin/api.php?api_action=contact_list&api_key={apiKey}&api_output={outputType}&filter[email]={email}");
        }

        #endregion

        #region GenerateSubmitMemberRequest

        [Fact]
        public async void GenerateSubmitMemberRequest_Generates_Request_As_Expected()
        {
            // Arrange
            var member = new MailingMember
            {
                Email = email,
                FirstName = "First",
                LastName = "Last",
                Phone = "Phone",
                ExtraData = new Dictionary<string, string>
                {
                    { "%TEST1%", "First" },
                    { "%TEST2%", "Second" },
                    { "%TEST3%", "Third" }
                }
            };

            await TestMemberEditAsync(true, member);
        }

        #endregion

        #region GenerateEditMemberRequest

        [Fact]
        public async void GenerateEditMemberRequest_Generates_Request_As_Expected()
        {
            // Arrange
            var member = new MailingMember
            {
                Id = "id",
                Email = email,
                FirstName = "First",
                LastName = "Last",
                Phone = "Phone",
                ExtraData = new Dictionary<string, string>
                {
                    { "%TEST1%", "First" },
                    { "%TEST2%", "Second" },
                    { "%TEST3%", "Third" }
                }
            };

            await TestMemberEditAsync(false, member);
        }

        #endregion

        #endregion

        #region Helper Methods

        private async Task TestMemberEditAsync(bool isAdd, MailingMember member)
        {
            // Act
            var result = isAdd ? 
                _sut.GenerateAddMemberRequest(member) : 
                _sut.GenerateEditMemberRequest(member);

            // Assert
            var action = isAdd ? "contact_add" : "contact_edit";

            var requestUri = $"{apiUrl}/admin/api.php?api_action={action}";
            Assert.Equal(requestUri, result.RequestUri.ToString());

            Assert.Equal(HttpMethod.Post, result.Method);

            var body = await result.Content.ReadAsStringAsync();

            var jObj = JObject.Parse(body);

            if (!isAdd)
            {
                Assert.Equal(member.Id, jObj.Property("id").Value);
            }

            Assert.Equal(apiKey, jObj.Property("api_key").Value);
            Assert.Equal(outputType, jObj.Property("api_output").Value);
            Assert.Equal(email, jObj.Property("email").Value);
            Assert.Equal(member.FirstName, jObj.Property("first_name").Value);
            Assert.Equal(member.LastName, jObj.Property("last_name").Value);
            Assert.Equal(member.Phone, jObj.Property("phone").Value);
            Assert.Equal(listId, jObj.Property($"p[{listId}]").Value);

            foreach (var item in member.ExtraData)
            {
                var propVal = jObj.Property($"field[{item.Key}]").Value;
                Assert.Equal(item.Value, propVal);
            }
        }

        #endregion
    }
}
