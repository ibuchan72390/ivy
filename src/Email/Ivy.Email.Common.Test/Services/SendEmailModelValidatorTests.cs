using Ivy.Email.Common.Test.Base;
using Ivy.Email.Core.Domain;
using Ivy.Email.Core.Services;
using Ivy.IoC;
using System;
using System.Collections.Generic;
using Xunit;

namespace Ivy.Email.Common.Test.Services
{
    public class SendEmailModelValidatorTests : CommonEmailTestBase
    {
        #region Variables & Constants

        private readonly ISendEmailModelValidator _sut;
        
        #endregion

        #region SetUp & TearDown

        public SendEmailModelValidatorTests()
        {
            _sut = ServiceLocator.Instance.Resolve<ISendEmailModelValidator>();
        }

        #endregion

        #region Tests
        
        [Fact]
        public void Validate_Throws_Exception_If_Recipients_Null()
        {
            var model = new SendEmailModel { Recipients = null };

            AssertModelException(model, "recipients is null");
        }

        [Fact]
        public void Validate_Throws_Exception_If_No_Recipients()
        {
            var model = new SendEmailModel { Recipients = new List<string>() };

            AssertModelException(model, "recipients is empty");
        }

        [Fact]
        public void Validate_Throws_Exception_If_Content_Is_Null()
        {
            var model = new SendEmailModel { Recipients = new List<string> { "test@gmail.com" } };

            AssertModelException(model, "content is null");
        }

        [Fact]
        public void Validate_Throws_Exception_If_Content_From_Is_Null()
        {
            var model = new SendEmailModel
            {
                Recipients = new List<string> { "test@gmail.com" },
                Content = new EmailContent()
            };

            AssertModelException(model, "content from is null");
        }

        [Fact]
        public void Validate_Throws_Exception_If_Content_From_Email_Is_Null()
        {
            var model = new SendEmailModel
            {
                Recipients = new List<string> { "test@gmail.com" },
                Content = new EmailContent
                {
                    From = new EmailSender { Email = null }
                }
            };

            AssertModelException(model, "email from address is null or empty");
        }

        [Fact]
        public void Validate_Throws_Exception_If_Content_From_Email_Is_Empty()
        {
            var model = new SendEmailModel
            {
                Recipients = new List<string> { "test@gmail.com" },
                Content = new EmailContent
                {
                    From = new EmailSender { Email = "" }
                }
            };

            AssertModelException(model, "email from address is null or empty");
        }

        [Fact]
        public void Validate_Throws_Exception_If_Content_Subject_Is_Null()
        {
            var model = new SendEmailModel
            {
                Recipients = new List<string> { "test@gmail.com" },
                Content = new EmailContent
                {
                    From = new EmailSender { Email = "test@gmail.com" },
                    Subject = null
                }
            };

            AssertModelException(model, "content subject is null or empty");
        }

        [Fact]
        public void Validate_Throws_Exception_If_Content_Subject_Is_Empty()
        {
            var model = new SendEmailModel
            {
                Recipients = new List<string> { "test@gmail.com" },
                Content = new EmailContent
                {
                    From = new EmailSender { Email = "test@gmail.com" },
                    Subject = ""
                }
            };

            AssertModelException(model, "content subject is null or empty");
        }

        [Fact]
        public void Validate_Throws_Exception_If_Content_Html_Is_Null()
        {
            var model = new SendEmailModel
            {
                Recipients = new List<string> { "test@gmail.com" },
                Content = new EmailContent
                {
                    From = new EmailSender { Email = "test@gmail.com" },
                    Subject = "Subject",
                    Html = null
                }
            };

            AssertModelException(model, "content HTML is null or empty");
        }

        [Fact]
        public void Validate_Throws_Exception_If_Content_Html_Is_Empty()
        {
            var model = new SendEmailModel
            {
                Recipients = new List<string> { "test@gmail.com" },
                Content = new EmailContent
                {
                    From = new EmailSender { Email = "test@gmail.com" },
                    Subject = "Subject",
                    Html = ""
                }
            };

            AssertModelException(model, "content HTML is null or empty");
        }

        [Fact]
        public void Validate_Passes_Without_Issue_If_Valid()
        {
            var model = new SendEmailModel
            {
                Recipients = new List<string> { "test@gmail.com" },
                Content = new EmailContent
                {
                    From = new EmailSender { Email = "test@gmail.com" },
                    Subject = "Subject",
                    Html = "<p>TESTING</p>"
                }
            };

            _sut.Validate(model);

            // If we made it this far, we're fine
        }

        #endregion

        #region Helper Methods

        private void AssertModelException(SendEmailModel model, string exception)
        {
            var e = Assert.Throws<Exception>(() => _sut.Validate(model));

            string expectedMsg = $"Unable to send email model, {exception}!";

            Assert.Equal(expectedMsg, e.Message);
        }

        #endregion
    }
}
