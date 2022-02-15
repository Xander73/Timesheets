

using System.Threading;

namespace Timesheets.DB.DAL
{
    public interface IBaseRepo<T>
    {
        Task<IEnumerable<T>> GetAllAsync(CancellationToken token);
        

        Task AddItemAsync(T item, CancellationToken token);


        Task UpdateItemAsync(T item, CancellationToken token);


        Task DeleteItemAsync(Guid id, CancellationToken token);
    }
}
