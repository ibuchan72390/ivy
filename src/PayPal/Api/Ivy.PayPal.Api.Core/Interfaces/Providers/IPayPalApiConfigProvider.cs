namespace Ivy.PayPal.Api.Core.Interfaces.Providers
{
    public interface IPayPalApiConfigProvider
    {
        string ClientId { get; }
        string ClientSecret { get; }
    }
}
