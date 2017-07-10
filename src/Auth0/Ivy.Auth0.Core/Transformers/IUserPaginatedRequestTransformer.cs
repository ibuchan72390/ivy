using Ivy.Auth0.Core.Models;
using Ivy.Auth0.Core.Models.Requests;
using Ivy.Auth0.Core.Models.Responses;
using Ivy.Data.Core.Interfaces.Pagination;

namespace Ivy.Auth0.Core.Transformers
{
    public interface IUserPaginatedRequestTransformer
    {
        Auth0ListUsersRequest Transform(IPaginationRequest request);

        IPaginationResponse<Auth0User> Transform(Auth0ListUsersResponse response);
    }
}
