﻿using Ivy.Push.Web.Core.Interfaces.Clients;

namespace Ivy.Push.Web.Core.Interfaces.Services
{
    public interface IWebPushClientService
    {
        IWebPushClient GetClient();
    }
}
