

namespace Timesheets.DB.DAL
{
    public interface IBaseRepo<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        

        Task AddItemAsync(T item);


        Task UpdateItemAsync(T item);


        Task DeleteItemAsync(Guid id);
    }
}
