using System;
using System.Collections.Generic;
using System.Text;

namespace Ivy.PayPal.Api.Core.Interfaces.Services
{
    public interface IPayPalUrlGenerator
    {
        string GetPayPalUrl();
    }
}
