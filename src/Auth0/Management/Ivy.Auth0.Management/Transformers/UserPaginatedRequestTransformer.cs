using Ivy.Auth0.Core.Models;
using Ivy.Data.Core.Interfaces.Pagination;
using Ivy.Data.Common.Pagination;
using Ivy.Auth0.Management.Core.Transformers;
using Ivy.Auth0.Management.Core.Models.Requests;
using Ivy.Auth0.Management.Core.Models.Responses;

namespace Ivy.Auth0.Management.Transformers
{
    public class UserPaginatedRequestTransformer : IUserPaginatedRequestTransformer
    {
        public Auth0ListUsersRequest Transform(IPaginationRequest request)
        {
            return new Auth0ListUsersRequest
            {
                Page = request.PageNumber,
                PerPage = request.PageCount,
                QueryString = request.Search
            };
        }

        public IPaginationResponse<Auth0User> Transform(Auth0ListUsersResponse response)
        {
            return new PaginationResponse<Auth0User>
            {
                Data = response.users,
                TotalCount = response.total
            };
        }
    }
}
