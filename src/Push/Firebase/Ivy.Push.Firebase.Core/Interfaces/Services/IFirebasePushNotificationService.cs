﻿using Ivy.Push.Core.Interfaces.Models.Messages;
using Ivy.Push.Firebase.Core.Models.Responses;
using System.Threading.Tasks;

namespace Ivy.Push.Firebase.Core.Interfaces.Services
{
    public interface IFirebasePushNotificationService
    {
        Task<PushResponse> SendPushNotificationAsync(IDataPushMessage message);
    }
}
