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
using System.Net;
using System.Linq;

namespace Ivy.Mailing.ActiveCampaign.Test.Services
{
    public class ActiveCampaignRequestGeneratorTests : 
        BaseActiveCampaignTest<IMailingRequestFactory>
    {
        #region Variables & Constants

        private Mock<IActiveCampaignConfigurationProvider> _mockConfigProvider;

        private const string apiUrl = "http://api-url.com/";
        private const string apiKey = "test-api-key";
        private const string listId = "test-list-id";

        private const string outputType = "json";
        private const string email = "test@gmail.com";

        #endregion

        #region SetUp & TearDown

        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            _mockConfigProvider = InitializeMoq<IActiveCampaignConfigurationProvider>(containerGen);
            _mockConfigProvider.Setup(x => x.ApiUrl).Returns(apiUrl);
            _mockConfigProvider.Setup(x => x.ApiKey).Returns(apiKey);
            _mockConfigProvider.Setup(x => x.ListId).Returns(listId);
        }

        #endregion

        #region Tests

        #region GenerateGetMemberRequest

        [Fact]
        public void GenerateGetMemberRequest_Generates_Request_As_Expected()
        {
            // Act
            var result = Sut.GenerateGetMemberRequest(email);

            // Assert
            Assert.Equal(HttpMethod.Get, result.Method);
            Assert.Equal(result.RequestUri.ToString(), $"{apiUrl}/admin/api.php?api_action=contact_list&api_key={apiKey}&api_output={outputType}&filters[email]={email}");
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
                Sut.GenerateAddMemberRequest(member) : 
                Sut.GenerateEditMemberRequest(member);

            // Assert
            var action = isAdd ? "contact_add" : "contact_edit";

            var requestUri = $"{apiUrl}/admin/api.php?api_action={action}&api_key={apiKey}&api_output={outputType}";
            Assert.Equal(requestUri, result.RequestUri.ToString());

            Assert.Equal(HttpMethod.Post, result.Method);

            var body = await result.Content.ReadAsStringAsync();

            // Need to decode this before we can rip it to a dictionary
            body = WebUtility.UrlDecode(body);

            var bodyParts = body.Split('&');

            var bodyDict = bodyParts.ToDictionary(x => x.Split('=')[0], x => x.Split('=')[1]);

            if (!isAdd)
            {
                Assert.Equal(member.Id, bodyDict["id"]);
            }

            Assert.Equal(email, bodyDict["email"]);
            Assert.Equal(member.FirstName, bodyDict["first_name"]);
            Assert.Equal(member.LastName, bodyDict["last_name"]);
            Assert.Equal(member.Phone, bodyDict["phone"]);
            Assert.Equal(listId, bodyDict[$"p[{listId}]"]);

            foreach (var item in member.ExtraData)
            {
                var propVal = bodyDict[$"field[{item.Key}]"];
                Assert.Equal(item.Value, propVal);
            }
        }

        #endregion
    }
}
