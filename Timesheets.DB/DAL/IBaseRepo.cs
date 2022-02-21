namespace Timesheets.DB.DAL
{
    public interface IBaseRepo<T>
    {
        List<T> GetAll();
        
        T Get(int id);


        T GetByTerm(string term);


        IEnumerable<T> GetSomePersons(int skip, int take);


        int AddItem(T item);


        T UpdateItem(T item);


        void DeleteItem(int id);
    }
}
