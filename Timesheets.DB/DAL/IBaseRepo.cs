

using System.Threading;

namespace Timesheets.DB.DAL
{
    public interface IBaseRepo<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> Get(int id);


        Task<T> GetByTerm(string term);


        Task<IEnumerable<T>> GetSomePersons(int skip, int take);


        Task<IEnumerable<Guid>> AddItem(T item);


        Task<T> UpdateItem(T item);


        Task DeleteItem(int id);
    }
}
