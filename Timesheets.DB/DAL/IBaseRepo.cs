namespace Timesheets.DB.DAL
{
    public interface IBaseRepo<T>
    {
        List<T> GetAll();
        

        int AddItem(T item);


        int UpdateItem(T item);


        void DeleteItem(int id);
    }
}
