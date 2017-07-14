using Ivy.Auth0.Core.Models.Responses;
using Ivy.Auth0.Core.Sevices;
using Ivy.Web.Core.Client;
using System.Threading.Tasks;

namespace Ivy.Auth0.Core.Base
{
    public class Auth0BaseTokenGenerator<TRequestGenerator>
        where TRequestGenerator : IApiTokenRequestGenerator
    {
        #region Variables & Constants

        private readonly TRequestGenerator _requestGenerator;
        private readonly IApiHelper _apiHelper;

        #endregion

        #region Constructor

        public Auth0BaseTokenGenerator(
            TRequestGenerator requestGenerator,
            IApiHelper apiHelper)
        {
            _requestGenerator = requestGenerator;
            _apiHelper = apiHelper;
        }

        #endregion

        #region Public Methods

        public async Task<string> GetApiTokenAsync()
        {
            var req = _requestGenerator.GenerateApiTokenRequest();

            var responseModel = await _apiHelper.GetApiDataAsync<Auth0AccessTokenResponse>(req);

            return responseModel.access_token;
        }

        #endregion
    }
}
