using System.Threading.Tasks;

namespace Ivy.Data.Core.Interfaces.Functional
{
    public interface IGetEntityCount
    {
        int GetCount(ITranConn tc = null);
    }

    public interface IGetEntityCountAsync
    {
        Task<int> GetCountAsync(ITranConn tc = null);
    }

    public interface IGetCount :
        IGetEntityCount,
        IGetEntityCountAsync
    {
    }
}
