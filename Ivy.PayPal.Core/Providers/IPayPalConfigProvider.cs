﻿namespace Ivy.PayPal.Core.Providers
{
    public interface IPayPalConfigProvider
    {
        string ReceiverEmail { get; }

        bool IsSandbox { get; }
    }
}
