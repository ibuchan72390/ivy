﻿using Ivy.Push.Core.Interfaces.Models.Messages;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ivy.Push.Core.Interfaces.Services
{
    public interface IPushNotificationRequestFactory
    {
        Task<HttpRequestMessage> GeneratePushMessageRequestAsync(IDataPushMessage message);
    }
}
