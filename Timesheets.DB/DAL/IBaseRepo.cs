using System.ComponentModel.DataAnnotations;

namespace Timesheets.DB.DAL
{
    public interface IBaseRepo<T> where T : class
    {
        Task<List<T>> GetAll(CancellationToken token);

        Task<T> Get(Guid id, CancellationToken token);


        Task<T> GetByTerm([MinLength(3), StringLength(50)]string term, CancellationToken token);


        Task<IEnumerable<T>> GetSomeItems([Range(0, int.MaxValue)]int skip, [Range(1, int.MaxValue)] int take, CancellationToken token);


        Task<Guid> AddItem(T item, CancellationToken token);
        

        Task<T> UpdateItem(T item, CancellationToken token);


        Task DeleteItem(Guid id, CancellationToken token);
    }
}
