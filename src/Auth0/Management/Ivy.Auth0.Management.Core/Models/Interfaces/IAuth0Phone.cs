namespace Ivy.Auth0.Management.Core.Models.Interfaces
{
    public interface IAuth0Phone
    {
        string phone_number { get; set; }
        bool phone_verified { get; set; }
    }
}
