namespace Ivy.ReCaptcha.Core.Interfaces.Providers
{
    public interface IReCaptchaConfigurationProvider
    {
        string Secret { get; }
        string ValidationUrl { get; }
    }
}
