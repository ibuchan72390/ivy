using IBFramework.Data.Core.Interfaces.Pagination;

namespace IBFramework.Data.Core.Interfaces.Functional
{
    interface IPaginatedGetAll<TEntity>
        where TEntity : class
    {
        IPaginationResponse<TEntity> GetAll(IPaginationRequest request, ITranConn tc = null);
    }
}
