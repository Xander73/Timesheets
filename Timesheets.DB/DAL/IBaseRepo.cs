

namespace Timesheets.DB.DAL
{
    public interface IBaseRepo<T> where T : class
    {

        List<T> GetAll();
        
        T Get(int id);


        T GetByTerm(string term);


        IEnumerable<T> GetSomePersons(int skip, int take);
 main


        Task<Guid> AddItem(T item, CancellationToken token);



        T UpdateItem(T item);
 main


        Task DeleteItem(Guid id, CancellationToken token);
    }
}
