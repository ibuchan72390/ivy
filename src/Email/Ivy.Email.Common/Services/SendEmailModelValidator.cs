using Ivy.Email.Core.Services;
using System;
using Ivy.Email.Core.Domain;
using System.Linq;

namespace Ivy.Email.Common.Services
{
    public class SendEmailModelValidator : ISendEmailModelValidator
    {
        public void Validate(SendEmailModel model)
        {
            if (model.Recipients == null)
            {
                ThrowException("recipients is null");
            }

            if (!model.Recipients.Any())
            {
                ThrowException("recipients is empty");
            }

            if (model.Content == null)
            {
                ThrowException("content is null");
            }

            if (model.Content.From == null)
            {
                ThrowException("content from is null");
            }

            if (string.IsNullOrEmpty(model.Content.From.Email))
            {
                ThrowException("email from address is null or empty");
            }

            if (string.IsNullOrEmpty(model.Content.Subject))
            {
                ThrowException("content subject is null or empty");
            }

            if (string.IsNullOrEmpty(model.Content.Html))
            {
                ThrowException("content HTML is null or empty");
            }
        }

        private void ThrowException(string exception)
        {
            throw new Exception($"Unable to send email model, {exception}!");
        }
    }
}
