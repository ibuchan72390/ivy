namespace Ivy.Auth0.Web.Core.Services
{
    /*
     * As awesome as this service is, let's not go about wiring this
     * class into our service methods.  We can't guarantee they're going
     * to be using web-based identity.  Simply parameterize the UserId
     * wherever necessary instead of referencing this piece.
     */
    public interface IUserProvider
    {
        string AuthenticationId { get; }
    }
}
