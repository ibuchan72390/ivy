using Ivy.Auth0.Authorization.Core.Services;
using Ivy.Auth0.Core.Base;
using Ivy.Web.Core.Client;

namespace Ivy.Auth0.Authorization.Services
{
    public class AuthorizationApiTokenGenerator : 
        Auth0BaseTokenGenerator<IAuth0AuthorizationRequestGenerator>,
        IAuthorizationApiTokenGenerator
    {
        #region Constructor

        public AuthorizationApiTokenGenerator(
            IAuth0AuthorizationRequestGenerator requestGenerator,
            IApiHelper apiHelper)
            : base(requestGenerator, apiHelper)
        {
        }

        #endregion
    }
}
