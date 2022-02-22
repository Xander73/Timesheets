

using System.Threading;

namespace Timesheets.DB.DAL
{
    public interface IBaseRepo<T> where T : class
    {
        Task<IActionResultList<T> GetAll();

        T Get(Guid id);


        T GetByTerm(string term);


        IEnumerable<T> GetSomePersons(int skip, int take);


        Guid AddItem(T item);


        T UpdateItem(T item);


        void DeleteItem(Guid id);
    }
}
