﻿namespace Ivy.Mailing.MailChimp.Core.Providers
{
    public interface IMailChimpConfigurationProvider
    {
        string DataCenter { get; }

        string ApiKey { get; }

        string ListId { get; }
    }
}