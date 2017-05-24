using Ivy.Data.Core.Interfaces.Pagination;

namespace Ivy.Data.Core.Interfaces.Functional
{
    interface IPaginatedGetAll<TEntity>
        where TEntity : class
    {
        IPaginationResponse<TEntity> GetAll(IPaginationRequest request, ITranConn tc = null);
    }
}
