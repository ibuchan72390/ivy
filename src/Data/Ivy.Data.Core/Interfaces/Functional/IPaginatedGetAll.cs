using Ivy.Data.Core.Interfaces.Pagination;
using System.Threading.Tasks;

namespace Ivy.Data.Core.Interfaces.Functional
{
    public interface IPaginatedGetAllEntities<TEntity>
        where TEntity : class
    {
        IPaginationResponse<TEntity> GetAll(IPaginationRequest request, ITranConn tc = null);
    }

    public interface IPaginatedGetAllEntitiesAsync<TEntity>
        where TEntity : class
    {
        Task<IPaginationResponse<TEntity>> GetAllAsync(IPaginationRequest request, ITranConn tc = null);
    }

    public interface IPaginatedGetAll<TEntity> :
        IPaginatedGetAllEntities<TEntity>,
        IPaginatedGetAllEntitiesAsync<TEntity>
        where TEntity : class
    {
    }
}
