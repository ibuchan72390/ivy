using Ivy.Auth0.Core.Models.Requests;
using Ivy.Data.Core.Interfaces.Pagination;

namespace Ivy.Auth0.Core.Transformers
{
    public interface IUserPaginatedRequestTransformer
    {
        Auth0ListUsersRequest Transform(IPaginationRequest request);
    }
}
