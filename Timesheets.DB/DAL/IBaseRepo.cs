

namespace Timesheets.DB.DAL
{
    public interface IBaseRepo<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(CancellationToken token);

        Task<T> Get(Guid id, CancellationToken token);


        Task<T> GetByTerm(string term, CancellationToken token);


        Task<IEnumerable<T>> GetSomePersons(int skip, int take, CancellationToken token);


        Task<Guid> AddItem(T item, CancellationToken token);


        Task<T> UpdateItem(T item, CancellationToken token);


        Task DeleteItem(Guid id, CancellationToken token);
    }
}
