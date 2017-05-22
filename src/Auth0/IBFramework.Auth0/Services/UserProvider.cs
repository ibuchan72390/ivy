using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace IBFramework.Auth0.Services
{
    public class UserProvider : IUserProvider
    {
        #region Private Attrs

        private IHttpContextAccessor _contextAccessor;

        #endregion

        #region Constructor

        public UserProvider(
            IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        #endregion

        #region Public Attrs

        public string AuthenticationId => GetClaimValue(ClaimTypes.NameIdentifier);

        #endregion

        #region Helper Methods

        private string GetClaimValue(string claimKey)
        {
            if (_contextAccessor.HttpContext.User == null)
            {
                throw new Exception("No user has been authenticated in the provided context!");
            }

            var value = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == claimKey);

            if (value == null)
            {
                throw new Exception($"The provided claim key does not exist on the User! Claim Key: {claimKey}");
            }

            return value.Value;
        }

        #endregion
    }
}
