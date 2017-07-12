namespace Ivy.Auth0.Core.Models.Interfaces
{
    public interface IAuth0Phone
    {
        string phone_number { get; set; }
        bool phone_verified { get; set; }
    }
}
