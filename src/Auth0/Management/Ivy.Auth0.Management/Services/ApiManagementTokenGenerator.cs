using Ivy.Auth0.Core.Base;
using Ivy.Auth0.Management.Core.Services;
using Ivy.Web.Core.Client;

namespace Ivy.Auth0.Management.Services
{
    public class ApiManagementTokenGenerator : 
        Auth0BaseTokenGenerator<IAuth0ManagementRequestGenerator>,
        IManagementApiTokenGenerator
    {
        #region Constructor

        public ApiManagementTokenGenerator(
            IAuth0ManagementRequestGenerator requestGenerator,
            IApiHelper apiHelper)
            :base(requestGenerator, apiHelper)
        {
        }

        #endregion
    }
}
