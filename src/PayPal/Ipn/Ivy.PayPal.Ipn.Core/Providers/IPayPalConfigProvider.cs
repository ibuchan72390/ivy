namespace Ivy.PayPal.Ipn.Core.Providers
{
    public interface IPayPalConfigProvider
    {
        string ReceiverEmail { get; }
    }
}
