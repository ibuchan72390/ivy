using Ivy.Email.Core.Domain;
using Ivy.Email.SparkPost.Core.Services;
using Ivy.Email.SparkPost.Test.Base;
using Ivy.IoC;
using Ivy.TestUtilities;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Ivy.Email.SparkPost.Test.Services
{
    public class SparkPostTransmissionGeneratorTests : SparkPostTestBase
    {
        #region Variables & Constants

        private readonly ISparkPostTransmissionGenerator _sut;

        private readonly SendEmailModel TestModel = new SendEmailModel
        {
            Recipients = new List<string> { "test1@gmail.com", "test2@gmail.com", "test3@gmail.com" },
            Content = new EmailContent
            {
                From = new EmailSender { Email = "recipient@gmail.com", Name = "TEST" },
                Subject = "Subject",
                Html = "<p>TESTING</p>"
            }
        };

        #endregion

        #region SetUp & TearDown

        public SparkPostTransmissionGeneratorTests()
        {
            _sut = ServiceLocator.Instance.Resolve<ISparkPostTransmissionGenerator>();
        }

        #endregion

        #region Tests

        [Fact]
        public void GenerateTransmission_Translates_Properties_As_Expected()
        {
            var result = _sut.GenerateTransmission(TestModel);

            var resultRecipients = result.Recipients.Select(x => x.Address.EMail);

            AssertExtensions.FullBasicListExclusion(TestModel.Recipients, resultRecipients);
            Assert.Equal(TestModel.Content.Html, result.Content.Html);
            Assert.Equal(TestModel.Content.Subject, result.Content.Subject);
            Assert.Equal(TestModel.Content.From.Email, result.Content.From.EMail);
            Assert.Equal(TestModel.Content.From.Name, result.Content.From.Name);
        }

        #endregion
    }
}
