

using System.Threading;

namespace Timesheets.DB.DAL
{
    public interface IBaseRepo<T> where T : class
    {

        List<T> GetAll();
        
        T Get(int id);
main


        T GetByTerm(string term);


        IEnumerable<T> GetSomePersons(int skip, int take);


        Guid AddItem(T item);


        T UpdateItem(T item);


        void DeleteItem(Guid id);
    }
}
